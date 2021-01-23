namespace Line.Messaging.Webhooks
{
    /// <summary>
    /// Message object which contains the text sent from the source.
    /// </summary>
    public class TextEventMessage : EventMessage
    {
        public string Text { get; }
        public Emoji[] Emojis { get; set; }
        public Mention Mention { get; set; }

        public TextEventMessage(string id, string text, Emoji[] emojis,Mention mention) : base(EventMessageType.Text, id)
        {
            Text = text;
            Emojis = emojis;
            Mention = mention;
        }
    }
}
