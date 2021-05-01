namespace Domain.Models
{
    public class Email
    {
        public int EmailId { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
    }
}
