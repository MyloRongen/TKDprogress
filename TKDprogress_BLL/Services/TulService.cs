﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Interfaces;

namespace TKDprogress_BLL.Services
{
    public class TulService : ITulService
    {
        private readonly ITulRepository _tulRepository;

        public TulService(ITulRepository tulRepository)
        {
            _tulRepository = tulRepository;
        }

        public async Task<List<TulDto>> GetTulsAsync(string searchString)
        {
            List<TulDto> tuls = await _tulRepository.GetTulsAsync(searchString);

            return tuls;
        }
    }
}
