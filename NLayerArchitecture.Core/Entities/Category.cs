using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NLayerArchitecture.Core.Entities
{
    public class Category:BaseEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public string   Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; private set; }
    }
}
