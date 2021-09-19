namespace Line.Messaging.Webhooks
{
    /// <summary>
    /// Image event message
    /// </summary>
    public class ImageEventMessage : EventMessage
    {
        /// <summary>
        /// ContentProvider
        /// </summary>
        public ContentProvider ContentProvider { get; }
        public ImageSet ImageSet {  get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Event Message Type</param>
        /// <param name="id">Message ID</param>
        /// <param name="contentProvider">ContentProvider Object</param>
        /// <param name="imageSet">ImageSet</param>
        public ImageEventMessage(EventMessageType type, string id, ContentProvider contentProvider = null, ImageSet imageSet = null) : base(type, id)
        {
            ContentProvider = contentProvider;
            ImageSet = imageSet;
        }
    }
}
