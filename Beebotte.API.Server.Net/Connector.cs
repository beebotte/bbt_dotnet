// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-16-2014
// ***********************************************************************
// <copyright file="Connector.cs" company="Beebotte">
//     Copyright (c) Beebotte. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

/// <summary>
/// The Net namespace.
/// </summary>
namespace Beebotte.API.Server.Net
{
    /// <summary>
    /// The Connector is the interface that makes it possible for applications to communicate with Beebotte.
    /// The Connector allows you to Read, Create and Delete Channels and Resources, Read data from Beebotte, and Write and Publish data to Beebotte.
    /// </summary>
    public class Connector
    {
        #region Properties
        /// <summary>
        /// The Access Key used to connect to Beebotte. the Access Key can be found in the Account Settings page.
        /// </summary>
        /// <value>The access key.</value>
        public string AccessKey { get; set; }
        /// <summary>
        /// The Security Key is a secret shared between user and Beebotte. It is used to authenticate and sign the communications with Beebotte.
        /// </summary>
        /// <value>The secure key.</value>
        public string SecureKey { get; set; }
        /// <summary>
        /// Beebotte API version
        /// </summary>
        public static string APIVersion;
        /// <summary>
        /// Represents Beebotte host name e.g. api.beebotte.com
        /// </summary>
        /// <value>The hostname.</value>
        [DefaultValue(Constants.DefaultHostName)]
        public string Hostname { get; set; }
        /// <summary>
        /// Represents the default protocol used to connect to Beebotte. e.g. http
        /// </summary>
        /// <value>The protocol.</value>
        [DefaultValue(Constants.DefaultProtocol)]
        public string Protocol { get; set; }
        /// <summary>
        /// Represents the port used to connect to Beebotte.
        /// </summary>
        /// <value>The port.</value>
        [DefaultValue(Constants.DefaultPort)]
        public int Port { get; set; }

        public string Token { get; set; }

