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
            //���b�Z�[�W�C�x���g
            //���v���C�\
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
            //���M�������C�x���g
        }
        protected override async Task OnJoinAsync(JoinEvent ev) 
        {
            //�Q���C�x���g
            //���v���C�\
            //https://developers.line.biz/ja/reference/messaging-api/#join-event
        }

        protected override async Task OnLeaveAsync(LeaveEvent ev) 
        {
            //�����o�[�ޏo�C�x���g
            //https://developers.line.biz/ja/reference/messaging-api/#member-left-event
        }

        protected override async Task OnFollowAsync(FollowEvent ev) 
        {
            //�t�H���[�C�x���g
            //���v���C�\
            //https://developers.line.biz/ja/reference/messaging-api/#follow-event
        }

        protected override async Task OnUnfollowAsync(UnfollowEvent ev) 
        {
            //�t�H���[�����C�x���g
            //https://developers.line.biz/ja/reference/messaging-api/#unfollow-event
        }

        protected override async Task OnVideoPlayCompleteAsync(VideoPlayCompleteEvent ev) 
        {
            //���掋�������C�x���g
            //���v���C�\
            //https://developers.line.biz/ja/reference/messaging-api/#video-viewing-complete
        }

        protected override async Task OnBeaconAsync(BeaconEvent ev) 
        {
            //�r�[�R���C�x���g
            //���v���C�\
            //https://developers.line.biz/ja/reference/messaging-api/#beacon-event
        }

        protected override async Task OnPostbackAsync(PostbackEvent ev) 
        {
            //�|�X�g�o�b�N�C�x���g
            //���v���C�\
            //https://developers.line.biz/ja/reference/messaging-api/#postback-event
        }

        protected override async Task OnAccountLinkAsync(AccountLinkEvent ev) 
        {
            //�A�J�E���g�A�g�C�x���g
            //���v���C�\
            //https://developers.line.biz/ja/reference/messaging-api/#account-link-event
        }

        protected override async Task OnMemberJoinAsync(MemberJoinEvent ev) 
        {
            //�����o�[�Q���C�x���g
            //���v���C�\
            //https://developers.line.biz/ja/reference/messaging-api/#member-joined-event
        }

        protected override async Task OnMemberLeaveAsync(MemberLeaveEvent ev) 
        {
            //�����o�[�ޏo�C�x���g
            //https://developers.line.biz/ja/reference/messaging-api/#member-left-event
        }

        protected override async Task OnDeviceLinkAsync(DeviceLinkEvent ev) 
        {
            //�f�o�C�X�A�g�C�x���g
            //���v���C�\
            //https://developers.line.biz/ja/reference/messaging-api/#device-link-event
        }

        protected override async Task OnDeviceUnlinkAsync(DeviceUnlinkEvent ev) 
        {
            //�f�o�C�X�A�g�����C�x���g
            //���v���C�\
            //https://developers.line.biz/ja/reference/messaging-api/#device-unlink-event
        }
    }
}
