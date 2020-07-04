using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerArchitecture.Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int Stock { get; set; }
        public decimal Price{ get; set; }
        public string BarCode { get; set; }
        public static Product Create(int productId,int categoryId,
                                        string name,decimal price,int stock)
        {
            return new Product()
            {
                Id = productId,
                CategoryId = categoryId,
                Name = name,
                Price = price,
                Stock = stock,
                BarCode=GetBarCode(),
                CreatedDate=DateTime.Now,
            };
        }
        private static string GetBarCode() {
            return Guid.NewGuid().ToString();
        }
    }
}
