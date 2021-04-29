using Line.Messaging;
using Line.Messaging.Webhooks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FunctionAppSample
{
    public static class HttpTriggerFunction
    {
        [FunctionName("FreedomBot")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req, ILogger log)
        {
            {
                try
                {
                    log.LogInformation(req.Content.ReadAsStringAsync().Result);
                    var channelSecret = "ChannelSecret";
                    var LineMessagingClient = new LineMessagingClient("ChannelAccessToken");
                    var events = await req.GetWebhookEventsAsync(channelSecret);

                    var app = new LineBotApp(LineMessagingClient);

                    await app.RunAsync(events);

                }
                catch (InvalidSignatureException e)
                {
                    return req.CreateResponse(HttpStatusCode.Forbidden, new { e.Message });
                }

                return req.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
