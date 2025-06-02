namespace GeneCare.Models.DTO
{
    public class TestResultDAO
    {
        private int resultId;
        private BookingDAO booking;
        private DateTime date;
        private String resultSummary;

        public TestResultDAO() { }
        public TestResultDAO(int resultId, BookingDAO booking, DateTime date, String resultSummary)
        {
            this.resultId = resultId;
            this.booking = booking;
            this.date = date;
            this.resultSummary = resultSummary;
        }
        public int ResultId
        {
            get { return resultId; }
            set { resultId = value; }
        }
        public BookingDAO Booking
        {
            get { return booking; }
            set { booking = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public String ResultSummary
        {
            get { return resultSummary; }
            set { resultSummary = value; }

        }
    }
}
