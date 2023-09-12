using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKDprogress_SL.Entities;
using TKDprogress_SL.Interfaces;

namespace TKDprogress_UnitTest.DummyRepositories
{
    public class TerminologyTestRepository : ITerminologyRepository
    {
        private List<TerminologyDto> terminologies;

        public void InitializeTerminologies(List<TerminologyDto> newTerminologies)
        {
            terminologies = newTerminologies;
        }

        public async Task<List<TerminologyDto>> GetTerminologiesAsync(string searchString)
        {
            return terminologies;
        }

        public async Task<TerminologyDto> GetTerminologyByIdAsync(int id)
        {
            TerminologyDto terminology = new();

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

        public async Task<TerminologyDto> CreateTerminologyAsync(TerminologyDto terminology)
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

        public async Task<TerminologyDto> DeleteTerminologyAsync(TerminologyDto terminology)
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

        public async Task<TerminologyDto> UpdateTerminologyAsync(TerminologyDto newTerminology)
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
