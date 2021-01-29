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
            //メッセージイベント
            //リプライ可能
            //https://developers.line.biz/ja/reference/messaging-api/#message-event
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
        protected override async Task OnUnsendAsync(UnsendEvent ev) 
        {
            //送信取り消しイベント
        }
        protected override async Task OnJoinAsync(JoinEvent ev) 
        {
            //参加イベント
            //リプライ可能
            //https://developers.line.biz/ja/reference/messaging-api/#join-event
        }

        protected override async Task OnLeaveAsync(LeaveEvent ev) 
        {
            //メンバー退出イベント
            //https://developers.line.biz/ja/reference/messaging-api/#member-left-event
        }

        protected override async Task OnFollowAsync(FollowEvent ev) 
        {
            //フォローイベント
            //リプライ可能
            //https://developers.line.biz/ja/reference/messaging-api/#follow-event
        }

        protected override async Task OnUnfollowAsync(UnfollowEvent ev) 
        {
            //フォロー解除イベント
            //https://developers.line.biz/ja/reference/messaging-api/#unfollow-event
        }

        protected override async Task OnVideoPlayCompleteAsync(VideoPlayCompleteEvent ev) 
        {
            //動画視聴完了イベント
            //リプライ可能
            //https://developers.line.biz/ja/reference/messaging-api/#video-viewing-complete
        }

        protected override async Task OnBeaconAsync(BeaconEvent ev) 
        {
            //ビーコンイベント
            //リプライ可能
            //https://developers.line.biz/ja/reference/messaging-api/#beacon-event
        }

        protected override async Task OnPostbackAsync(PostbackEvent ev) 
        {
            //ポストバックイベント
            //リプライ可能
            //https://developers.line.biz/ja/reference/messaging-api/#postback-event
        }

        protected override async Task OnAccountLinkAsync(AccountLinkEvent ev) 
        {
            //アカウント連携イベント
            //リプライ可能
            //https://developers.line.biz/ja/reference/messaging-api/#account-link-event
        }

        protected override async Task OnMemberJoinAsync(MemberJoinEvent ev) 
        {
            //メンバー参加イベント
            //リプライ可能
            //https://developers.line.biz/ja/reference/messaging-api/#member-joined-event
        }

        protected override async Task OnMemberLeaveAsync(MemberLeaveEvent ev) 
        {
            //メンバー退出イベント
            //https://developers.line.biz/ja/reference/messaging-api/#member-left-event
        }

        protected override async Task OnDeviceLinkAsync(DeviceLinkEvent ev) 
        {
            //デバイス連携イベント
            //リプライ可能
            //https://developers.line.biz/ja/reference/messaging-api/#device-link-event
        }

        protected override async Task OnDeviceUnlinkAsync(DeviceUnlinkEvent ev) 
        {
            //デバイス連携解除イベント
            //リプライ可能
            //https://developers.line.biz/ja/reference/messaging-api/#device-unlink-event
        }
    }
}
