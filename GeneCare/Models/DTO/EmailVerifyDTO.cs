namespace GeneCare.Models.DTO
{
    public class EmailVerifyDTO
    {
        private string UUID;
        private string email;
        private DateTime date;

        public EmailVerifyDTO() { }
        public EmailVerifyDTO(string UUID, string email, DateTime date)
        {
            this.UUID = UUID;
            this.email = email;
            this.date = date;
        }

        public string getUUID() { return UUID; }
        public string getEmail() { return email; }
        public DateTime getDate() { return date; }

        public void setUUID(string UUID) {  this.UUID = UUID; }
        public void setEmail(string Email) { email = Email; }
        public void setDate(DateTime Date) { date = Date; }


    }
}
