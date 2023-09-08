using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKDprogress_DAL.Entities
{
    [Keyless]
    public class UserCategory
    {
        public int UserId { get; set; }
        public IdentityUser User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
