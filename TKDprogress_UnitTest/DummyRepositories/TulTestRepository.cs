using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces.Repositories;
using TKDprogress_BLL.Models;

namespace TKDprogress_UnitTest.DummyRepositories
{
    public class TulTestRepository : ITulRepository
    {
        private List<Tul> tuls;

        public void InitializeTuls(List<Tul> newTuls)
        {
            tuls = newTuls;
        }

        public async Task<List<Tul>> GetTulsAsync(string searchString)
        {
            return tuls;
        }

        public async Task<Tul> CreateTulAsync(Tul newTul)
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

        public async Task<Tul> DeleteTulAsync(Tul tul)
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

        public async Task<Tul> UpdateTulAsync(Tul newTul)
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

        public Tul GetTulById(int id)
        {
            Tul tul = new();

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
