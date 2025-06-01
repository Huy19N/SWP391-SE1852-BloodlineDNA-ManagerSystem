using System.Runtime.CompilerServices;

namespace GeneCare.Models.DTO
{
    public class DurationDAO
    {
        private int durationId;
        private String durationName;
        private int time;

        public DurationDAO() { }
        public DurationDAO(int durationId, String durationName, int time)
        {
            this.durationId = durationId;
            this.durationName = durationName;
            this.time = time;
        }
        public int DurationId
        {
            get { return durationId; }
            set { durationId = value; }
        }
        public String DurationName
        {
            get { return durationName; }
            set { durationName = value; }
        }
        public int Time
        {
            get { return time; }
            set { time = value; }

        }
    }
}
