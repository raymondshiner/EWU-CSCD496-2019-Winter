namespace SecretSanta.Domain.Models
{
    public class Pairing : Entity
    {
        public User Recipient;
        public User Santa;
    } 
}