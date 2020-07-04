using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace NLayerArchitecture.Application.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}:{Id},{nameof(Name)}:{Name},{nameof(Price)}:{Price},{nameof(Stock)}:{Stock},{nameof(CategoryId)}:{CategoryId}";
        }
    }
}
