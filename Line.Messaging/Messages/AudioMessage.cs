namespace Line.Messaging
{
    /// <summary>
    /// Audio
    /// https://developers.line.me/en/docs/messaging-api/reference/#audio
    /// </summary>
    public class AudioMessage : ISendMessage
    {
        public MessageType Type { get; } = MessageType.Audio;

        /// <summary>
        /// These properties are used for the quick reply feature
        /// </summary>
        public QuickReply QuickReply { get; set; }
        public MessageSender Sender { get; set; }

        /// <summary>
        /// URL of audio file (Max: 1000 characters)
        /// HTTPS
        /// m4a
        /// Less than 1 minute
        /// Max 10 MB
        /// </summary>
        public string OriginalContentUrl { get; }

        /// <summary>
        /// Length of audio file (milliseconds)
        /// </summary>
        public long Duration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="originalContentUrl">
        /// URL of audio file (Max: 1000 characters)
        /// HTTPS
        /// m4a
        /// Less than 1 minute
        /// Max 10 MB
        /// </param>
        /// <param name="duration">
        /// Length of audio file (milliseconds)
        /// </param>
        /// <param name="quickReply">
        /// QuickReply
        /// </param>
        public AudioMessage(string originalContentUrl, long duration, QuickReply quickReply = null, MessageSender messageSender = null)
        {
            OriginalContentUrl = originalContentUrl;
            Duration = duration;
            QuickReply = quickReply;
            Sender = messageSender;
        }
    }
}
