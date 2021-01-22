﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging
{
    /// <summary>
    /// LINE Messaging API client, which handles request/response to LINE server.
    /// </summary>
    public class LineMessagingClient : ILineMessagingClient, IDisposable
    {
        private const string DEFAULT_URI = "https://api.line.me/v2";

        private HttpClient _client;
        private JsonSerializerSettings _jsonSerializerSettings;
        private string _uri;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="channelAccessToken">ChannelAccessToken</param>
        /// <param name="uri">Request URI</param>
        public LineMessagingClient(string channelAccessToken, string uri = DEFAULT_URI)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", channelAccessToken);
            _jsonSerializerSettings = new CamelCaseJsonSerializerSettings();
            _uri = uri;
        }

        #region OAuth
        // https://developers.line.biz/ja/reference/messaging-api/#oauth

        /// <summary>
        /// Issues a short-lived channel access token. 
        /// Up to 30 tokens can be issued. If the maximum is exceeded, existing channel access tokens will be revoked in the order of when they were first issued.
        /// https://developers.line.me/en/docs/messaging-api/reference/#oauth
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <param name="channelId">ChannelId</param>
        /// <param name="channelAccessToken">ChannelAccessToken</param>
        /// <param name="uri">URI</param>
        /// <returns>ChannelAccessToken</returns>
        public static async Task<ChannelAccessToken> IssueChannelAccessTokenAsync(HttpClient httpClient, string channelId, string channelAccessToken, string uri = DEFAULT_URI)
        {
            var response = await httpClient.PostAsync($"{uri}/oauth/accessToken",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "client_credentials",
                    ["client_id"] = channelId,
                    ["client_secret"] = channelAccessToken
                })).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ChannelAccessToken>(json,
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                });
        }

        /// <summary>
        /// Revokes a channel access token.
        /// https://developers.line.me/en/docs/messaging-api/reference/#revoke-channel-access-token
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <param name="channelAccessToken">ChannelAccessToken</param>
        /// <param name="uri">URI</param>
        public static async Task RevokeChannelAccessTokenAsync(HttpClient httpClient, string channelAccessToken, string uri = DEFAULT_URI)
        {
            var response = await httpClient.PostAsync($"{uri}/oauth/revoke",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["access_token"] = channelAccessToken
                })).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Instantiate LineMessagingClient by using OAuth.
        /// https://developers.line.me/en/docs/messaging-api/reference/#oauth
        /// </summary>
        /// <param name="channelId">ChannelId</param>
        /// <param name="channelSecret">ChannelSecret</param>
        /// <param name="uri">URI</param>
        /// <returns></returns>
        public static async Task<LineMessagingClient> CreateAsync(string channelId, string channelSecret, string uri = DEFAULT_URI)
        {
            if (string.IsNullOrEmpty(channelId)) { throw new ArgumentNullException(nameof(channelId)); }
            if (string.IsNullOrEmpty(channelSecret)) { throw new ArgumentNullException(nameof(channelSecret)); }
            using (var client = new HttpClient())
            {
                var accessToken = await IssueChannelAccessTokenAsync(client, channelId, channelSecret, uri);
                return new LineMessagingClient(accessToken.AccessToken, uri);
            }
        }

        #endregion

        #region Message 
        // https://developers.line.biz/ja/reference/messaging-api/#messages

        /// <summary>
        /// 応答メッセージを送る。
        /// Send response messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-reply-message
        /// </summary>
        /// <param name="replyToken">ReplyToken</param>
        /// <param name="messages">Reply messages. Up to 5 messages.</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        public virtual async Task ReplyMessageAsync(string replyToken, IList<ISendMessage> messages, bool notificationDisabled = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/bot/message/reply");
            var content = JsonConvert.SerializeObject(new { replyToken, messages, notificationDisabled }, _jsonSerializerSettings);
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 応答メッセージを送る。
        /// Send response messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-reply-message
        /// </summary>
        /// <param name="replyToken">ReplyToken</param>
        /// <param name="messages">Reply messages. Up to 5 messages.</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        public virtual Task ReplyMessageAsync(string replyToken, bool notificationDisabled = false, params string[] messages)
        {
            return ReplyMessageAsync(replyToken, messages.Select(msg => new TextMessage(msg)).ToArray(), notificationDisabled);
        }

        /// <summary>
        /// 応答メッセージを送る。
        /// Send response messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-reply-message
        /// </summary>
        /// <param name="replyToken">ReplyToken</param>
        /// <param name="messages">Reply messages. Up to 5 messages.</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        public virtual async Task ReplyMessageWithJsonAsync(string replyToken, bool notificationDisabled = false, params string[] messages)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/bot/message/reply");
            var json =
