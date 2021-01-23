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
                    //var channelSecret = "ChannelSecret";
                    //var LineMessagingClient = new LineMessagingClient("ChannelAccessToken");
                    var channelSecret = "249a38ce3e56f4d06eb03068d31260e1";
                    var LineMessagingClient = new LineMessagingClient("KkHD2SXj+QFf+F7rNcBro7z2W2ooimmiu/FWnrq/sJmwhOfxJKatqHHEuamTRTxXa77vG4RUouTmyI/cingpwHMIbZvth8FY30RpBtOT4J7xsImM+Cr+XexxbYhJhfLn0Qo8UzZOvc6HAlgxwddekgdB04t89/1O/w1cDnyilFU=");
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
