// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface ISampleRepository
    {
        IEnumerable<Sample> GetAllSamplesPaging(string? typeSearch, string? search, string? sortBy, int? page);
        Sample? GetSampleById(int id);
        bool CreateSample(Sample sample);
        bool UpdateSample(Sample sample);
        bool DeleteSampleById(int id);
    }
}
