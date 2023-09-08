using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TKDprogress_DAL.Entities
{
    public class TulMovement
    {
        public int Id { get; set; }

        public int TulId { get; set; }
        public Tul Tul { get; set; }

        public int MovementId { get; set; }
        public Movement Movement { get; set; }

        public int Order { get; set; }
    }
}
