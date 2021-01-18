namespace Line.Messaging.Webhooks
{
    /// <summary>
    /// Message object which contains the text sent from the source.
    /// </summary>
    public class TextEventMessage : EventMessage
    {
        public string Text { get; }
        public Emoji[] Emojis { get; set; }

        public TextEventMessage(string id, string text, Emoji[] emojis) : base(EventMessageType.Text, id)
        {
            Text = text;
            Emojis = emojis;
        }
    }
}
