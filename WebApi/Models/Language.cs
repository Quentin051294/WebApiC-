using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Language
    {
        public Language()
        {

        }

        public int LanguageID { get; set; }
        [Required, StringLength(30)]
        public String Name { get; set; }


    }
}
