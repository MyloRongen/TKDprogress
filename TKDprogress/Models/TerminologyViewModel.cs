using TKDprogress_SL.Enums;

namespace TKDprogress.Models
{
    public class TerminologyViewModel
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Meaning { get; set; }

        public EnumStatus Status { get; set; }
    }
}
