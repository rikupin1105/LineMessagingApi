# LINE Messaging API

## 目次
- [Usage](https://github.com/rikupin1105/LineMessagingApi/blob/main/readme1.md#usage)
    - [Installation](https://github.com/rikupin1105/LineMessagingApi/blob/main/readme1.md#installation)
- [API Referenve](https://github.com/rikupin1105/LineMessagingApi/blob/main/readme1.md#api-refarence)
    - [Create Message Objects](https://github.com/rikupin1105/LineMessagingApi/blob/main/readme1.md#create-message-objects)
    - [LineMessagingClient Class](https://github.com/rikupin1105/LineMessagingApi/blob/main/readme1.md#linemessagingclient-class)
        - [Reply](https://github.com/rikupin1105/LineMessagingApi/blob/main/readme1.md#reply)
        - [Push](https://github.com/rikupin1105/LineMessagingApi/blob/main/readme1.md#push)

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
|messages|`String`|○|
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
