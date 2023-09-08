using TKDprogress_SL.Enums;

namespace TKDprogress.Models
{
    public class UserCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EnumStatus Status { get; set; }
        public string StatusText { get; set; }
    }
}
