using System.Data;
using GeneCare.Models.Utils;
using Microsoft.Data.SqlClient;

namespace GeneCare.Models
{
    public class ServicePrice
    {
        private int _priceId;
        private Service _service;
        private Duration _duration;
        private Double _price;

        #region Properties
        public ServicePrice()
        {
            _service = new Service();
            _duration = new Duration();
        }
        public ServicePrice(int priceId, Service service, Duration duration, Double price)
        {
            _priceId = priceId;
            _service = service;
            _duration = duration;
            _price = price;
        }
        #endregion
        #region Getters and Setters
        public int PriceId
        {
            get { return _priceId; }
            set { _priceId = value; }
        }
        public Service Service
        {
            get { return _service; }
            set { _service = value; }
        }
        public Duration Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        public Double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        #endregion
        #region DAO
        public ServicePrice? getServicePrice(int? priceId, int? serviceId, int? durationId)
        {
            using (SqlConnection con = new DBUtils().getConnection())
            {
                var query = "SELECT * FROM ServicePrices WHERE PriceID = @PriceId OR ServiceID = @ServiceId OR DurationID = @DurationId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@PriceId", SqlDbType.Int).Value = priceId;
                    cmd.Parameters.Add("@ServiceId", SqlDbType.Int).Value = serviceId;
                    cmd.Parameters.Add("@DurationId", SqlDbType.Int).Value = durationId;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _priceId = reader.GetInt32("PriceID");
                            _service.ServiceId = reader.GetInt32("ServiceID");
                            _duration.DurationId = reader.GetInt32("DurationID");
                            _price = reader.GetDouble("Price");
                            return this;
                        }
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
