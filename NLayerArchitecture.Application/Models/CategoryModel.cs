using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerArchitecture.Application.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductModel> Products { get; set; }
        public CategoryModel()
        {
            Products = new HashSet<ProductModel>();
        }

        public override string ToString()
        {
            return $"{nameof(Id)}:{Id},{nameof(Name)}:{Name},{nameof(Description)}:{Description}";
        }
    }
}
