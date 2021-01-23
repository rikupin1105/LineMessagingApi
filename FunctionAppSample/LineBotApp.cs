using Line.Messaging;
using Line.Messaging.Webhooks;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionAppSample
{
    class LineBotApp : WebhookApplication
    {
        private LineMessagingClient LineMessagingClient { get; set; }
        public LineBotApp(LineMessagingClient lineMessagingClient)
        {
            LineMessagingClient = lineMessagingClient;
        }
        protected override async Task OnMessageAsync(MessageEvent ev)
        {
            switch (ev.Message.Type)
            {
                case EventMessageType.Text:
                    break;
                case EventMessageType.Image:
                    break;
                case EventMessageType.Audio:
                    break;
                case EventMessageType.Video:
                    break;
                case EventMessageType.File:
                    break;
                case EventMessageType.Location:
                    break;
                case EventMessageType.Sticker:
                    break;
            }
        }
    }
}
