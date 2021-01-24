namespace Line.Messaging.Webhooks
{
    /// <summary>
    /// Webhook Event Type
    /// </summary>
    public enum WebhookEventType
    {
        Message,
        Unsend,
        Follow,
        Unfollow,
        Join,
        Leave,
        Postback,
        Beacon,
        AccountLink,
        MemberJoined,
        MemberLeft,
        Things
    }
}
