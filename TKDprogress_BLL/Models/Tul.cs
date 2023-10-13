using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKDprogress_BLL.Models
{
    public class Tul
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }

        public List<Movement> Movements { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
