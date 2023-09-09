using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKDprogress_SL.Entities
{
    public class TulMovementDto
    {
        public int Id { get; set; }

        public int TulId { get; set; }
        public TulDto Tul { get; set; }

        public int MovementId { get; set; }
        public MovementDto Movement { get; set; }

        public int Order { get; set; }
    }
}
