using Microsoft.EntityFrameworkCore;
using mvcCatalog.Data;

namespace mvcCatalog.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
        {
            if (context.Categories.Any()) return;
            if (context.ProductsFromSuppliers.Any()) return;


            var p1 = new Product
            {
                Image = "123",
                ProductName = "iPhone 14",
                ProductDescription = "Lorem Ipsum is simply dummy text of the printing and" +
                                     " typesetting industry. Lorem Ipsum has been the industry's" +
                                     " standard dummy text ever since the 1500s.",
                Features = "features",
                
                Year = 2022,
                Manufacturer = "Apple"
            };
            var s1 = new Supplier
            {
                SupplierName = "Alta"
            };
            var s2 = new Supplier
            {
                SupplierName = "Smog"
            };
            var s3 = new Supplier
            {
                SupplierName = "On Line"
            };

            var pfs1 = new ProductFromSupplier
            {
                Price = (decimal)1100.99,
                Product = p1,
                Supplier = s1,
                IsDiscount = false
            };
            var pfs2 = new ProductFromSupplier
            {
                Price = (decimal)1580.99,
                Product = p1,
                Supplier = s2,
                IsDiscount = false,
            };
            var pfs3 = new ProductFromSupplier
            {
                Price = (decimal)1069.99,
                Product = p1,
                Supplier = s3,
                IsDiscount = true,
                NewPrice = (decimal)1199.99,
            };

            context.ProductsFromSuppliers.AddRange(pfs1, pfs2, pfs3);

            var p2 = new Product
            {
                Image = "123",
                ProductName = "iPad Pro M1",
                ProductDescription = "Lorem Ipsum is simply dummy text of the printing and" +
                                     " typesetting industry. Lorem Ipsum has been the industry's" +
                                     " standard dummy text ever since the 1500s.",
                Features = "features",
                Year = 2022,
                Manufacturer = "Apple"
            };
            var p3 = new Product
            {
                Image = "123",
                ProductName = "iPhone 13",
                ProductDescription = "Lorem Ipsum is simply dummy text of the printing and" +
                                     " typesetting industry. Lorem Ipsum has been the industry's" +
                                     " standard dummy text ever since the 1500s.",
                Features = "features",
                Year = 2021,
                Manufacturer = "Apple"
            };
            var p4 = new Product
            {
                Image = "123",
                ProductName = "iPhone 12",
                ProductDescription = "Lorem Ipsum is simply dummy text of the printing and" +
                                     " typesetting industry. Lorem Ipsum has been the industry's" +
                                     " standard dummy text ever since the 1500s.",
                Features = "features",
                Year = 2020,
                Manufacturer = "Apple"
            };
            var p5 = new Product
            {
                Image = "123",
                ProductName = "iPhone 12 Pro",
                ProductDescription = "Lorem Ipsum is simply dummy text of the printing and" +
                                     " typesetting industry. Lorem Ipsum has been the industry's" +
                                     " standard dummy text ever since the 1500s.",
                Features = "features",
                Year = 2020,
                Manufacturer = "Apple"
            };
            var p6 = new Product
            {
                Image = "123",
                ProductName = "Galaxy S10",
                ProductDescription = "Lorem Ipsum is simply dummy text of the printing and" +
                                     " typesetting industry. Lorem Ipsum has been the industry's" +
                                     " standard dummy text ever since the 1500s.",
                Features = "features",
                Year = 2023,
                Manufacturer = "Samsung"
            };

            var p7 = new Product
            {
                Image = "123",
                ProductName = "Vichy Lipstick",
                ProductDescription = "Lorem Ipsum is simply dummy text of the printing and" +
                                     " typesetting industry. Lorem Ipsum has been the industry's" +
                                     " standard dummy text ever since the 1500s.",
                Features = "features",
                Year = 2022,
                Manufacturer = "Vichy"
            };

            var c1 = new Category
            {
                CategoryName = "Telephones",
                Image = "123",
                Description = "root",
                Products = new List<Product> { p1, p3, p4, p5, p6 }
            };
            var c2 = new Category
            {
                CategoryName = "Tablets",
                Image = "123",
                Description = "root",
                Products = new List<Product> { p2 }
            };
            var c3 = new Category
            {
                CategoryName = "Notebooks",
                Image = "123",
                Description = "root",
                Products = new List<Product>()
            };
            var c4 = new Category
            {
                CategoryName = "Accessories",
                Image = "123",
                Description = "root",
                Products = new List<Product>()
            };
            var c5 = new Category
            {
                CategoryName = "Network Appliance",
                Image = "123",
                Description = "root",
                Products = new List<Product>()
            };
            var c6 = new Category
            {
                CategoryName = "Data Storage",
                Image = "123",
                Description = "root",
                Products = new List<Product>()
            };

            var c8 = new Category
            {
                CategoryName = "Audio Techniques",
                Image = "123",
                Description = "root",
                Products = new List<Product>()
            };
            var c9 = new Category
            {
                CategoryName = "Video Games",
                Image = "123",
                Description = "root",
                Products = new List<Product>()
            };
            var c10 = new Category
            {
                CategoryName = "Photo",
                Image = "123",
                Description = "root",
                Products = new List<Product>()
            };
            var c11 = new Category
            {
                CategoryName = "Television",
                Image = "123",
                Description = "root",
                Products = new List<Product>()
            };
            var c12 = new Category
            {
                CategoryName = "Cosmetics",
                Image = "123",
                Description = "root",
                Products = new List<Product> { p7 }
            };


            context.Categories.AddRange(
                new Category
                {
                    CategoryName = "Others",
                    Image = "123",
                    Description = "root",
                    ChildrenCategories = new List<Category> { c12 }
                },
                new Category
                {
                    CategoryName = "Electronics",
                    Image = "123",
                    Description = "root",
                    ChildrenCategories = new List<Category> { c1, c2, c3, c4, c5, c6, c8, c9, c10, c11 }
                },
                new Category
                {
                    CategoryName = "Auto",
                    Image = "123",
                    Description = "root",
                    ChildrenCategories = new List<Category>()
                },
                new Category
                {
                    CategoryName = "Building",
                    Image = "123",
                    Description = "root",
                    ChildrenCategories = new List<Category>()
                },
                new Category
                {
                    CategoryName = "Home",
                    Image = "123",
                    Description = "root",
                    ChildrenCategories = new List<Category>()
                },
                new Category
                {
                    CategoryName = "Appliances",
                    Image = "123",
                    Description = "root",
                    ChildrenCategories = new List<Category>()
                },
                new Category
                {
                    CategoryName = "Work and Office",
                    Image = "123",
                    Description = "root",
                    ChildrenCategories = new List<Category>()
                }
            );
            context.SaveChanges();
        }
    }
}