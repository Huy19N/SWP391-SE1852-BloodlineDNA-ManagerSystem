namespace GeneCare.Models.DTO
{
    public class ServicePriceDAO
    {
        private int priceId;
        private ServiceDAO service;
        private DurationDAO duration;
        private double price;

        public ServicePriceDAO() { }
        public ServicePriceDAO(int priceId, ServiceDAO service, DurationDAO duration, double price)
        {
            this.priceId = priceId;
            this.service = service;
            this.duration = duration;
            this.price = price;
        }
        public int PriceId
        {
            get { return priceId; }
            set { priceId = value; }
        }
        public ServiceDAO Service
        {
            get { return service; }
            set { service = value; }
        }
        public DurationDAO Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

    }
}
