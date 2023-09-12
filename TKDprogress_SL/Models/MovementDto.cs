﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKDprogress_SL.Entities
{
    public class MovementDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
