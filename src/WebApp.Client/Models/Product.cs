namespace WebApp.Client.Models;

using System.ComponentModel.DataAnnotations;

public class Product : IEntity<int>
{
    [Display(Name = "N°")]
    public int Id { get; set; }

    [Display(Name = "Désignation")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Catégorie")]
    public string Category { get; set; } = string.Empty;

    [Display(Name = "Prix (€)")]
    public decimal Price { get; set; }

    public string Description { get; set; } = string.Empty;

    [Display(Name = "Date Création")]
    public DateTime CreatedAt { get; set; }
}