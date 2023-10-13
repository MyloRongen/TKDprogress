using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_BLL.Interfaces.Repositories;
using TKDprogress_BLL.Models;

namespace TKDprogress_UnitTest.DummyRepositories
{
    public class TerminologyTestRepository : ITerminologyRepository
    {
        private List<Terminology> terminologies;

        public void InitializeTerminologies(List<Terminology> newTerminologies)
        {
            terminologies = newTerminologies;
        }

        public async Task<List<Terminology>> GetTerminologiesAsync(string searchString)
        {
            return terminologies;
        }

        public async Task<Terminology> GetTerminologyByIdAsync(int id)
        {
            Terminology terminology = new();

            foreach (var item in terminologies)
            {
                if (item.Id == id)
                {
                    terminology = item;
                    break;
                }
            }

            return terminology;
        }

        public async Task<Terminology> CreateTerminologyAsync(Terminology terminology)
        {
            foreach (var item in terminologies)
            {
                if (terminology.Id == item.Id)
                {
                    throw new Exception("This terminology already exists!");
                }
            }

            terminologies.Add(terminology);
            return terminology;
        }

        public async Task<Terminology> DeleteTerminologyAsync(Terminology terminology)
        {
            foreach (var item in terminologies)
            {
                if (terminology.Id == item.Id)
                {
                    terminologies.Remove(item);
                    break;
                }
            }

            return terminology;
        }

        public async Task<Terminology> UpdateTerminologyAsync(Terminology newTerminology)
        {
            for (int i = 0; i < terminologies.Count; i++)
            {
                if (newTerminology.Id == terminologies[i].Id)
                {
                    terminologies[i] = newTerminology;
                    break;
                }
            }

            return newTerminology;
        }
    }
}
