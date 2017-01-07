using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Customer
    {
        public Customer()
        {

        }

        public int CustomerID { get; set; }
        [Required, StringLength(100)]
        public String Email { get; set; }
        [Required, StringLength(50)]
        public String Password { get; set; }
        [Required, StringLength(30)]
        public String FirstName { get; set; }
        [Required, StringLength(30)]
        public String Name { get; set; }
        [MaxLength(10)]
        public String PhoneNumber { get; set; }
        [Required, StringLength(50)]
        public String Rue { get; set; }
        [Required]
        public int CodePostal { get; set; }
        [Required, StringLength(50)]
        public String Localite { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
