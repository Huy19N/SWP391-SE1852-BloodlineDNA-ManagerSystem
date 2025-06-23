// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using APIGeneCare.Entities;
using APIGeneCare.Model.DTO;

namespace APIGeneCare.Repository.Interface
{
    public interface ISampleRepository
    {
        IEnumerable<SampleDTO> GetAllSamplesPaging(string? typeSearch, string? search, string? sortBy, int? page);
        SampleDTO? GetSampleById(int id);
        bool CreateSample(SampleDTO sample);
        bool UpdateSample(SampleDTO sample);
        bool DeleteSampleById(int id);
    }
}
