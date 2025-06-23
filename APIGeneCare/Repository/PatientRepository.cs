using APIGeneCare.Entities;
using APIGeneCare.Repository.Interface;

namespace APIGeneCare.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly GeneCareContext _context;
        public static int PAGE_SIZE { get; set; } = 10;
        public PatientRepository(GeneCareContext context) => _context = context;


    }
}
