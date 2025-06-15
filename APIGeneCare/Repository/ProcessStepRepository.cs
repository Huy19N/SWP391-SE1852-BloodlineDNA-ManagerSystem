using APIGeneCare.Data;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class ProcessStepRepository : IProcessStepRepository
    {
        public bool CreateProcessStep(ProcessStep processStep)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProcessStepById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProcessStep> GetAllProcessStepsPaging(string? typeSearch, string? search, string? sortBy, int? page)
        {
            throw new NotImplementedException();
        }

        public ProcessStep? GetProcessStepById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProcessStep(ProcessStep processStep)
        {
            throw new NotImplementedException();
        }
    }
}
