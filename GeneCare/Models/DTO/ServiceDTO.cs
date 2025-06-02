namespace GeneCare.Models.DTO
{
    public class ServiceDAO
    {
        private int serviceId;
        private String serviceName;
        private String serviceType;
        private String description;

        public ServiceDAO() { }
        public ServiceDAO(int serviceId, String serviceName, String serviceType, String description)
        {
            this.serviceId = serviceId;
            this.serviceName = serviceName;
            this.serviceType = serviceType;
            this.description = description;
        }
        public int ServiceId
        {
            get { return serviceId; }
            set { serviceId = value; }
        }
        public String ServiceName
        {
            get { return serviceName; }
            set { serviceName = value; }
        }
        public String ServiceType
        {
            get { return serviceType; }
            set { serviceType = value; }
        }
        public String Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
