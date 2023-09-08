using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity.EntityFramework;
using TKDprogress_SL.Enums;

namespace TKDprogress_SL.Entities
{
    [Keyless]
    public class UserCategoryDto
    {
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }

        public EnumStatus Status { get; set; }
    }
}
