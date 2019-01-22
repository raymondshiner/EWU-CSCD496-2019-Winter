namespace SecretSanta.Domain.Models
{
    public class Message : Entity
    {
        public User Sender { get; set; }
        public User Recipient { get; set; }
        public string Text { get; set; }
    }
}