using System.ComponentModel.DataAnnotations;

namespace mvcCatalog.Models;

public class Category
{
    public int CategoryId { get; set; }

    [StringLength(60)] [Required] public string CategoryName { get; set; }

    [StringLength(100)] [Required] public string Image { get; set; }

    [StringLength(160)] [Required] public string Description { get; set; }

    //TODO - check how it is going to display in MSSQL
    public ICollection<Product>? Products { get; set; }
    public IEnumerable<Category>? ChildrenCategories { get; set; }

    [Display(Name = "ParentCategoryId")] public int? ParentCategoryId { get; set; }

    public Category? ParentCategory { get; set; }
}