        protected string AuthenticationType
        {
            get
            {
                if (!String.IsNullOrEmpty(SecureKey) && !String.IsNullOrEmpty(AccessKey))
                {
                    return AuthenticationTypes.APIKeys.ToString();
                }
                else if (!String.IsNullOrEmpty(Token))
                {
                    return Token.Contains(Constants.IAMTokenPrefix) ?
                        AuthenticationTypes.IAMToken.ToString() : AuthenticationTypes.Token.ToString();
                }
                else

                {
                    throw new MissingParameterException("You must provide an authentication token or your API access and secret keys!");
                }
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// The _date
        /// </summary>
        private DateTime _date;

        #endregion

        #region ctor
        /// <summary>
        /// Initialize a new instance of Beebotte connector
        /// </summary>
        /// <param name="accessKey">Account Access Key</param>
        /// <param name="secureKey">Account Security Key</param>
        public Connector(string accessKey, string secureKey)
            : this(accessKey, secureKey, Constants.DefaultHostName, Constants.DefaultProtocol, Constants.DefaultPort, Constants.DefaultVersion)
        {
        }

        /// <summary>
        /// Initialize a new instance of Beebotte connector
        /// </summary>
        public Connector()
            : this(Constants.DefaultHostName, Constants.DefaultProtocol, Constants.DefaultPort, Constants.DefaultVersion)
        {
        }

        /// <summary>
        /// Initialize a new instance of Beebotte connector
        /// </summary>
        /// <param name="hostName">Beebotte API host name</param>
        public Connector(string hostname)
            : this(hostname, Constants.DefaultProtocol, Constants.DefaultPort, Constants.DefaultVersion)
        {
        }

        /// <summary>
        /// Initialize a new instance of Beebotte connector
        /// </summary>
        /// <param name="accessKey">Account Access Key</param>
        /// <param name="secureKey">Account Security Key</param>
        /// <param name="hostName">Beebotte API host name</param>
        public Connector(string accessKey, string secureKey, string hostName)
            : this(accessKey, secureKey, hostName, Constants.DefaultProtocol, Constants.DefaultPort, Constants.DefaultVersion)
        {
        }

        /// <summary>
        /// Initialize a new instance of Beebotte connector
        /// </summary>
        /// <param name="accessKey">Account Access Key</param>
        /// <param name="secureKey">Account Security Key</param>
        /// <param name="hostname">Beebotte API host name</param>
        /// <param name="protocol">Beebotte API protocol</param>
        /// <param name="port">Beebotte API port</param>
        /// <param name="version">Beebotte API version</param>
        /// <exception cref="MissingParameterException"></exception>
        public Connector(string accessKey, string secureKey, string hostname, string protocol, int port, string version)
        {
            if (String.IsNullOrEmpty(accessKey) || String.IsNullOrEmpty(secureKey) || String.IsNullOrEmpty(hostname) ||
                String.IsNullOrEmpty(protocol) || String.IsNullOrEmpty(version))
            {
                throw new MissingParameterException(
                    String.Format(
                        "Missing parameter(s) to connect to Beebotte. Access Key: {0}, Secure key: {1}, Hostoname:{2}, Protocol:{3}, Port:{4}, Version:{5}.",
                        accessKey, secureKey, hostname, protocol, port, version));
            }
            AccessKey = accessKey;
            SecureKey = secureKey;
            Hostname = hostname;
            Protocol = protocol;
            Port = port;
            APIVersion = version;
        }

        public Connector(string token, string hostname, string protocol, int port, string version)
        {
            if (String.IsNullOrEmpty(token) || String.IsNullOrEmpty(hostname) ||
                String.IsNullOrEmpty(protocol) || String.IsNullOrEmpty(version))
            {
                throw new MissingParameterException(
                    String.Format(
                        "Missing parameter(s) to connect to Beebotte. Token: {0}, Hostoname:{1}, Protocol:{2}, Port:{3}, Version:{4}.",
                        token, hostname, protocol, port, version));
            }
            Token = token;
            Hostname = hostname;
            Protocol = protocol;
            Port = port;
            APIVersion = version;
        }


        /// <summary>
        /// Initialize a new instance of Beebotte connector
        /// </summary>
        /// <param name="hostname">Beebotte API host name</param>
        /// <param name="protocol">Beebotte API protocol</param>
        /// <param name="port">Beebotte API port</param>
        /// <param name="version">Beebotte API version</param>
        /// <exception cref="MissingParameterException"></exception>
        public Connector(string hostname, string protocol, int port, string version)
        {
            if (String.IsNullOrEmpty(hostname) || String.IsNullOrEmpty(protocol) || String.IsNullOrEmpty(version))
            {
                throw new MissingParameterException(
                    String.Format(
                        "Missing parameter(s) to connect to Beebotte. Hostoname:{0}, Protocol:{1}, Port:{2}, Version:{3}.",
                        hostname, protocol, port, version));
            }
            Hostname = hostname;
            Protocol = protocol;
            Port = port;
            APIVersion = version;
        }
        #endregion

        #region Public Mehtods


        /// <summary>
        /// This method allows you to write to a given channel resource
        /// </summary>
        /// <param name="channel">A channel name to write to (e.g. channel1)</param>
        /// <param name="resource">A resource name in the given channel to write to (e.g. resource1)</param>
        /// <param name="data">Data to be written to the given resource</param>
        /// <param name="timeStamp">Time stamp associated to the record to write.
        /// The time stamp represents milliseconds since epoch.
        /// This parameter defaults to the current time when left blank</param>
        /// <returns>Boolean value. True if the operation was successful, False in the otherwise</returns>
        public bool Write(string channel, string resource, object data, int timeStamp = 0)
        {
            var message = new PersistentMessage(channel, resource, data, timeStamp);
            return GetResponse(message, true);
        }

        /// <summary>
        /// This method allows you to publish data to a given channel resource
        /// </summary>
        /// <param name="channel">A channel name to publish data to (e.g. channel1)</param>
        /// <param name="resource">A resource name in the given channel to publish data to (e.g. resource1)</param>
        /// <param name="data">Data to be published to the given resource.</param>
        /// <param name="timestamp">Time stamp associated to the record to publish.
        /// The time stamp represents milliseconds since epoch.
        /// This parameter defaults to the current time when left blank</param>
        /// /// <param name="isPrivatechannel">Indicates if the channel is private.</param>
        /// <returns>Boolean value. True if the operation was successful, False in the otherwise</returns>
        public bool Publish(string channel, string resource, object data, int timestamp = 0, bool isPrivateChannel = false)
        {
            channel = isPrivateChannel ? String.Format("private-{0}", channel) : channel;
            var message = new TransientMessage(channel, resource, data, timestamp);
            return GetResponse(message, true);
        }

        /// <summary>
        /// This method allows you to write multiple records in one API call
        /// </summary>
        /// <param name="channel">A channel name to write to (e.g. channel1)</param>
        /// <param name="messages">List of ResourceMessage representing the data records to be written to the given channel</param>
        /// <returns>List of ResourceRecord</returns>
        public List<ResourceRecord> WriteBulk(string channel, List<ResourceData> messages)
        {
            var bulkMessage = new BulkPersistentMessage(channel) { Records = messages };
            return GetMultiResponse(bulkMessage, true);
        }

        /// <summary>
        /// This method allows you to publish multiple data records in one API call
        /// </summary>
        /// <param name="channel">A channel name to write to (e.g. channel1)</param>
        /// <param name="messages">List of ResourceMessage representing the data records to be published to the given channel.</param>
        /// <param name="isPrivatechannel">Indicates if the channel is private.</param>
        /// <returns>List of ResourceRecord</returns>
        public bool PublishBulk(string channel, List<ResourceData> messages, bool isPrivateChannel = false)
        {
            channel = isPrivateChannel ? String.Format("private-{0}", channel) : channel;
            var bulkMessage = new BulkTransientMessage(channel) { Records = messages };
            return GetResponse(bulkMessage, true);
        }

        /// <summary>
        /// This method allows you to retrieve a user object
        /// </summary>
        /// <param name="channel">A channel name to read from (e.g. channel1)</param>
        /// <param name="resource">A resource name in the given channel to read from (e.g. resource1)</param>
        /// <param name="limit">Maximum number of resource records to return</param>
        /// <param name="source">Indicates the source where to read data from (live or statistics)
        /// It accepts the following values:
        /// raw: Instructs to read from the live records database
        /// hour-stats: Instructs to read from the hour based statistics
        /// day-stats: Instructs to read from the day based statistics</param>
        /// <param name="timeRange">Indicates the time range for the returned data. If source parameter is not set, the following rule applies.
        /// if the time range is less or equal to 6hours the default source would be live.
        /// if the time range is less or equal to 30 days, the default source would be hour-stats.
        /// if the time range is larger than 30 days then the default source would be day-stats.
        /// If the source parameter is specified the data source will be the specified one. Beware that requesting records for the last 6month from the live source might include a large number of records. In this case, the limit parameter will apply.
        /// It accepts the following values:
        /// Xhour: indicates the last X hours.
        /// Xday: indicates last X days
        /// today: today
        /// yesterday: yesterday
        /// Xweek: last X weeks
        /// current-week: current week
        /// last-week: last week
        /// Xmonth: last X month
        /// current-month: this month
        /// last-month: last month
        /// ytd: year to date</param>
        /// <returns>List of ResourceRecord</returns>
        public List<ResourceRecord> Read(string channel, string resource, int limit = 0, string source = "", string timeRange = "")
        {
            var message = new PrivateMessage(channel, resource, limit, source, timeRange);
            return GetMultiResponse(message, true);
        }

        /// <summary>
        /// Read data from a public resource
        /// </summary>
        /// <param name="username">A user name (e.g. beebotte)</param>
        /// <param name="channel">A channel name to read from (e.g. channel1)</param>
        /// <param name="resource">A resource name in the given channel to read from (e.g. resource1)</param>
        /// <param name="limit">Maximum number of resource records to return</param>
        /// <param name="source">Indicates the source where to read data from (live or statistics)
        /// It accepts the following values:
        /// raw: Instructs to read from the live records database
        /// hour-stats: Instructs to read from the hour based statistics
        /// day-stats: Instructs to read from the day based statistics</param>
        /// <param name="timeRange">Indicates the time range for the returned data. If source parameter is not set, the following rule applies.
        /// if the time range is less or equal to 6hours the default source would be live.
        /// if the time range is less or equal to 30 days, the default source would be hour-stats.
        /// if the time range is larger than 30 days then the default source would be day-stats.
        /// If the source parameter is specified the data source will be the specified one. Beware that requesting records for the last 6month from the live source might include a large number of records. In this case, the limit parameter will apply.
        /// It accepts the following values:
        /// Xhour: indicates the last X hours.
        /// Xday: indicates last X days
        /// today: today
        /// yesterday: yesterday
        /// Xweek: last X weeks
        /// current-week: current week
        /// last-week: last week
        /// Xmonth: last X month
        /// current-month: this month
        /// last-month: last month
        /// ytd: year to date</param>
        /// <returns>List of ResourceRecord</returns>
        public List<ResourceRecord> PublicRead(string username, string channel, string resource, int limit = 0, string source = "", string timeRange = "")
        {
            var message = new PublicMessage(username, channel, resource, limit, source, timeRange);
            return GetMultiResponse(message, true);
        }

        /// <summary>
        /// This method allows you to create a new channel
        /// </summary>
        /// <param name="channel">object of type Beebotte.API.Server.Net.Channel containing the channel details</param>
        /// <returns>Boolean value. true if the operation was successful, false in the otherwise.</returns>
        public bool CreateChannel(Channel channel)
        {
            channel.SetCreateMode();
            return GetResponse(channel, false);
        }

        /// <summary>
        /// This method allows you to get the model of the given channel
        /// </summary>
        /// <param name="name">A channel name to get (e.g. channel1)</param>
        /// <returns>object of type Beebotte.API.Server.Net.Channel representing the model of the given channel</returns>
        public Channel GetChannel(string name)
        {
            var channel = new Channel(name);
            channel.SetGetMode();
            return JsonHelper.JsonDeserialize<Channel>(SendRequest(channel, false));
        }

        /// <summary>
        /// This method allows you to get the models of all the channels you have created
        /// </summary>
        /// <returns>List of Beebotte.API.Server.Net.Channel</returns>
        public List<Channel> GetAllChannels()
        {
            var channel = new Channel();
            channel.SetGetAllMode();
            return JsonHelper.JsonDeserialize<List<Channel>>(SendRequest(channel, false));
        }

        /// <summary>
        /// This method allows you to delete a channel. Beware, you will loose all data associated with the channel.
        /// </summary>
        /// <param name="name">A channel name to delete (e.g. channel1)</param>
        /// <returns>Boolean value. true if the operation was successful, false in the otherwise.</returns>
        public bool DeleteChannel(string name)
        {
            var channel = new Channel(name);
            channel.SetDeleteMode();
            bool result;
            Boolean.TryParse(SendRequest(channel, false), out result);
            return result;
        }

        /// <summary>
        /// This method allows you to get the models of all the public channels  of a given user
        /// </summary>
        /// <param name="owner">The owner of the public channels</param>
        /// <returns>List of Beebotte.API.Server.Net.Channel</returns>
        public List<Channel> GetPublicChannels(string owner)
        {
            var channel = new Channel();
            channel.Owner = owner;
            channel.SetPublicReadAllMode();
            return JsonHelper.JsonDeserialize<List<Channel>>(SendRequest(channel, false));
        }

        /// <summary>
        /// This method allows you to get the model of a public channel
        /// </summary>
        /// <param name="owner">The owner of the channel</param>
        /// <param name="channelName">A channel name to get (e.g. channel1)</param>
        /// <returns>object of type Beebotte.API.Server.Net.Channel representing the model of the given channel</returns>
        public Channel GetPublicChannel(string owner, string channelName)
        {
            var channel = new Channel(owner, channelName);
            channel.SetPublicReadMode();
            return JsonHelper.JsonDeserialize<Channel>(SendRequest(channel, false));
        }

        /// <summary>
        /// This method allows you to add a new resource to a channel
        /// </summary>
        /// <param name="resource">object of type Beebotte.API.Server.Net.Resource representing the resource to create</param>
        /// <returns>Boolean value. true if the operation was successful, false in the otherwise.</returns>
        public bool CreateResource(Resource resource)
        {
            resource.SetCreateMode();
            return GetResponse(resource, false);
        }

        /// <summary>
        /// This method allows you to get the model of a resource
        /// </summary>
        /// <param name="channel">A channel name to get resource from (e.g. channel1)</param>
        /// <param name="name">A resource name to get (e.g. resource1)</param>
        /// <returns>object of type Beebotte.API.Server.Net.Resource representing the resource model</returns>
        public Resource GetResource(string channel, string name)
        {
            var resource = new Resource(channel, name);
            resource.SetGetMode();
            var response = SendRequest(resource, false);
            return JsonConvert.DeserializeObject<Resource>(response);
        }

        /// <summary>
        /// This method allows you to get the models of all the resources of a channel
        /// </summary>
        /// <param name="channel">A channel name to get resources from (e.g. channel1)</param>
        /// <returns>List of Beebotte.API.Server.Net.Source</returns>
        public List<Resource> GetAllResources(string channel)
        {
            var resource = new Resource(channel);
            resource.SetGetAllMode();
            return JsonConvert.DeserializeObject<List<Resource>>(SendRequest(resource, false));
        }

        /// <summary>
        /// This method allows you to delete a resource. Beware, you will loose all data associated with the resource.
        /// </summary>
        /// <param name="channel">A channel name (e.g. channel1)</param>
        /// <param name="name">A resource name to delete (e.g. resource1)</param>
        /// <returns>Boolean value. True if the operation was successful, False in the otherwise.</returns>
        public bool DeleteResource(string channel, string name)
        {
            var resource = new Resource(channel, name);
            resource.SetDeleteMode();
            bool result;
            Boolean.TryParse(SendRequest(resource, false), out result);
            return result;
        }

        /// <summary>
        /// This method allows you to get all connections including active subscriptions.
        /// </summary>
        /// <returns>List of Beebotte.API.Server.Net.Connection</returns>
        public List<Connection<T>> GetAllConnections<T>()
        {
            var connection = new Connection<T>();
            connection.SetGetAllMode();
            List<Connection<T>> connections = JsonHelper.JsonDeserialize<List<Connection<T>>>(SendRequest(connection, false));
            return connections;
        }

        /// <summary>
        /// This method allows you to all the connections of a given user
        /// </summary>
        /// <param name="userId">Id of the user t get the connections for</param>
        /// <param name="sessionId">Id of the session to get the connections for</param>
        /// <returns>List of Beebotte.API.Server.Net.Connection</returns>
        public List<Connection<T>> GetUserConnections<T>(string userId, string sessionId = "")
        {
            var connection = new Connection<T>();
            connection.SetGetMode(userId, sessionId);
            return JsonHelper.JsonDeserialize<List<Connection<T>>>(SendRequest(connection, false));
        }

        /// <summary>
        /// This method allows you to drop active connections of the given user. 
        /// If a session id is specified , only the specified session will be dropped (this is because one userid may have multiple connections)
        /// </summary>
        /// <param name="userId">Id of the user to drop connection for</param>
        /// <param name="sessionId">Id of the session to drop</param>
        /// <returns>Boolean value. True if the operation was successful, False in the otherwise.</returns>
        public void DeleteConnection<T>(string userId, string sessionId = "")
        {
            var connection = new Connection<T>();
            connection.SetDeleteMode(userId, sessionId);
            SendRequest(connection, false);
        }

        /// <summary>
        /// This method allows you to create a new IAM token
        /// </summary>
        /// <param name="iamtoken">object of type Beebotte.API.Server.Net.IAMToken containing the IAM token details</param>
        /// <returns>object of type Beebotte.API.Server.Net.IAMToken representing the token created</returns>
        public IAMToken CreateIAMToken(IAMToken iamtoken)
        {
            iamtoken.SetCreateMode();
            var response = GetResponseValue(iamtoken, false);
            return JsonConvert.DeserializeObject<IAMToken>(response);
        }

        /// <summary>
        /// This method allows you to update an existing IAM token with a new ACL list
        /// </summary>
        /// <param name="tokenId">the Id of the token that you want to update</param>
        /// <param name="aclList">List of Beebotte.API.Server.Net.ACL representing the new set of ACL rules</param>
        /// <returns>object of type Beebotte.API.Server.Net.IAMToken representing the updated token</returns>
        public IAMToken UpdateIAMToken(string tokenId, List<ACL> aclList)
        {
            IAMToken token = new IAMToken();
            token.ID = tokenId;
            token.ACLList = aclList;
            token.SetUpdateMode(tokenId);
            return JsonConvert.DeserializeObject<IAMToken>(GetResponseValue(token, false));
        }


        /// <summary>
        /// This method allows you to get the IAM token given its ID
        /// </summary>
        /// <param name="iamTokenId">The ID of the IAM token to get</param>
        /// <returns>object of type Beebotte.API.Server.Net.IAMToken representing the IAM token</returns>
        public IAMToken GetIAMToken(string iamTokenId)
        {
            var iamToken = new IAMToken(iamTokenId);
            iamToken.SetGetMode();
            var response = SendRequest(iamToken, false);
            return JsonConvert.DeserializeObject<IAMToken>(response);
        }

        /// <summary>
        /// This method allows you to delete an IAM token.
        /// </summary>
        /// <param name="iamTokenId">The ID of the IAM token to delete</param>
        /// <returns>Boolean value. true if the operation was successful, false in the otherwise.</returns>
        public bool DeleteIAMToken(string iamTokenId)
        {
            var iamToken = new IAMToken(iamTokenId);
            iamToken.SetDeleteMode();
            bool result;
            Boolean.TryParse(SendRequest(iamToken, false), out result);
            return result;
        }

        /// <summary>
        /// This method allows you to get all the IAM tokens
        /// </summary>
        /// <returns>List of Beebotte.API.Server.Net.IAMToken</returns>
        public List<IAMToken> GetAllIAMTokens()
        {
            var iamToken = new IAMToken();
            iamToken.SetGetAllMode();
            var response = SendRequest(iamToken, false);
            return JsonConvert.DeserializeObject<List<IAMToken>>(response);
        }

        /// <summary>
        /// This method allows you to create a new BeeRule
        /// </summary>
        /// <param name="rule">object of type Beebotte.API.Server.Net.BeeRule containing the BeeRule details</param>
        /// <returns>object of type Beebotte.API.Server.Net.BeeRule containing the BeeRule created.</returns>
        public BeeRule CreateBeeRule(BeeRule rule)
        {
            rule.SetCreateMode();
            var response = GetResponseValue(rule, false);
            //JObject o = JObject.Parse(response);
            //string name = (string)o["_id"];
            //return name;
            return JsonConvert.DeserializeObject<BeeRule>(response);
        }

        /// <summary>
        /// This method allows you to delete a BeeRule.
        /// </summary>
        /// <param name="beeRuleId">the ID of the BeeRule to delete</param>
        /// <returns>Boolean value. true if the operation was successful, false in the otherwise.</returns>
        public bool DeleteBeeRule(string beeRuleId)
        {
            var beerule = new BeeRule(beeRuleId);
            beerule.SetDeleteMode();
            bool result;
            Boolean.TryParse(SendRequest(beerule, false), out result);
            return result;
        }

        /// <summary>
        /// This method allows you to get a BeeRule given its ID
        /// </summary>
        /// <param name="ruleId">the ID of the BeeRule to get</param>
        /// <returns>object of type Beebotte.API.Server.Net.BeeRule representing the BeeRule</returns>
        public BeeRule GetBeeRule(string ruleId)
        {
            var rule = new BeeRule(ruleId);
            rule.SetGetMode();
            var response = SendRequest(rule, false);
            return JsonConvert.DeserializeObject<BeeRule>(response);
        }

        /// <summary>
        /// This method allows to retrieval of Beerules of the user satisfying query on the rule trigger.
        /// </summary>
        /// <param name="action">the BeeRule action</param>
        /// <param name="trigger">The BeeRule trigger type</param>
        /// <param name="channel">The BeeRule trigger channel</param>
        /// <param name="resource">The BeeRule trigger resource</param>
        /// <returns>List of Beebotte.API.Server.Net.BeeRule</returns>
        public List<BeeRule> GetBeeRules(string action, string trigger, string channel, string resource)
        {
            if (!Enum.IsDefined(typeof(ActionTypes), action) || !Enum.IsDefined(typeof(TriggerTypes), trigger) ||
                !Regex.IsMatch(channel, Constants.TriggerChannelSchema) || !Regex.IsMatch(resource, Constants.TriggerResourceSchema)
                )
                throw (new InvalidParameterSchemaException("GetBeeRules", String.Format("action:{0}, trigger:{1}, channel:{2}, resource:{3}", action, trigger, channel, resource)));
            BeeRule rule = new BeeRule(action, trigger, channel, resource);
            rule.SetGetAllMode();
            var response = SendRequest(rule, false);
            return JsonConvert.DeserializeObject<List<BeeRule>>(response);

        }

        /// <summary>
        /// This methos allows you to invoke a Beerule given its identifier.
        /// </summary>
        /// <param name="beeRuleID">The ID of the BeeRule to invoke</param>
        /// <param name="beeRuleInvocation">object of type Beebotte.API.Server.Net.BeeRuleInvocation containing the BeeRule Invocation details</param>
        /// <returns>Boolean value. true if the operation was successful, false in the otherwise.</returns>
        public bool InvokeBeeRule(string beeRuleID, BeeRuleInvocation beeRuleInvocation)
        {
            beeRuleInvocation._beeruleId = beeRuleID;
            beeRuleInvocation.SetCreateMode();
            var response = GetResponseValue(beeRuleInvocation, false);
            bool result;
            Boolean.TryParse(response, out result);
            return result;
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the multi response.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>List&lt;ResourceRecord&gt;.</returns>
        /// <exception cref="InvalidParameterSchemaException"></exception>
        private List<ResourceRecord> GetMultiResponse(RequestBase message, bool supporChannelTokenAuth)
        {
            if (!message.ValidateSchema())
                throw (new InvalidParameterSchemaException(OperationUri.Write.ToString(), message.SerializedContent));
            return ResourceRecord.Deserialize(SendRequest(message, supporChannelTokenAuth));
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="InvalidParameterSchemaException"></exception>
        private bool GetResponse(RequestBase message, bool supporChannelTokenAuth)
        {
            if (!message.ValidateSchema())
                throw (new InvalidParameterSchemaException(String.Format("Method: {0}. Error Message: {1}", message.Uri, message.ModelSchemaError)));
            bool result;
            Boolean.TryParse(SendRequest(message, supporChannelTokenAuth), out result);
            return result;
        }

        private string GetResponseValue(RequestBase message, bool supporChannelTokenAuth)
        {
            if (!message.ValidateSchema())
                throw (new InvalidParameterSchemaException(String.Format("Method: {0}. Error Message: {1}", message.Uri, message.ModelSchemaError)));
            return SendRequest(message, supporChannelTokenAuth);
        }

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>System.String.</returns>
        private string SendRequest(RequestBase message, bool supporChannelTokenAuth)
        {
            _date = DateTime.UtcNow;
            var url = Utilities.GenerateUrl(Protocol, Hostname, Port, message.Uri);
            var headers = new Dictionary<string, string>();
            if (message.RequireAuthentication)
            {
                if (String.Equals(AuthenticationType, AuthenticationTypes.Token.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!supporChannelTokenAuth)
                        throw new NotSupportedException("Method Not Allowed Error: This method is not allowed for Channel Token based authentication! You must provide your Access and Secret Keys, or your IAM token instead!'");
                    headers.Add("X-Auth-Token", Token);
                }
                else if (String.Equals(AuthenticationType, AuthenticationTypes.IAMToken.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    headers.Add("X-Auth-Token", Token);
                }

                else
                {
                    var signature = Utilities.GenerateHMACHash(message.GenerateStringToSign(_date), SecureKey);
                    headers.Add("Authorization", String.Format("{0}:{1}", AccessKey, signature));
                }
            }

            if (!String.IsNullOrEmpty(message.HashedContent))
            {
                headers.Add("Content-MD5", message.HashedContent);
            }

            var client = new RestClient(url, message.Verb,
                                        String.Equals(message.Verb, HttpVerb.GET.ToString(),
                                                      StringComparison.InvariantCultureIgnoreCase)
                                            ? String.Empty
                                            : message.SerializedContent, Constants.JsonContentType, _date);
            var response = client.MakeRequest(String.Empty, headers);
            return response;
        }

        #endregion
    }
}
