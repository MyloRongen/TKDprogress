using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Enums;

namespace TKDprogress_DAL.Entities
{
    [Keyless]
    public class UserTul
    {
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public int TulId { get; set; }
        public Tul Tul { get; set; }

        public EnumStatus Status { get; set; }
    }
}
