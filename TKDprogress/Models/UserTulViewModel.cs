using TKDprogress_BLL.Enums;

namespace TKDprogress.Models
{
    public class UserTulViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EnumStatus Status { get; set; }
        public string StatusText { get; set; }
    }
}
