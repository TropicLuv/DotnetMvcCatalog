using System.ComponentModel.DataAnnotations;

namespace mvcCatalog.Models;

public class Image
{
    public int Id { get; set; }
    [StringLength(160)] public string Url { get; set; }
}