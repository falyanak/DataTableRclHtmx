using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core; // Pour OrderBy(string)
using DataTable.Rcl.Models;
using WebApp.Client.Models;

public partial class ProductController : Controller
{
   private static readonly Random _random = new Random();

    // Simulation 1M lignes avec dates aléatoires
    private static readonly List<Product> _db = Enumerable.Range(1, 1000000)
        .Select(i => {
            // Génère une date aléatoire entre il y a 730 jours et aujourd'hui
            DateTime startDate = DateTime.Now.AddDays(-730);
            int range = (DateTime.Now - startDate).Days;
            DateTime randomDate = startDate.AddDays(_random.Next(range))
                                           .AddHours(_random.Next(24))
                                           .AddMinutes(_random.Next(60));

            return new Product { 
                Id = i, 
                Name = $"Produit {i}", 
                Category = i % 2 == 0 ? "Tech" : "Bio", 
                // Prix entre 5.00€ et 1005.00€
                Price = 5.0m + (decimal)(_random.NextDouble() * 1000),
                Description = $"Description du produit {i}.",
                CreatedAt = randomDate // Injection de la date
            };
        }).ToList();

    public IActionResult Index() => View();

    public IActionResult Details(int id)
    {
        var product = _db.FirstOrDefault(x => x.Id == id);
        if (product == null) return NotFound();

        return View("/Views/Product/Details/Details.cshtml", product);
    }


    [HttpGet]
public IActionResult GetTable(string search, string sortBy, bool isAsc = true, int page = 1, int pageSize = 10)
{
    var query = _db.AsQueryable();
    
    // Filtrage recherche
    if (!string.IsNullOrEmpty(search))
        query = query.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

    // Tri dynamique (System.Linq.Dynamic.Core)
    if (!string.IsNullOrEmpty(sortBy))
        query = query.OrderBy($"{sortBy} {(isAsc ? "ascending" : "descending")}");

    var totalRecords = query.Count();
    var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

    var viewModel = new GenericTableViewModel<dynamic, int> {
        Items = items.Cast<dynamic>().ToList(),
        Columns = new List<string> { "Id", "Name", "Category", "Price", "CreatedAt" },
        CurrentPage = page,
        PageSize = pageSize,
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize),
        BaseApiUrl = "/Product/GetTable",
        DetailsUrl = "/Product/Details/",
        DeleteApiUrl = "/Product/Delete/",
        ExportApiUrl = "/Product/ExportPdf",
        TargetId = "#smart-table-wrapper",
        Search = search,
        SortBy = sortBy,
        IsAsc = isAsc
    };

    return PartialView("_SmartTable", viewModel);
}

    // --- LA MÉTHODE MANQUANTE ---
    [HttpGet]
    public IActionResult GetTabContent(int id, string tab)
    {
        var product = _db.FirstOrDefault(x => x.Id == id);
        if (product == null) return NotFound();

        // On utilise ToLower() pour éviter les erreurs de casse (Info vs info)
        return tab?.ToLower() switch
        {
            "history" => PartialView("/Views/Product/Details/_TabHistory.cshtml", product),
            "info"    => PartialView("/Views/Product/Details/_TabInfo.cshtml", product),
            _         => PartialView("/Views/Product/Details/_TabInfo.cshtml", product)
        };
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var item = _db.FirstOrDefault(x => x.Id == id);
        if (item != null) _db.Remove(item);
        return Ok();
    }
}