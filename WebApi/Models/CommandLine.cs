using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CommandLine
    {
        public CommandLine()
        {

        }

        public int CommandLineID { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public double RealPrice { get; set; }


        public int CommandId { get; set; }
        public Command Command { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
