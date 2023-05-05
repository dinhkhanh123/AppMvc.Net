using System.Collections.Generic;
using DoAnLapTrinhWebNC.Models;

namespace DoAnLapTrinhWebNC.Service
{
    public class ProductService : List<ProductModel>
    {
        public ProductService()// pt khoi tao
        {
            this.AddRange(new ProductModel[] {
                new ProductModel() {Id = 1, ProductName = "San pham 1",Price = 1000},
                new ProductModel() {Id = 2, ProductName = "San pham 2",Price = 2000},
                new ProductModel() {Id = 3, ProductName = "San pham 3",Price = 3000},
                new ProductModel() {Id = 4, ProductName = "San pham 4",Price = 4000},

            });
        }
    }
}