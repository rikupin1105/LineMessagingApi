namespace Line.Messaging
{
    public class Mention
    {
        public Mentionees[] Mentionees { get; set; }
    }
    public class Mentionees
    {
        public int Index { get; set; }
        public int Length { get; set; }
        public string UserId { get; set; }
    }
}
