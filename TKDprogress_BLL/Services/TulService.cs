using System;
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

        public async Task<TulDto> CreateTulAsync(TulDto newTul)
        {
            if (string.IsNullOrEmpty(newTul.Name) || string.IsNullOrEmpty(newTul.Description))
            {
                newTul.ErrorMessage = "Tul name or description has an incorrect input.";
                return newTul;
            }

            TulDto tul =  await _tulRepository.CreateTulAsync(newTul);

            return tul;
        }

        public async Task<TulDto> UpdateTulAsync(TulDto newTul)
        {
            if (newTul.Id <= 0)
            {
                newTul.ErrorMessage = "Tul type does not exist.";
                return newTul;
            }

            if (string.IsNullOrEmpty(newTul.Name) || string.IsNullOrEmpty(newTul.Description))
            {
                newTul.ErrorMessage = "Tul name or description has an incorrect input.";
                return newTul;
            }

            TulDto tul = await _tulRepository.UpdateTulAsync(newTul);

            return tul;
        }

        public async Task<TulDto> DeleteTulAsync(TulDto tul)
        {
            if (tul.Id <= 0)
            {
                tul.ErrorMessage = "Tul type does not exist.";
                return tul;
            }

            await _tulRepository.DeleteTulAsync(tul);

            return tul;
        }
    }
}
