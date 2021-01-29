# LINE Messaging API
## 目次
---
<br>
<br>
<br>

# Installation
```
dotnet add package Line.Messaging.rikupin
```
or
```
Install-Package Line.Messaging.rikupin
```

# Usage
### Initialize
```cs
var LineMessagingClient = new LineMessagingClient("ChannelAccessToken");
```

# Message objects
### Text Message
|Param|Type|Description|
|---|---|---|
|type| `String`|`text`|
|text|`String`|Text of Message|

<br>

|Option pram|Type|Description|
|---|---|---|
|emojis|Array of LINE emoji objects|One or more LINE emojis
|emojis.index|`Int`|The position of $ in the text property, with 0 being the beginning of the text.
|emojis.productId|`String`|Product ID indicating a set of LINE emojis.
|emojis.emojiId|`String`|The ID of the LINE emoji in the product ID.

<br>
---

### Sticker Message
|Param|Type|Description|
|---|---|---|
|type| `String`|`sticker`|
|packageId|`String`|Package ID of the sticker set.|
|stickerId|`String`|Sticker ID

<br>

---

### Image Message
|Param|Type|Description|
|---|---|---|
|type| `String`|`image`|
|originalContentUrl|`String`|Image URL|
|previewImageUrl|`String`|URL of the preview Image

<br>

---

### Video Message
|Param|Type|Description|
|---|---|---|
|type| `String`|`video`|
|originalContentUrl|`String`|Video URL|
|previewImageUrl|`String`|URL of the preview Image|

<br>

|Option pram|Type|Description|
|---|---|---|
|trackingId|`String`|ID to identify the video when the video viewing completion event occurs.

<br>

---

### Audio Message
|Param|Type|Description|
|---|---|---|
|type| `String`|`audio`|
|originalContentUrl|`String`|Audio URL|
|duration|`int`|Length of the audio file|

<br>

---

### Location Message
|Param|Type|Description|
|---|---|---|
|type| `String`|`location`|
|title|`String`|Title|
|address|`string`|Address|
|latitude|`Decimal`|Latitude
|longitude|`Decimal`|Longitude

<br>

---

### ImageMap Message
|Param|Type|Description|
|---|---|---|
|type| `String`|`imagemap`|
|baseUrl|`String`|Base URL of the image|
|altText|`string`|Alternative text|
|baseSize.width|`Int`|Width of the basic image
|baseSize.height|`Int`|The height of the basic image when its width is 1040px.
|action|Array of ImageMap Action Objects|

<br>

|Option pram|Type|Description|
|---|---|---|
|video.previewImageUrl|`String`|URL of preview image
|video.area.x|`Int`|
|video.area.y|`Int`|
|video.area.width|`Int`|
|video.area.height|`Int`|
|video.externalLink.linkUri|`String`|
|video.externalLink.label|`String`|

<br>
<br>

# API Refarence
## Reply
## `ReplyMessageAsync(replytoken, message, option)`
|Param|Type|Description|
|---|---|---|
|replytoken| `String`|`replyToken` received via webhook.|
|message|`String`|Text message|

<br>

|Option pram|Type|Description|
|---|---|---|
|notificationDisabled|`bool`|`true` The user is not notified when a message is sent.

Example:
```cs
await LineMessagingClient.ReplyMessageAsync(replytoken, "Helloworld")
```
<br>

## `ReplyMessageAsync(replytoken, messages, option)`

|Param|Type|Description|
|---|---|---|
|replytoken| `String`|`replyToken` received via webhook.|
|message|`IList<ISendMessage>`|List of objects which contains the contents of the message to be sent.|

|Option pram|Type|Description|
|---|---|---|
|notificationDisabled|`bool`|`true` The user is not notified when a message is sent.

Example:
```cs
await LineMessagingClient.ReplyMessageAsync(replytoken, messages)
```
---

## Push
## `PushMessageAsync(to, message, option)`
|Param|Type|Description|
|---|---|---|
|to| `String`|ID of the destination|
|message|`String`|Text message|

<br>

|Option pram|Type|Description|
|---|---|---|
|notificationDisabled|`bool`|`true` The user is not notified when a message is sent.

Example:
```cs
await LineMessagingClient.PushMessageAsync(to, "Helloworld")
```
<br>

## `PushMessageAsync(to, messages, option)`

|Param|Type|Description|
|---|---|---|
|to| `String`|ID of the destination|
|message|`IList<ISendMessage>`|List of objects which contains the contents of the message to be sent.|

|Option pram|Type|Description|
|---|---|---|
|notificationDisabled|`bool`|`true` The user is not notified when a message is sent.

Example:
```cs
await LineMessagingClient.PushMessageAsync(to, messages)
```