using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Product
    {
        public Product()
        {

        }

        public int ProductID { get; set; }
        [Required]
        public double UnitPrice { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
