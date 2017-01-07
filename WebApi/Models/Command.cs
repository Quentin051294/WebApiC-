using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Command
    {
        public Command()
        {

        }

        public int CommandID { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
