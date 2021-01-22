﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging
{
    /// <summary>
    /// LINE Messaging API client, which handles request/response to LINE server.
    /// </summary>
    public interface ILineMessagingClient
    {
        #region Message 

        /// <summary>
        /// 応答メッセージを送る。
        /// Send response messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-reply-message
        /// </summary>
        /// <param name="replyToken">ReplyToken</param>
        /// <param name="messages">Reply messages. Up to 5 messages.</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        Task ReplyMessageAsync(string replyToken, IList<ISendMessage> messages, bool notificationDisabled = false);

        /// <summary>
        /// 応答メッセージを送る。
        /// Send response messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-reply-message
        /// </summary>
        /// <param name="replyToken">ReplyToken</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Reply Text messages. Up to 5 messages.</param>
        Task ReplyMessageAsync(string replyToken, bool notificationDisabled = false, params string[] messages);

        /// <summary>
        /// 応答メッセージを送る。
        /// Send response messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-reply-message
        /// </summary>
        /// <param name="replyToken">ReplyToken</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Set reply messages with Json string.</param>
        Task ReplyMessageWithJsonAsync(string replyToken, bool notificationDisabled = false, params string[] messages);

        /// <summary>
        /// 応答メッセージを送る。
        /// Send response messages.
        /// /// https://developers.line.biz/ja/reference/messaging-api/#send-push-message
        /// </summary>
        /// <param name="to">ID of the receiver</param>
        /// <param name="messages">Reply messages. Up to 5 messages.</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        Task PushMessageAsync(string to, IList<ISendMessage> messages, bool notificationDisabled = false);

        /// <summary>
        /// プッシュメッセージを送る。
        /// Send push messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-push-message
        /// </summary>
        /// <param name="to">ID of the receiver</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Set reply messages with Json string.</param>
        Task PushMessageWithJsonAsync(string to, bool notificationDisabled = false, params string[] messages);


        /// <summary>
        /// プッシュメッセージを送る。
        /// Send push messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-push-message
        /// </summary>
        /// <param name="to">ID of the receiver</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Reply text messages. Up to 5 messages.</param>
        Task PushMessageAsync(string to, bool notificationDisabled = false, params string[] messages);

        /// <summary>
        /// マルチキャストメッセージを送る。
        /// Send multicast messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-multicast-message
        /// </summary>
        /// <param name="to">IDs of the receivers. Max: 500 users</param>
        /// <param name="messages">Reply messages. Up to 5 messages.</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        Task MultiCastMessageAsync(IList<string> to, IList<ISendMessage> messages, bool notificationDisabled = false);

        /// <summary>
        /// マルチキャストメッセージを送る。
        /// Send multicast messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-multicast-message
        /// </summary>
        /// <param name="to">IDs of the receivers. Max: 500 users</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Set reply messages with Json string.</param>
        Task MultiCastMessageWithJsonAsync(IList<string> to, bool notificationDisabled = false, params string[] messages);

        /// <summary>
        /// マルチキャストメッセージを送る。
        /// Send multicast messages.\
        /// https://developers.line.biz/ja/reference/messaging-api/#send-multicast-messages
        /// </summary>
        /// <param name="to">IDs of the receivers. Max: 500 users</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Reply text messages. Up to 5 messages.</param>
        Task MultiCastMessageAsync(IList<string> to, bool notificationDisabled = false, params string[] messages);

        /// <summary>
        /// ナローキャストメッセージの進行状況を取得する
        /// Get the progress of a narrowcast message.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-narrowcast-progress-status
        /// </summary>
        /// <param name="requestId"></param>
        Task<ProgressNarrowcast> GetProgressNarrowcastAsync(string requestId);

        /// <summary>
        /// ブロードキャストキャストメッセージを送る。
        /// Send broadcast messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-multicast-messages
        /// </summary>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Reply text messages. Up to 5 messages.</param>
        Task BroadCastMessageAsync(IList<ISendMessage> messages, bool notificationDisabled = false);

        /// <summary>
        /// ブロードキャストキャストメッセージを送る。
        /// Send broadcast messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-multicast-messages
        /// </summary>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Reply text messages. Up to 5 messages.</param>
        Task BroadCastMessageAsync( bool notificationDisabled = false, params string[] messages);

        /// <summary>
        /// コンテンツを取得する
        /// Retrieving contents
        /// https://developers.line.biz/ja/reference/messaging-api/#get-content
        /// </summary>
        /// <param name="messageId">Message ID</param>
        /// <returns>Content as ContentStream</returns>
        Task<ContentStream> GetContentStreamAsync(string messageId);

        /// <summary>
        /// コンテンツを取得する
        /// Retrieving contents
        /// https://developers.line.biz/ja/reference/messaging-api/#get-content
        /// </summary>
        /// <param name="messageId">Message ID</param>
        /// <returns>Content as byte array</returns>
        Task<byte[]> GetContentBytesAsync(string messageId);

        /// <summary>
        /// 当月分の上限目安を取得します。
        /// Get the upper limit guideline for the current month.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-quota
        /// </summary>
        Task<Quota> GetQuota();

        /// <summary>
        /// 当月に送信されたメッセージの数を取得します。
        /// Gets the number of messages sent in the current month.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-consumption
        /// </summary>
        Task<Consumption> GetConsumption();

        /// <summary>
        /// 送信済みの応答メッセージの数を取得する。
        /// Get the number of response messages that have been sent.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-number-of-reply-messages
        /// </summary>
        Task<NumberOfMessages> GetNumberOfReplyMessages(DateTime date);

        /// <summary>
        /// 送信済みのプッシュメッセージの数を取得する。
        /// Get the number of push messages that have been sent.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-number-of-push-messages
        /// </summary>
        Task<NumberOfMessages> GetNumberOfPushMessages(DateTime date);

        /// <summary>
        /// 送信済みのマルチキャストメッセージの数を取得する。
        /// Get the number of multicast messages that have been sent.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-number-of-multicast-messages
        /// </summary>
        Task<NumberOfMessages> GetNumberOfMulticastMessages(DateTime date);

        /// <summary>
        /// 送信済みのブロードキャストメッセージの数を取得する。
        /// Get the number of broadcast messages that have been sent.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-number-of-broadcast-messages
        /// </summary>
        Task<NumberOfMessages> GetNumberOfBroadcastMessages(DateTime date);


        #endregion

        #region Profile

        /// <summary>
        /// Get user profile information.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-profile
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        Task<UserProfile> GetUserProfileAsync(string userId);

        #endregion

        #region Group

        /// <summary>
        /// Gets the user profile of a member of a group that the bot is in. This includes user profiles of users who have not added the bot as a friend or have blocked the bot.
        /// Use the group ID and user ID returned in the source object of webhook event objects. Do not use the LINE ID used in the LINE app. 
        /// https://developers.line.biz/ja/reference/messaging-api/#get-group-member-profile
        /// </summary>
        /// <param name="groupId">Identifier of the group</param>
        /// <param name="userId">Identifier of the user</param>
        /// <returns>User Profile</returns>
        Task<UserProfile> GetGroupMemberProfileAsync(string groupId, string userId);

        /// <summary>
        /// Gets the user IDs of the members of a group that the bot is in. This includes the user IDs of users who have not added the bot as a friend or has blocked the bot.
        /// This feature is only available for LINE@ Approved accounts or official accounts.
        /// Use the group Id returned in the source object of webhook event objects. 
        /// Users who have not agreed to the Official Accounts Terms of Use are not included in memberIds. There is no fixed number of memberIds. 
        /// https://developers.line.biz/ja/reference/messaging-api/#get-group-member-user-ids
        /// </summary>
        /// <param name="groupId">Identifier of the group</param>
        /// <param name="continuationToken">ContinuationToken</param>
        /// <returns>GroupMemberIds</returns>
        Task<GroupMemberIds> GetGroupMemberIdsAsync(string groupId, string continuationToken);

        /// <summary>
        /// Gets the user profiles of the members of a group that the bot is in. This includes the user IDs of users who have not added the bot as a friend or has blocked the bot.
        /// Use the group Id returned in the source object of webhook event objects. 
        /// This feature is only available for LINE@ Approved accounts or official accounts
        /// </summary>
        /// <param name="groupId">Identifier of the group</param>
        /// <returns>List of UserProfile</returns>
        Task<IList<UserProfile>> GetGroupMemberProfilesAsync(string groupId);

        /// <summary>
        /// Leave a group.
        /// Use the ID that is returned via webhook from the source group. 
        /// https://developers.line.biz/ja/reference/messaging-api/#leave-group
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns></returns>
        Task LeaveFromGroupAsync(string groupId);
        Task<GroupSummary> GetGroupSummary(string groupId);
        Task<MemberCount> GetGroupMemberCount(string groupid);

        #endregion

        #region Room

        /// <summary>
        /// Gets the user profile of a member of a room that the bot is in. This includes user profiles of users who have not added the bot as a friend or have blocked the bot.
        /// Use the room ID and user ID returned in the source object of webhook event objects. Do not use the LINE ID used in the LINE app
        /// </summary>
        /// <param name="roomId">Identifier of the room</param>
        /// <param name="userId">Identifier of the user</param>
        /// <returns></returns>
        Task<UserProfile> GetRoomMemberProfileAsync(string roomId, string userId);

        /// <summary>
        /// Gets the user IDs of the members of a room that the bot is in. This includes the user IDs of users who have not added the bot as a friend or has blocked the bot.
        /// Use the room ID returned in the source object of webhook event objects. 
        /// This feature is only available for LINE@ Approved accounts or official accounts.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-room-member-user-ids
        /// </summary>
        /// <param name="roomId">Identifier of the room</param>
        /// <param name="continuationToken">ContinuationToken</param>
        /// <returns>GroupMemberIds</returns>
        Task<GroupMemberIds> GetRoomMemberIdsAsync(string roomId, string continuationToken = null);

        /// <summary>
        /// Gets the user profiles of the members of a room that the bot is in. This includes the user IDs of users who have not added the bot as a friend or has blocked the bot.
        /// Use the room ID returned in the source object of webhook event objects. 
        /// This feature is only available for LINE@ Approved accounts or official accounts.
        /// </summary>
        /// <param name="roomId">Identifier of the room</param>
        /// <returns>List of UserProfiles</returns>
        Task<IList<UserProfile>> GetRoomMemberProfilesAsync(string roomId);

        /// <summary>
        /// Leave a room.
        /// Use the ID that is returned via webhook from the source room. 
        /// </summary>
        /// <param name="roomId">Room ID</param>
        Task LeaveFromRoomAsync(string roomId);
        Task<MemberCount> GetRoomMemberCount(string roomId);

        #endregion

        #region Rich menu

        /// <summary>
        /// Gets a rich menu via a rich menu ID.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-rich-menu
        /// </summary>
        /// <param name="richMenuId">ID of an uploaded rich menu</param>
        /// <returns>RichMenu</returns>
        Task<RichMenu> GetRichMenuAsync(string richMenuId);

        /// <summary>
        /// Creates a rich menu. 
        /// Note: You must upload a rich menu image and link the rich menu to a user for the rich menu to be displayed.You can create up to 10 rich menus for one bot.
        /// The rich menu represented as a rich menu object.
        /// https://developers.line.biz/ja/reference/messaging-api/#create-rich-menu
        /// </summary>
        /// <param name="richMenu">RichMenu</param>
        /// <returns>RichMenu Id</returns>
        Task<string> CreateRichMenuAsync(RichMenu richMenu);

        /// <summary>
        /// Deletes a rich menu.
        /// https://developers.line.biz/ja/reference/messaging-api/#delete-rich-menu
        /// </summary>
        /// <param name="richMenuId">RichMenu Id</param>
        Task DeleteRichMenuAsync(string richMenuId);

        /// <summary>
        /// Gets the ID of the rich menu linked to a user.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-rich-menu-id-of-user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns>RichMenu Id</returns>
        Task<string> GetRichMenuIdOfUserAsync(string userId);

        /// <summary>
        /// Sets a default ritch menu
        /// </summary>
        /// <param name="richMenuId">
        /// ID of an uploaded rich menu
        /// </param>
        Task SetDefaultRichMenuAsync(string richMenuId);

        /// <summary>
        /// Links a rich menu to a user.
        /// Note: Only one rich menu can be linked to a user at one time.
        /// https://developers.line.biz/ja/reference/messaging-api/#link-rich-menu-to-user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <param name="richMenuId">ID of an uploaded rich menu</param>
        /// <returns></returns>
        Task LinkRichMenuToUserAsync(string userId, string richMenuId);

        /// <summary>
        /// Unlinks a rich menu from a user.
        /// https://developers.line.biz/ja/reference/messaging-api/#unlink-rich-menu-from-user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns></returns>
        Task UnLinkRichMenuFromUserAsync(string userId);

        /// <summary>
        /// Downloads an image associated with a rich menu.
        /// https://developers.line.biz/ja/reference/messaging-api/#download-rich-menu-image
        /// </summary>
        /// <param name="richMenuId">RichMenu Id</param>
        /// <returns>Image as ContentStream</returns>
        Task<ContentStream> DownloadRichMenuImageAsync(string richMenuId);

        /// <summary>
        /// Uploads and attaches a jpeg image to a rich menu.
        /// Images must have one of the following resolutions: 2500x1686, 2500x843. 
        /// You cannot replace an image attached to a rich menu.To update your rich menu image, create a new rich menu object and upload another image.
        /// https://developers.line.biz/ja/reference/messaging-api/#upload-rich-menu-image
        /// </summary>
        /// <param name="stream">Jpeg image for the rich menu</param>
        /// <param name="richMenuId">The ID of the rich menu to attach the image to.</param>
        Task UploadRichMenuJpegImageAsync(Stream stream, string richMenuId);

        /// <summary>
        /// Uploads and attaches a png image to a rich menu.
        /// Images must have one of the following resolutions: 2500x1686, 2500x843. 
        /// You cannot replace an image attached to a rich menu.To update your rich menu image, create a new rich menu object and upload another image.
        /// https://developers.line.biz/ja/reference/messaging-api/#upload-rich-menu-image
        /// </summary>
        /// <param name="stream">Png image for the rich menu</param>
        /// <param name="richMenuId">The ID of the rich menu to attach the image to.</param>
        Task UploadRichMenuPngImageAsync(Stream stream, string richMenuId);

        /// <summary>
        /// Gets a list of all uploaded rich menus.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-rich-menu-list
        /// </summary>
        /// <returns>List of ResponseRichMenu</returns>
        Task<IList<ResponseRichMenu>> GetRichMenuListAsync();

        #endregion

        #region Account Link

        /// <summary>
        /// Issues a link token used for the account link feature.
        /// <para>https://developers.line.me/en/docs/messaging-api/linking-accounts</para>
        /// </summary>
        /// <param name="userId">
        /// User ID for the LINE account to be linked. Found in the source object of account link event objects. Do not use the LINE ID used in the LINE app.
        /// </param>
        /// <returns>
        /// Returns the status code 200 and a link token. Link tokens are valid for 10 minutes and can only be used once.
        /// Note: The validity period may change without notice.
        /// </returns>
        Task<string> IssueLinkTokenAsync(string userId);

        #endregion
    }
}
