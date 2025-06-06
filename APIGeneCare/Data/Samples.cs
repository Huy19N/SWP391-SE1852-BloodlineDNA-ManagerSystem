namespace APIGeneCare.Data
{
    public class Samples
    {
        public int SampleID { get; set; }
        public int BookingID { get; set; }
        public DateTime Date { get; set; }
        public String SampleVariant { get; set; } = string.Empty;
        public String CollectedBy { get; set; } = string.Empty;
        public String DeliveryMethod { get; set; } = string.Empty;
        public String Status { get; set; } = string.Empty;
    }
}
