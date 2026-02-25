using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core; // Pour OrderBy(string)
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Linq;

public partial class ProductController
{
    [HttpGet]
    public IActionResult ExportPdf(string search)
    {
        // 1. On récupère les données (mêmes filtres que le tableau)
        var query = _db.AsQueryable().Where(p => p.Price >= 5);
        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        var data = query.Take(100).ToList(); // On limite à 100 pour l'exemple

        // 2. Génération du PDF avec QuestPDF
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(1, Unit.Centimetre);
                page.Header().Text("Rapport d'inventaire Produits").FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(50);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    // Entête du tableau PDF
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("ID");
                        header.Cell().Element(CellStyle).Text("Désignation");
                        header.Cell().Element(CellStyle).Text("Catégorie");
                        header.Cell().Element(CellStyle).Text("Prix");

                        static IContainer CellStyle(IContainer container) =>
                            container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    });

                    // Lignes de données
                    foreach (var item in data)
                    {
                        table.Cell().Element(ContentStyle).Text(item.Id.ToString());
                        table.Cell().Element(ContentStyle).Text(item.Name);
                        table.Cell().Element(ContentStyle).Text(item.Category);
                        table.Cell().Element(ContentStyle).Text(item.Price.ToString("C2"));

                        static IContainer ContentStyle(IContainer container) =>
                            container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    }
                });
            });
        });

        // 3. Envoi du fichier au navigateur
        byte[] pdfBytes = document.GeneratePdf();
        return File(pdfBytes, "application/pdf", $"Export_Produits_{DateTime.Now:yyyyMMdd}.pdf");
    }


}