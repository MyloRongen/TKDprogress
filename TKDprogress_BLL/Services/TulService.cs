using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces;
using TKDprogress_BLL.Models;
using TKDprogress_BLL.Interfaces.Repositories;
using TKDprogress_BLL.Interfaces.Services;

namespace TKDprogress_BLL.Services
{
    public class TulService : ITulService
    {
        private readonly ITulRepository _tulRepository;

        public TulService(ITulRepository tulRepository)
        {
            _tulRepository = tulRepository;
        }

        public async Task<List<Tul>> GetTulsAsync(string searchString)
        {
            List<Tul> tuls = await _tulRepository.GetTulsAsync(searchString);

            return tuls;
        }

        public async Task<Tul> CreateTulAsync(Tul newTul)
        {
            if (string.IsNullOrEmpty(newTul.Name) || string.IsNullOrEmpty(newTul.Description))
            {
                newTul.ErrorMessage = "Tul name or description has an incorrect input.";
                return newTul;
            }

            Tul tul =  await _tulRepository.CreateTulAsync(newTul);

            return tul;
        }

        public async Task<Tul> UpdateTulAsync(Tul newTul)
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

            Tul tul = await _tulRepository.UpdateTulAsync(newTul);

            return tul;
        }

        public async Task<Tul> DeleteTulAsync(Tul tul)
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