$@"{{ 
    ""replyToken"" : ""{replyToken}"", 
    ""messages"" : [{string.Join(", ", messages)}],
    ""notificationDisabled"" : {notificationDisabled}
}}";
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// プッシュメッセージを送る。
        /// Send push messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-push-message
        /// </summary>
        /// <param name="to">ID of the receiver</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Set reply messages with Json string.</param>
        public virtual async Task PushMessageAsync(string to, IList<ISendMessage> messages, bool notificationDisabled = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/bot/message/push");
            var content = JsonConvert.SerializeObject(new { to, messages, notificationDisabled }, _jsonSerializerSettings);
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// プッシュメッセージを送る。
        /// Send push messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-push-message
        /// </summary>
        /// <param name="to">ID of the receiver</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Set reply messages with Json string.</param>
        public virtual async Task PushMessageWithJsonAsync(string to, bool notificationDisabled = false, params string[] messages)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/bot/message/push");
            var json =
$@"{{ 
    ""to"" : ""{to}"", 
    ""messages"" : [{string.Join(", ", messages)}],
    ""notificationDisabled"" : {notificationDisabled}
}}";
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }


        /// <summary>
        /// プッシュメッセージを送る。
        /// Send push messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-push-message
        /// </summary>
        /// <param name="to">ID of the receiver</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Set reply messages with Json string.</param>
        public virtual Task PushMessageAsync(string to, bool notificationDisabled = false, params string[] messages)
        {
            return PushMessageAsync(to, messages.Select(msg => new TextMessage(msg)).ToArray(), notificationDisabled);
        }

        /// <summary>
        /// マルチキャストメッセージを送る。
        /// Send multicast messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-multicast-message
        /// </summary>
        /// <param name="to">IDs of the receivers. Max: 500 users</param>
        /// <param name="messages">Reply messages. Up to 5 messages.</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        public virtual async Task MultiCastMessageAsync(IList<string> to, IList<ISendMessage> messages, bool notificationDisabled = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/bot/message/multicast");
            var content = JsonConvert.SerializeObject(new { to, messages, notificationDisabled }, _jsonSerializerSettings);
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// マルチキャストメッセージを送る。
        /// Send multicast messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-multicast-message
        /// </summary>
        /// <param name="to">IDs of the receivers. Max: 500 users</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Reply messages. Up to 5 messages.</param>
        public virtual async Task MultiCastMessageWithJsonAsync(IList<string> to, bool notificationDisabled = false, params string[] messages)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/bot/message/multicast");
            var json =
