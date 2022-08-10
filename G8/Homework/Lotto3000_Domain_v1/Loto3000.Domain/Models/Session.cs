using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Domain.Models
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime Start { get; set; } 
        public DateTime End { get; set; }
    }
}
