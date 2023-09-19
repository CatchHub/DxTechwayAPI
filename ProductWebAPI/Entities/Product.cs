using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductAppAPI.Entities;

namespace ProductListApp.Domain.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string Detail { get; set; }
        public string Brand { get; set; }
        public string CurrencyCode { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Product()
        {
            CreateDate = DateTime.Now;
        }
    }
}
