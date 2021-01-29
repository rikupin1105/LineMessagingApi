# LINE Messaging API
## 目次
---
<br>
<br>
<br>

## インストール
---
```
dotnet add package Line.Messaging.rikupin
```
or
```
Install-Package Line.Messaging.rikupin
```

## API Reference
---
### リプライメッセージ
## `ReplyMessageAsync(replytoken, message, option)`
|Param|Type|Description|
|---|---|---|
|replytoken| `String`|WebHookより受け取った `replyToken`|
|message|`String`|テキストメッセージ|

<br>

|option|Type|Description|
|---|---|---|
|notificationDisabled|`bool`|`true` メッセージ送信時に、ユーザーに通知されない。

<br>

サンプル
```cs
await LineMessagingClient.ReplyMessageAsync(replytoken, "Helloworld")
```
<br>

## `ReplyMessageAsync(replytoken, messages, option)`

|Param|Type|Description|
|---|---|---|
|replytoken| `String`|WebHookより受け取った `replyToken`|
|message|`IList<ISendMessage>`|メッセージオブジェクトの配列|

<br>

|option|Type|Description|
|---|---|---|
|notificationDisabled|`bool`|`true` メッセージ送信時に、ユーザーに通知されない。

<br>

サンプル
```cs
await LineMessagingClient.ReplyMessageAsync(replytoken, messages)
```