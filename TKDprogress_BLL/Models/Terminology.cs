using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Enums;

namespace TKDprogress_BLL.Models
{
    public class Terminology
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Meaning { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
