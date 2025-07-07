namespace APIGeneCare.Entities;

public partial class Sample
{
    public int SampleId { get; set; }

    public string? SampleName { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
