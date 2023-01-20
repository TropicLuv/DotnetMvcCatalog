using Microsoft.EntityFrameworkCore;
using mvcCatalog.Models;

namespace mvcCatalog.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<ProductFromSupplier> ProductsFromSuppliers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductFromSupplier>()
            .HasKey(pfs => new { pfs.ProductId, pfs.SupplierId });

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);


        // modelBuilder.Entity<ProductFromSupplier>()
        //     .HasOne(pfs => pfs.Product)
        //     .WithMany(p => p.ProductFromSuppliers)
        //     .HasForeignKey(pfs => pfs.ProductId).OnDelete(DeleteBehavior.NoAction);
        // modelBuilder.Entity<ProductFromSupplier>()
        //     .HasOne(pfs => pfs.Supplier)
        //     .WithMany(s => s.ProductsFromSupplier)
        //     .HasForeignKey(pfs => pfs.SupplierId).OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Category>().HasMany(c => c.ChildrenCategories)
            .WithOne(cc => cc.ParentCategory)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        // modelBuilder.Entity<Product>().HasMany(p => p.AlbumOfImages)
        //     .WithOne()
        //     .OnDelete(DeleteBehavior.SetNull);
    }
}