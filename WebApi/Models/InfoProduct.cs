using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InfoProduct
    {
        public InfoProduct()
        {

        }

        public int InfoProductID { get; set; }
        [Required, StringLength(50)]
        public String Label { get; set; }
        [Required, StringLength(100)]
        public String Description { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
