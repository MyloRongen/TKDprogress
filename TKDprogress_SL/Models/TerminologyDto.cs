using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Enums;

namespace TKDprogress_SL.Entities
{
    public class TerminologyDto
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Meaning { get; set; }

        public EnumStatus Status { get; set; }
    }
}
