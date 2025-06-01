namespace GeneCare.Models.DTO
{
    public class SampleDTO
    {
        private int sampleId;
        private BookingDTO booking;
        private DateTime date;
        private String sampleVariant;
        private String collectBy;
        private String deliveryMethod;
        private String status;

        public SampleDTO() { }
        public SampleDTO(int sampleId, BookingDTO booking, DateTime date, String sampleVariant, String collectBy, String deliveryMethod, String status)
        {
            this.sampleId = sampleId;
            this.booking = booking;
            this.date = date;
            this.sampleVariant = sampleVariant;
            this.collectBy = collectBy;
            this.deliveryMethod = deliveryMethod;
            this.status = status;
        }
        public int SampleId
        {
            get { return sampleId; }
            set { sampleId = value; }
        }
        public BookingDTO Booking
        {
            get { return booking; }
            set { booking = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public String SampleVariant
        {
            get { return sampleVariant; }
            set { sampleVariant = value; }
        }
        public String CollectBy
        {
            get { return collectBy; }
            set { collectBy = value; }
        }
        public String DeliveryMethod
        {
            get { return deliveryMethod; }
            set { deliveryMethod = value; }
        }
        public String Status
        {
            get { return status; }
            set { status = value; }

        }
    }
}
