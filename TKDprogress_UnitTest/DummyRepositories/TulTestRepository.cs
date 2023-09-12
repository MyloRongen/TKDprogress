using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Interfaces;

namespace TKDprogress_UnitTest.DummyRepositories
{
    public class TulTestRepository : ITulRepository
    {
        private List<TulDto> tuls;

        public void InitializeTuls(List<TulDto> newTuls)
        {
            tuls = newTuls;
        }

        public async Task<List<TulDto>> GetTulsAsync(string searchString)
        {
            return tuls;
        }

        public async Task<TulDto> CreateTulAsync(TulDto newTul)
        {
            foreach (var item in tuls)
            {
                if (newTul.Id == item.Id)
                {
                    throw new Exception("This tul already exists!");
                }
            }

            tuls.Add(newTul);
            return newTul;
        }

        public async Task<TulDto> DeleteTulAsync(TulDto tul)
        {
            foreach (var item in tuls)
            {
                if (tul.Id == item.Id)
                {
                    tuls.Remove(item);
                    break;
                }
            }

            return tul;
        }

        public async Task<TulDto> UpdateTulAsync(TulDto newTul)
        {
            for (int i = 0; i < tuls.Count; i++)
            {
                if (newTul.Id == tuls[i].Id)
                {
                    tuls[i] = newTul;
                    break;
                }
            }

            return newTul;
        }

        public TulDto GetTulById(int id)
        {
            TulDto tul = new();

            foreach (var item in tuls)
            {
                if (item.Id == id)
                {
                    tul = item;
                    break;
                }
            }

            return tul;
        }
    }
}
