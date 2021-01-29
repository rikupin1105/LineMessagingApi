# LINE Messaging API
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
## Reply

## ReplyMessageAsync(replyToken, messages, notificationDisabled)
|Param|Type|required|Description|
|---|---|---|---|
|replytoken| `String`|○|`replyToken` received via webhook.|
|messages|`String`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
---

## ReplyTextAsync(replyToken, message, notificationDisabled, quickReply, messageSender)
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

## ReplyStickerAsync(replyToken, packageId, stickerId, notificationDisabled, quickReply, messageSender)
|Param|Type|required|Description|
|---|---|---|---|
|replytoken| `String`|○|`replyToken` received via webhook.|
|packageId|`String`|○|
|stickerId|`String`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---

## ReplyImageAsync(replyToken, originalContentUrl, previewImageUrl, notificationDisabled, quickReply, messageSender)
|Param|Type|required|Description|
|---|---|---|---|
|replytoken| `String`|○|`replyToken` received via webhook.|
|originalContentUrl|`String`|○|
|previewImageUrl|`String`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---

## ReplyVideoAsync(replyToken, originalContentUrl, previewImageUrl, trackingId, notificationDisabled, quickReply, messageSender)
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

## ReplyAudioAsync(replyToken, originalContentUrl, duration, notificationDisabled, quickReply, messageSender)
|Param|Type|required|Description|
|---|---|---|---|
|replytoken| `String`|○|`replyToken` received via webhook.|
|originalContentUrl|`String`|○|
|duration|`long`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---

## ReplyLocationAsync(replyToken, title, address, latitude, longitude, notificationDisabled, quickReply, messageSender)
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
## PushMessageAsync(to, messages, notificationDisabled)
|Param|Type|required|Description|
|---|---|---|---|
|to| `String`|○||
|messages|`String`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
---

## PushTextAsync(to, message, notificationDisabled, quickReply, messageSender)
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

## PushStickerAsync(to, packageId, stickerId, notificationDisabled = false, quickReply = null, messageSender = null)
|Param|Type|required|Description|
|---|---|---|---|
|to| `String`|○|
|packageId|`String`|○|
|stickerId|`String`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---

## PushImageAsync(to, originalContentUrl, previewImageUrl, notificationDisabled = false, quickReply = null, messageSender = null)
|Param|Type|required|Description|
|---|---|---|---|
|to| `String`|○|
|originalContentUrl|`String`|○|
|previewImageUrl|`String`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---

## PushVideoAsync(to, originalContentUrl, previewImageUrl, trackingId, notificationDisabled = false, quickReply = null, messageSender = null)
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

## PushAudioAsync(to, originalContentUrl, duration, notificationDisabled = false, quickReply = null, messageSender = null)
|Param|Type|required|Description|
|---|---|---|---|
|to| `String`|○|
|originalContentUrl|`String`|○|
|duration|`long`|○|
|notificationDisabled|`bool`||`true` The user is not notified when a message is sent.|
|quickReply|`QuickReply`||
|messageSender|`MessageSender`||
---

## PushLocationAsync(to, title, address, latitude, longitude, notificationDisabled = false, quickReply = null, messageSender = null)
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
