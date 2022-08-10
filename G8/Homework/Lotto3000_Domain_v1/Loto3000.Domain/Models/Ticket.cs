using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Domain.Models
{
    public class Ticket
    {
        public Ticket() { }
        public Ticket(int id, User user, int[] nums)
        {
            Combination comb = new Combination();

            TicketId = id;
            User = user;
            //CombinationNumbers = comb.Combinations(nums);
        }
        public int TicketId { get; set; }
        //public IList<Combination> CombinationNumbers { get; set; } = new List<Combination>();
        public User User { get; set; }
        public Draw? Draw { get; set; }
        public Session Session { get; set; }    
    }
}