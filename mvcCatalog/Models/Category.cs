using System.ComponentModel.DataAnnotations;
using mvcCatalog.Data_Annotations;

namespace mvcCatalog.Models;

public class Category
{
    public int CategoryId { get; set; }

    // [StringLength(60)] [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")] [Required] public string CategoryName { get; set; }
    [Required] public string CategoryName { get; set; }

    [StringLength(100)] [Required] public string Image { get; set; }

    [StringLength(160)] [Required] public string Description { get; set; }

    public ICollection<Product>? Products { get; set; }
    public IEnumerable<Category>? ChildrenCategories { get; set; }

    [Display(Name = "ParentCategoryId")] public int? ParentCategoryId { get; set; }

    public Category? ParentCategory { get; set; }
}