namespace chatik.Models
{
    public class Message
    {
        public string User {  get; set; }
        public string Text { get; set; }
        public Message(string user, string text)
        {
            User = user;
            Text = text;
        }
        public override string ToString()
        {
            return $"{User}:{Text}";
        }
    }
}
