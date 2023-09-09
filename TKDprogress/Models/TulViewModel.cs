using TKDprogress_SL.Entities;
using TKDprogress_SL.Enums;

namespace TKDprogress.Models
{
    public class TulViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public EnumStatus? Status { get; set; }

        public List<MovementDto>? Movements { get; set; }
    }
}
