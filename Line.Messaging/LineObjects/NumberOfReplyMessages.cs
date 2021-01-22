namespace Line.Messaging
{
    /// <summary>
    /// https://developers.line.biz/ja/reference/messaging-api/#get-number-of-reply-messages
    /// </summary>
    public class NumberOfReplyMessages
    {
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public int Value { get; set; }
    }
}
