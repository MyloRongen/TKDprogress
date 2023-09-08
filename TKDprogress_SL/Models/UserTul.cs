using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TKDprogress_SL.Entities
{
    [Keyless]
    public class UserTul
    {
        public int UserId { get; set; }
        public IdentityUser User { get; set; }

        public int TulId { get; set; }
        public Tul Tul { get; set; }
    }
}
