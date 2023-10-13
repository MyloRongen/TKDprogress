using TKDprogress_BLL.Models;
using TKDprogress_BLL.Enums;

namespace TKDprogress.Models
{
    public class TulViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public EnumStatus? Status { get; set; }

        public List<Movement>? Movements { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
