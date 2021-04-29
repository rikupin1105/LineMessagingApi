# This repository is not used.
# The new repository is [here](https://github.com/rikupin1105/line-bot-sdk-csharp)

[![NuGet](https://img.shields.io/nuget/v/Line.Messaging.rikupin.svg)](https://www.nuget.org/packages/Line.Messaging.rikupin)
[![NuGet](https://img.shields.io/nuget/dt/Line.Messaging.rikupin.svg)](https://www.nuget.org/packages/Line.Messaging.rikupin)  
This is a C# implementation of the [LINE Messaging API](https://developers.line.biz/ja/reference/messaging-api/).
## 目次
- [Installation](https://github.com/rikupin1105/LineMessagingApi/blob/main/README.md#installation)
- [Usage](https://github.com/rikupin1105/LineMessagingApi/blob/main/README.md#usage)
- [API Refarence](https://github.com/rikupin1105/LineMessagingApi/blob/main/README.md#api-refarence)
    - [Create Message Objects](https://github.com/rikupin1105/LineMessagingApi/blob/main/README.md#create-message-objects)
    - [LineMessagingClient Class](https://github.com/rikupin1105/LineMessagingApi/blob/main/README.md#linemessagingclient-class)
        - [Reply](https://github.com/rikupin1105/LineMessagingApi/blob/main/README.md#reply)
        - [Push](https://github.com/rikupin1105/LineMessagingApi/blob/main/README.md#push)
    - [Parse Webhook-Events](https://github.com/rikupin1105/LineMessagingApi#parse-webhook-events)
    - [Process Webhook-events](https://github.com/rikupin1105/LineMessagingApi#process-webhook-events)


# Installation
```
dotnet add package Line.Messaging.rikupin
```
```
Install-Package Line.Messaging.rikupin
```

# Usage
### Initialize
```cs
var LineMessagingClient = new LineMessagingClient("ChannelAccessToken");
```

# API Refarence
## Create Message objects
```cs
var messages = new IList<IsendMessage>[]
{
    //Here is the message object
    //Up to 5.
}
```
### Message object
```cs
new TextMessage(Text, QuickReply, MessageSender)
new StickerMessage(packageId, stickerId, QuickReply, MessageSender)
new ImageMessage(originalContentUrl, previerImageUrl, QuickReply, MessageSender)
new VideoMessage(originalContentUrl, previerImageUrl, trackingId, QuickReply, MessageSender)
new AudioMessage(originalContentUrl, duration, QuickReply, MessageSender)
new LocationMessage(title, address, latitude, longitude, quickReply, messageSender)
new ImagemapMessage(baseUrl, altText, baseSize, actions, quickReply, Video, messageSender)
new TemplateMessage(altText, template, quickReply, messageSender)
new FlexMessage(altText)
{
    Contents = contents
}
```



## LineMessagingClient Class
## Reply
### ReplyMessageAsync
```cs 
ReplyMessageAsync(replyToken, messages, notificationDisabled)
```
|Param|Type|required|Description|
|---|---|---|---|
|replytoken| `String`|○|`replyToken` received via webhook.|
|messages|`Message Object[]`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
---
### ReplyTextAsync
```cs 
ReplyTextAsync(replyToken, message, notificationDisabled, quickReply, messageSender)
```
|Param|Type|required|Description|
|---|---|---|---|
|replytoken| `String`|○|`replyToken` received via webhook.|
|message|`String`|○|Text message|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||

Example:
```cs
await LineMessagingClient.ReplyTextAsync(replytoken, "Helloworld");
```
---
### ReplyStickerAsync
```cs
ReplyStickerAsync(replyToken, packageId, stickerId, notificationDisabled, quickReply, messageSender)
```
|Param|Type|required|Description|
|---|---|---|---|
|replytoken| `String`|○|`replyToken` received via webhook.|
|packageId|`String`|○|
|stickerId|`String`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---
### ReplyImageAsync
```cs
ReplyImageAsync(replyToken, originalContentUrl, previewImageUrl, notificationDisabled, quickReply, messageSender)
```
|Param|Type|required|Description|
|---|---|---|---|
|replytoken| `String`|○|`replyToken` received via webhook.|
|originalContentUrl|`String`|○|
|previewImageUrl|`String`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---
### ReplyVideoAsync
```cs
ReplyVideoAsync(replyToken, originalContentUrl, previewImageUrl, trackingId, notificationDisabled, quickReply, messageSender)
```
|Param|Type|required|Description|
|---|---|---|---|
|replytoken| `String`|○|`replyToken` received via webhook.|
|originalContentUrl|`String`|○|
|previewImageUrl|`String`|○|
|trackingId|`String`||
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---
### ReplyAudioAsync
```cs
ReplyAudioAsync(replyToken, originalContentUrl, duration, notificationDisabled, quickReply, messageSender)
```
|Param|Type|required|Description|
|---|---|---|---|
|replytoken| `String`|○|`replyToken` received via webhook.|
|originalContentUrl|`String`|○|
|duration|`long`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---
### ReplyLocationAsync
```cs
 ReplyLocationAsync(replyToken, title, address, latitude, longitude, notificationDisabled, quickReply, messageSender)
 ```
|Param|Type|required|Description|
|---|---|---|---|
|replytoken| `String`|○|
|title|`String`|○|
|address|`String`|○|
|latitude|`Decimal`|○|
|longitude|`Decimal`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---

## Push
### PushMessageAsync
```cs
PushMessageAsync(to, messages, notificationDisabled)
```
|Param|Type|required|Description|
|---|---|---|---|
|to| `String`|○||
|messages|`String`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
---

### PushTextAsync
```cs
PushTextAsync(to, message, notificationDisabled, quickReply, messageSender)
```
|Param|Type|required|Description|
|---|---|---|---|
|to| `String`|○|
|message|`String`|○|Text message|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||

Example:
```cs
await LineMessagingClient.PushTextAsync(to, "Helloworld");
```
---
### PushStickerAsync
```cs
PushStickerAsync(to, packageId, stickerId, notificationDisabled, quickReply, messageSender)
```
|Param|Type|required|Description|
|---|---|---|---|
|to| `String`|○|
|packageId|`String`|○|
|stickerId|`String`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---

### PushImageAsync
```cs
PushImageAsync(to, originalContentUrl, previewImageUrl, notificationDisabled, quickReply, messageSender)
```
|Param|Type|required|Description|
|---|---|---|---|
|to| `String`|○|
|originalContentUrl|`String`|○|
|previewImageUrl|`String`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---

### PushVideoAsync
```cs
PushVideoAsync(to, originalContentUrl, previewImageUrl, trackingId, notificationDisabled, quickReply, messageSender)
```
|Param|Type|required|Description|
|---|---|---|---|
|to| `String`|○|
|originalContentUrl|`String`|○|
|previewImageUrl|`String`|○|
|trackingId|`String`||
|notificationDisabled|`bool`|`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---

### PushAudioAsync
```cs
PushAudioAsync(to, originalContentUrl, duration, notificationDisabled, quickReply, messageSender)
```
|Param|Type|required|Description|
|---|---|---|---|
|to| `String`|○|
|originalContentUrl|`String`|○|
|duration|`long`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---

### PushLocationAsync
```cs
PushLocationAsync(to, title, address, latitude, longitude, notificationDisabled, quickReply, messageSender)
```
|Param|Type|required|Description|
|---|---|---|---|
|to| `String`|○|
|title|`String`|○|
|address|`String`|○|
|latitude|`Decimal`|○|
|longitude|`Decimal`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||

## Parse Webhook-Events
Use GetWebhookEventsAsync extension method for incoming request to parse the LINE events from the LINE platform. 
```cs
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
    [FunctionName("LineBot")]
    public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequestMessage req, ILogger log)
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
```

## Process Webhook-events
Create a class which inherits WebhookApplication class, then overrides the method you want to handle the LINE evnet in your class.
```cs
public abstract class WebhookApplication
{
    protected virtual Task OnMessageAsync(MessageEvent ev);
    protected virtual Task OnUnsendAsync(UnsendEvent ev);
    protected virtual Task OnJoinAsync(JoinEvent ev);
    protected virtual Task OnLeaveAsync(LeaveEvent ev);
    protected virtual Task OnFollowAsync(FollowEvent ev);
    protected virtual Task OnUnfollowAsync(UnfollowEvent ev);
    protected virtual Task OnVideoPlayCompleteAsync(VideoPlayCompleteEvent ev);
    protected virtual Task OnBeaconAsync(BeaconEvent ev);
    protected virtual Task OnPostbackAsync(PostbackEvent ev);
    protected virtual Task OnAccountLinkAsync(AccountLinkEvent ev);
    protected virtual Task OnMemberJoinAsync(MemberJoinEvent ev);
    protected virtual Task OnMemberLeaveAsync(MemberLeaveEvent ev);
    protected virtual Task OnDeviceLinkAsync(DeviceLinkEvent ev);
    protected virtual Task OnDeviceUnlinkAsync(DeviceUnlinkEvent ev);
}
```
Finally, instantiate the class and run RunAsync method by giving the parsed LINE events as shown above.

```cs
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
```
