using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IProcessStepRepository
    {
        IEnumerable<ProcessStep> GetAllProcessStepsPaging(string? typeSearch, string? search, string? sortBy, int? page);
        ProcessStep? GetProcessStepById(int id);
        bool CreateProcessStep(ProcessStep processStep);
        bool UpdateProcessStep(ProcessStep processStep);
        bool DeleteProcessStepById(int id);
    }
}
