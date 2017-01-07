using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InfoCategory
    {
        public InfoCategory()
        {

        }

        public int InfoCategoryID { get; set; }
        [Required, StringLength(50)]
        public String CategoryName { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