$@"{{ 
    ""to"" : [{string.Join(", ", to.Select(x => "\"" + x + "\""))}], 
    ""messages"" : [{string.Join(", ", messages)}] 
    ""notificationDisabled"" : {notificationDisabled}
}}";

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// マルチキャストメッセージを送る。
        /// Send multicast messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-multicast-message
        /// </summary>
        /// <param name="to">IDs of the receivers. Max: 500 users</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Reply messages. Up to 5 messages.</param>
        public virtual Task MultiCastMessageAsync(IList<string> to, bool notificationDisabled = false, params string[] messages)
        {
            return MultiCastMessageAsync(to, messages.Select(msg => new TextMessage(msg)).ToArray(), notificationDisabled);
        }

        /// <summary>
        /// ナローキャストメッセージの進行状況を取得する
        /// Get the progress of a narrowcast message.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-narrowcast-progress-status
        /// </summary>
        /// <param name="requestId"></param>
        public virtual async Task<ProgressNarrowcast> GetProgressNarrowcastAsync(string requestId)
        {
            var response = await GetStringAsync($"{_uri}/bot/message/progress/narrowcast");
            return JsonConvert.DeserializeObject<ProgressNarrowcast>(response);
        }

        /// <summary>
        /// ブロードキャストキャストメッセージを送る。
        /// Send broadcast messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-multicast-messages
        /// </summary>
        /// <param name="messages">Reply text messages. Up to 5 messages.</param>
        /// <param name="notificationDisabled">Notify the user.</param>
        public virtual async Task BroadCastMessageAsync(IList<ISendMessage> messages, bool notificationDisabled = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/bot/message/broadcast");
            var content = JsonConvert.SerializeObject(new { messages, notificationDisabled }, _jsonSerializerSettings);
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// ブロードキャストキャストメッセージを送る。
        /// Send broadcast messages.
        /// https://developers.line.biz/ja/reference/messaging-api/#send-multicast-messages
        /// </summary>
        /// <param name="notificationDisabled">Notify the user.</param>
        /// <param name="messages">Reply text messages. Up to 5 messages.</param>
        public virtual Task BroadCastMessageAsync(bool notificationDisabled = false, params string[] messages)
        {
            return BroadCastMessageAsync(messages.Select(msg => new TextMessage(msg)).ToArray(), notificationDisabled);
        }

        /// <summary>
        /// コンテンツを取得する
        /// Retrieving contents
        /// https://developers.line.biz/ja/reference/messaging-api/#get-content
        /// </summary>
        /// <param name="messageId">Message ID</param>
        /// <returns>Content as ContentStream</returns>
        public virtual async Task<ContentStream> GetContentStreamAsync(string messageId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api-data.line.me/v2/bot/message/{messageId}/content");
            var response = await _client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
            return new ContentStream(await response.Content.ReadAsStreamAsync(), response.Content.Headers);
        }

        /// <summary>
        /// コンテンツを取得する
        /// Retrieving contents
        /// https://developers.line.biz/ja/reference/messaging-api/#get-content
        /// </summary>
        /// <param name="messageId">Message ID</param>
        /// <returns>Content as ContentStream</returns>
        public virtual async Task<byte[]> GetContentBytesAsync(string messageId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api-data.line.me/v2/bot/message/{messageId}/content");
            var response = await _client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
            return await response.Content.ReadAsByteArrayAsync();
        }
        /// <summary>
        /// 当月分の上限目安を取得します。
        /// Get the upper limit guideline for the current month.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-quota
        /// </summary>
        public virtual async Task<Quota> GetQuota()
        {
            var response = await GetStringAsync($"{_uri}/bot/message/quota");
            return JsonConvert.DeserializeObject<Quota>(response);
        }

        /// <summary>
        /// 当月に送信されたメッセージの数を取得します。
        /// Gets the number of messages sent in the current month.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-consumption
        /// </summary>
        public virtual async Task<Consumption> GetConsumption()
        {
            var response = await GetStringAsync($"{_uri}/bot/message/quota/consumption");
            return JsonConvert.DeserializeObject<Consumption>(response);
        }

        /// <summary>
        /// 送信済みのプッシュメッセージの数を取得する。
        /// Get the number of push messages that have been sent.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-number-of-push-messages
        /// </summary>
        public virtual async Task<NumberOfMessages> GetNumberOfPushMessages(DateTime date)
        {
            var response = await GetStringAsync($"{_uri}/bot/message/delivery/push?date={date.ToString("yyyyMMdd")}");
            return JsonConvert.DeserializeObject<NumberOfMessages>(response);
        }

        /// <summary>
        /// 送信済みの応答メッセージの数を取得する。
        /// Get the number of response messages that have been sent.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-number-of-reply-messages
        /// </summary>
        public virtual async Task<NumberOfMessages> GetNumberOfReplyMessages(DateTime date)
        {
            var response = await GetStringAsync($"{_uri}/bot/message/delivery/reply?date={date.ToString("yyyyMMdd")}");
            return JsonConvert.DeserializeObject<NumberOfMessages>(response);
        }

        /// <summary>
        /// 送信済みのマルチキャストメッセージの数を取得する。
        /// Get the number of multicast messages that have been sent.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-number-of-multicast-messages
        /// </summary>
        public virtual async Task<NumberOfMessages> GetNumberOfMulticastMessages(DateTime date)
        {
            var response = await GetStringAsync($"{_uri}/bot/message/delivery/multicast?date={date.ToString("yyyyMMdd")}");
            return JsonConvert.DeserializeObject<NumberOfMessages>(response);
        } 

        /// <summary>
        /// 送信済みのブロードキャストメッセージの数を取得する。
        /// Get the number of broadcast messages that have been sent.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-number-of-broadcast-messages
        /// </summary>
        public virtual async Task<NumberOfMessages> GetNumberOfBroadcastMessages(DateTime date)
        {
            var response = await GetStringAsync($"{_uri}/bot/message/delivery/broadcast?date={date.ToString("yyyyMMdd")}");
            return JsonConvert.DeserializeObject<NumberOfMessages>(response);
        }

        #endregion

        #region Profile
        // https://developers.line.me/en/docs/messaging-api/reference/#profile

        /// <summary>
        /// Get user profile information.
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-profile
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        public virtual async Task<UserProfile> GetUserProfileAsync(string userId)
        {
            var content = await GetStringAsync($"{_uri}/bot/profile/{userId}").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<UserProfile>(content);
        }

        #endregion

        #region Group
        // https://developers.line.me/en/docs/messaging-api/reference/#group

        /// <summary>
        /// Gets the user profile of a member of a group that the bot is in. This includes user profiles of users who have not added the bot as a friend or have blocked the bot.
        /// Use the group ID and user ID returned in the source object of webhook event objects. Do not use the LINE ID used in the LINE app. 
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-group-member-profile
        /// </summary>
        /// <param name="groupId">Identifier of the group</param>
        /// <param name="userId">Identifier of the user</param>
        /// <returns>User Profile</returns>
        public virtual async Task<UserProfile> GetGroupMemberProfileAsync(string groupId, string userId)
        {
            var content = await GetStringAsync($"{_uri}/bot/group/{groupId}/member/{userId}").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<UserProfile>(content);
        }

        /// <summary>
        /// Gets the user IDs of the members of a group that the bot is in. This includes the user IDs of users who have not added the bot as a friend or has blocked the bot.
        /// This feature is only available for LINE@ Approved accounts or official accounts.
        /// Use the group Id returned in the source object of webhook event objects. 
        /// Users who have not agreed to the Official Accounts Terms of Use are not included in memberIds. There is no fixed number of memberIds. 
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-group-member-user-ids
        /// </summary>
        /// <param name="groupId">Identifier of the group</param>
        /// <param name="continuationToken">ContinuationToken</param>
        /// <returns>GroupMemberIds</returns>
        public virtual async Task<GroupMemberIds> GetGroupMemberIdsAsync(string groupId, string continuationToken)
        {
            var requestUrl = $"{_uri}/bot/group/{groupId}/members/ids";
            if (continuationToken != null)
            {
                requestUrl += $"?start={continuationToken}";
            }

            var content = await GetStringAsync(requestUrl).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GroupMemberIds>(content);
        }

        /// <summary>
        /// Gets the user profiles of the members of a group that the bot is in. This includes the user IDs of users who have not added the bot as a friend or has blocked the bot.
        /// Use the group Id returned in the source object of webhook event objects. 
        /// This feature is only available for LINE@ Approved accounts or official accounts
        /// </summary>
        /// <param name="groupId">Identifier of the group</param>
        /// <returns>List of UserProfile</returns>
        public virtual async Task<IList<UserProfile>> GetGroupMemberProfilesAsync(string groupId)
        {
            var result = new List<UserProfile>();
            string continuationToken = null;
            do
            {
                var ids = await GetGroupMemberIdsAsync(groupId, continuationToken);

                var tasks = ids.MemberIds.Select(async userId => await GetGroupMemberProfileAsync(groupId, userId));
                var profiles = await Task.WhenAll(tasks.ToArray());

                result.AddRange(profiles);
                continuationToken = ids.Next;
            }
            while (continuationToken != null);
            return result;
        }

        /// <summary>
        /// Leave a group.
        /// Use the ID that is returned via webhook from the source group. 
        /// https://developers.line.me/en/docs/messaging-api/reference/#leave-group
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns></returns>
        public virtual async Task LeaveFromGroupAsync(string groupId)
        {
            var response = await _client.PostAsync($"{_uri}/bot/group/{groupId}/leave", null).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }
        public virtual async Task<GroupSummary> GetGroupSummary(string groupId)
        {
            var content = await GetStringAsync($"{_uri}/bot/group/{groupId}/summary").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GroupSummary>(content);
        }
        public virtual async Task<MemberCount> GetGroupMemberCount(string groupId)
        {
            var content = await GetStringAsync($"{_uri}/bot/group/{groupId}/members/count").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<MemberCount>(content);
        }
        #endregion

        #region Room
        // https://developers.line.biz/ja/reference/messaging-api/#chat-room

        /// <summary>
        /// トークルームに参加しているユーザーの人数を取得する。
        /// Get the number of users participating in a talk room.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-members-room-count
        /// </summary>
        /// <param name="userId"></param>
        public virtual async Task<MemberCount> GetRoomMemberCountAsync(string roomId)
        {
            var content = await GetStringAsync($"{_uri}/bot/room/{roomId}/members/count").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<MemberCount>(content);
        }

        /// <summary>
        /// トークルームメンバーのユーザーIDを取得する
        /// Get the user ID of the talk room member.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-members-room-count
        /// </summary>
        /// <param name="roomId">Identifier of the room</param>
        /// <param name="continuationToken">ContinuationToken</param>
        /// <returns>GroupMemberIds</returns>
        public virtual async Task<GroupMemberIds> GetRoomMemberIdsAsync(string roomId, string continuationToken = null)
        {
            var requestUrl = $"{_uri}/bot/room/{roomId}/members/ids";
            if (continuationToken != null)
            {
                requestUrl += $"?start={continuationToken}";
            }

            var content = await GetStringAsync(requestUrl).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<GroupMemberIds>(content);
        }

        /// <summary>
        /// トークルームメンバーのプロフィール情報を取得する
        /// Get the profile information of a talk room member.
        /// https://developers.line.biz/ja/reference/messaging-api/#get-room-member-profile
        /// </summary>
        /// <param name="roomId">Identifier of the room</param>
        /// <returns>List of UserProfiles</returns>
        public virtual async Task<UserProfile> GetRoomMemberProfilesAsync(string roomId,string userId)
        {
            var requestUrl = $"{_uri}/bot/room/{roomId}/members/{userId}";

            var content = await GetStringAsync(requestUrl).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<UserProfile>(content);
        }

        /// <summary>
        /// トークルームから退出する
        /// Exit the talk room.
        /// https://developers.line.biz/ja/reference/messaging-api/#leave-room
        /// </summary>
        /// <param name="roomId">Room ID</param>
        public virtual async Task LeaveFromRoomAsync(string roomId)
        {
            var response = await _client.PostAsync($"{_uri}/bot/room/{roomId}/leave", null).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }
        #endregion

        #region Rich menu
        // https://developers.line.me/en/docs/messaging-api/reference/#rich-menu

        /// <summary>
        /// Gets a rich menu via a rich menu ID.
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-rich-menu
        /// </summary>
        /// <param name="richMenuId">ID of an uploaded rich menu</param>
        /// <returns>RichMenu</returns>
        public virtual async Task<RichMenu> GetRichMenuAsync(string richMenuId)
        {
            var json = await GetStringAsync($"{_uri}/bot/richmenu/{richMenuId}").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ResponseRichMenu>(json);
        }

        /// <summary>
        /// Creates a rich menu. 
        /// Note: You must upload a rich menu image and link the rich menu to a user for the rich menu to be displayed.You can create up to 10 rich menus for one bot.
        /// The rich menu represented as a rich menu object.
        /// https://developers.line.me/en/docs/messaging-api/reference/#create-rich-menu
        /// </summary>
        /// <param name="richMenu">RichMenu</param>
        /// <returns>RichMenu Id</returns>
        public virtual async Task<string> CreateRichMenuAsync(RichMenu richMenu)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_uri}/bot/richmenu");
            var content = JsonConvert.SerializeObject(richMenu, _jsonSerializerSettings);
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeAnonymousType(json, new { richMenuId = "" }).richMenuId;
        }

        /// <summary>
        /// Deletes a rich menu.
        /// https://developers.line.me/en/docs/messaging-api/reference/#delete-rich-menu
        /// </summary>
        /// <param name="richMenuId">RichMenu Id</param>
        public virtual async Task DeleteRichMenuAsync(string richMenuId)
        {
            var response = await _client.DeleteAsync($"{_uri}/bot/richmenu/{richMenuId}");
        }

        /// <summary>
        /// Gets the ID of the rich menu linked to a user.
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-rich-menu-id-of-user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns>RichMenu Id</returns>
        public virtual async Task<string> GetRichMenuIdOfUserAsync(string userId)
        {
            var json = await GetStringAsync($"{_uri}/bot/user/{userId}/richmenu");
            return JsonConvert.DeserializeAnonymousType(json, new { richMenuId = "" }).richMenuId;
        }

        /// <summary>
        /// Sets a default ritch menu
        /// </summary>
        /// <param name="richMenuId">
        /// ID of an uploaded rich menu
        /// </param>
        public virtual async Task SetDefaultRichMenuAsync(string richMenuId)
        {
            var response = await _client.PostAsync($"{_uri}/bot/user/all/richmenu/{richMenuId}", null);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Links a rich menu to a user.
        /// Note: Only one rich menu can be linked to a user at one time.
        /// https://developers.line.me/en/docs/messaging-api/reference/#link-rich-menu-to-user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <param name="richMenuId">ID of an uploaded rich menu</param>
        /// <returns></returns>
        public virtual async Task LinkRichMenuToUserAsync(string userId, string richMenuId)
        {
            var response = await _client.PostAsync($"{_uri}/bot/user/{userId}/richmenu/{richMenuId}", null);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Unlinks a rich menu from a user.
        /// https://developers.line.me/en/docs/messaging-api/reference/#unlink-rich-menu-from-user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns></returns>
        public virtual async Task UnLinkRichMenuFromUserAsync(string userId)
        {
            var response = await _client.DeleteAsync($"{_uri}/bot/user/{userId}/richmenu").ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Downloads an image associated with a rich menu.
        /// https://developers.line.me/en/docs/messaging-api/reference/#download-rich-menu-image
        /// </summary>
        /// <param name="richMenuId">RichMenu Id</param>
        /// <returns>Image as ContentStream</returns>
        public virtual async Task<ContentStream> DownloadRichMenuImageAsync(string richMenuId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api-data.line.me/v2/bot/richmenu/{richMenuId}/content");
            var response = await _client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
            return new ContentStream(await response.Content.ReadAsStreamAsync(), response.Content.Headers);
        }

        /// <summary>
        /// Uploads and attaches a jpeg image to a rich menu.
        /// Images must have one of the following resolutions: 2500x1686, 2500x843. 
        /// You cannot replace an image attached to a rich menu.To update your rich menu image, create a new rich menu object and upload another image.
        /// https://developers.line.me/en/docs/messaging-api/reference/#upload-rich-menu-image
        /// </summary>
        /// <param name="stream">Jpeg image for the rich menu</param>
        /// <param name="richMenuId">The ID of the rich menu to attach the image to.</param>
        public virtual Task UploadRichMenuJpegImageAsync(Stream stream, string richMenuId)
        {
            return UploadRichMenuImageAsync(stream, richMenuId, "image/jpeg");
        }

        /// <summary>
        /// Uploads and attaches a png image to a rich menu.
        /// Images must have one of the following resolutions: 2500x1686, 2500x843. 
        /// You cannot replace an image attached to a rich menu.To update your rich menu image, create a new rich menu object and upload another image.
        /// https://developers.line.me/en/docs/messaging-api/reference/#upload-rich-menu-image
        /// </summary>
        /// <param name="stream">Png image for the rich menu</param>
        /// <param name="richMenuId">The ID of the rich menu to attach the image to.</param>
        public virtual Task UploadRichMenuPngImageAsync(Stream stream, string richMenuId)
        {
            return UploadRichMenuImageAsync(stream, richMenuId, "image/png");
        }

        protected async Task UploadRichMenuImageAsync(Stream stream, string richMenuId, string mediaType)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api-data.line.me/v2/bot/richmenu/{richMenuId}/content")
            {
                Content = new StreamContent(stream)
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            var response = await _client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of all uploaded rich menus.
        /// https://developers.line.me/en/docs/messaging-api/reference/#get-rich-menu-list
        /// </summary>
        /// <returns>List of ResponseRichMenu</returns>
        public virtual async Task<IList<ResponseRichMenu>> GetRichMenuListAsync()
        {
            var response = await _client.GetAsync($"{_uri}/bot/richmenu/list").ConfigureAwait(false);
            var menus = new List<ResponseRichMenu>();
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return menus;
            }
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            dynamic result = JsonConvert.DeserializeObject(json);
            if (result == null) { return menus; }

            foreach (var dynamicObject in result.richmenus)
            {
                menus.Add(ResponseRichMenu.CreateFrom(dynamicObject));
            }
            return menus;
        }

        #endregion

        #region Account Link

        /// <summary>
        /// Issues a link token used for the account link feature.
        /// <para>https://developers.line.biz/ja/reference/messaging-api/#account-link</para>
        /// </summary>
        /// <param name="userId">
        /// User ID for the LINE account to be linked. Found in the source object of account link event objects. Do not use the LINE ID used in the LINE app.
        /// </param>
        /// <returns>
        /// Returns the status code 200 and a link token. Link tokens are valid for 10 minutes and can only be used once.
        /// Note: The validity period may change without notice.
        /// </returns>
        public virtual async Task<string> IssueLinkTokenAsync(string userId)
        {
            var response = await _client.PostAsync($"{_uri}/bot/user/{userId}/linkToken", null);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeAnonymousType(content, new { linkToken = "" }).linkToken;
        }

        #endregion

        public void Dispose()
        {
            _client?.Dispose();
        }

        protected virtual async Task<string> GetStringAsync(string requestUri)
        {
            var response = await _client.GetAsync(requestUri).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
