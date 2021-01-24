# LINE Messaging API

[![NuGet](https://img.shields.io/nuget/v/Line.Messaging.rikupin.svg)](https://www.nuget.org/packages/Line.Messaging.rikupin)
[![NuGet](https://img.shields.io/nuget/dt/Line.Messaging.rikupin.svg)](https://www.nuget.org/packages/Line.Messaging.rikupin)

[LINE Messaging API](https://developers.line.biz/ja/services/messaging-api/) の C# 用 SDK 実装です。
[こちら](https://github.com/pierre3/LineMessagingApi)を勝手に引き継いだバージョンです。
自分に必要な機能を実装しているので、最新のAPIとは差があります。Issue PR は歓迎です。

### .Net Standard クラスライブラリ   
NuGet マネージャーなどでプロジェクトに参照可能です。

[NuGet ギャラリー | Line.Messaging](https://www.nuget.org/packages/Line.Messaging.rikupin/)  

# 利用方法
### インストール
```
dotnet add package Line.Messaging.rikupin --version 1.4.6.3
```
```
Install-Package Line.Messaging.rikupin -Version 1.4.6.3
```
以下の 3 ステップで SDK を利用します。
  - LineMessagingClient のインスタンス作成
  - WebhookApplication を継承したクラスの作成
  - 各イベント発生時のロジックを実装

## LineMessagingClient クラス

このクラスで LINE Messaging API プラットフォームと通信します。内部で HttpClient ベースの非同期通信を利用しており、以下のような機能を提供します。
```cs
Task ReplyMessageAsync(string replyToken, string message, bool notificationDisabled = false)
Task ReplyMessageAsync(string replyToken, IList<ISendMessage> messages, bool notificationDisabled = false)
Task ReplyMessageAsync(string replyToken, bool notificationDisabled = false, params string[] messages)
Task PushMessageAsync(string to, String message, bool notificationDisabled = false)
Task PushMessageAsync(string to, IList<ISendMessage> messages, bool notificationDisabled = false)
Task PushMessageAsync(string to, bool notificationDisabled = false, params string[] messages)
Task MultiCastMessageAsync(IList<string> to, IList<ISendMessage> messages, bool notificationDisabled = false)
Task MultiCastMessageAsync(IList<string> to, bool notificationDisabled = false, params string[] messages)
Task BroadCastMessageAsync(IList<ISendMessage> messages, bool notificationDisabled = false)
Task BroadCastMessageAsync(bool notificationDisabled = false, params string[] messages)
Task<ContentStream> GetContentStreamAsync(string messageId)
Task<UserProfile> GetUserProfileAsync(string userId)
Task<byte[]> GetContentBytesAsync(string messageId)
Task<UserProfile> GetGroupMemberProfileAsync(string groupId, string userId)
Task<UserProfile> GetRoomMemberProfileAsync(string roomId, string userId)
Task<IList<UserProfile>> GetGroupMemberProfilesAsync(string groupId)
Task<IList<UserProfile>> GetRoomMemberProfilesAsync(string roomId)
Task<GroupMemberIds> GetGroupMemberIdsAsync(string groupId, string continuationToken)
Task<GroupMemberIds> GetRoomMemberIdsAsync(string roomId, string continuationToken = null)
Task LeaveFromGroupAsync(string groupId)
Task LeaveFromRoomAsync(string roomId)
```

## メッセージオブジェクトの作成
[LINE Decelopers メッセージオブジェクト](https://developers.line.biz/ja/reference/messaging-api/#message-objects)

```cs
var messages = new ISendMessage[]
{
    new TextMessage("テキストメッセージ")
    new StickerMessage("パッケージID","スティッカーID")
    new ImageMessage("画像のURL","プレビュー画像のURL")
    new VideoMessage("動画のURL","プレビュー画像のURL")
    new AudioMessage("音声ファイルのURL",音声ファイルの長さ(ミリ秒))
    new LocationMessage("タイトル","住所",緯度,経度)
    new ImagemapMessage("画像のベースURL","代替テキスト",new ImagemapSize(基本画像の幅,高さ),アクションオブジェクト)
    new TemplateMessage("代替テキスト",ボタン、確認、カルーセル、または画像カルーセルオブジェクト)
    new FlexMessage("代替テキスト")
    {
       Contents = FlexMessageのコンテナ
    }
};
```



## Webhook イベントのパース
GetWebhookEventsAsync 拡張メソッドを呼び出して、要求から LINE イベントを取得できます。例) [FunctionAppSample/HttpTriggerFunction.cs](https://github.com/rikupin1105/LineMessagingApi/blob/master/FunctionAppSample/HttpTriggerFunction.cs) 

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
    [FunctionName("FreedomBot")]
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
## Webhook イベントのハンドル
WebhookApplication を継承したクラスを作成し、各種イベント発生時のロジックを実装します。

```cs
public abstract class WebhookApplication
{
  protected virtual Task OnMessageAsync(MessageEvent ev);
  protected virtual Task OnJoinAsync(JoinEvent ev);
  protected virtual Task OnLeaveAsync(LeaveEvent ev);
  protected virtual Task OnFollowAsync(FollowEvent ev);
  protected virtual Task OnUnfollowAsync(UnfollowEvent ev);
  protected virtual Task OnBeaconAsync(BeaconEvent ev);
  protected virtual Task OnPostbackAsync(PostbackEvent ev);
}

```

最後に作成したクラスのインスタンスを作成し、RunAsync メソッドに対してパースした LINE イベントを渡します。

例) [Line.Messaging/Webhooks/WebhookApplication.cs](https://github.com/rikupin1105/LineMessagingApi/blob/master/Line.Messaging/Webhooks/WebhookApplication.cs) 




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
