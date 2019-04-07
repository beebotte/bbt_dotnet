// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-15-2014
// ***********************************************************************
// <copyright file="Constants.cs" company="Beebotte">
//     Copyright (c) Beebotte. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;
using System.Runtime.Serialization;

/// <summary>
/// The Net namespace.
/// </summary>
namespace Beebotte.API.Server.Net
{
    /// <summary>
    /// Enum Beebotte Rest API operations URIs
    /// </summary>
    public enum OperationUri
    {
        /// <summary>
        /// Write operation Uri
        /// </summary>
        [Description("/{0}/data/write")]
        Write,
        /// <summary>
        /// Public Read operation Uri
        /// </summary>
        [Description("/{0}/public/data/read")]
        PublicRead,
        /// <summary>
        /// Private Read operation Uri
        /// </summary>
        [Description("/{0}/data/read")]
        PrivateRead,
        /// <summary>
        /// Publish operation Uri
        /// </summary>
        [Description("/{0}/data/publish")]
        Publish,
        /// <summary>
        /// Channel management operations Uri
        /// </summary>
        [Description("/{0}/channels")]
        ManageChannel,
        /// <summary>
        /// Resource management operations Uri
        /// </summary>
        [Description("/{0}/channels/channelName/resources")]
        ManageResource,
        [Description("/{0}/public/channels/")]
        ReadPublicChannel,
        [Description("/{0}/connections/")]
        ManageConnection,
        [Description("/{0}/beerules")]
        ManageBeeRule,
        [Description("/{0}/iamtokens")]
        ManageIAMTokens,
        [Description("/{0}/beerules")]
        InvokeBeeRule,
    }

    /// <summary>
    /// Enum Http verbs
    /// </summary>
    public enum HttpVerb
    {
        /// <summary>
        /// The get verb
        /// </summary>
        GET,
        /// <summary>
        /// The post verb
        /// </summary>
        POST,
        /// <summary>
        /// The put verb
        /// </summary>
        PUT,
        /// <summary>
        /// The delete verb
        /// </summary>
        DELETE
    }


    /// <summary>
    /// Enum Beebotte data types
    /// </summary>
    public enum BBTDataType
    {
        /// <summary>
        /// Any
        /// </summary>
        [Description("any")]
        Any,
        /// <summary>
        /// The number
        /// </summary>
        [Description("number")]
        Number,
        /// <summary>
        /// The string
        /// </summary>
        [Description("string")]
        String,
        /// <summary>
        /// The object
        /// </summary>
        [Description("object")]
        Object,
        /// <summary>
        /// The function
        /// </summary>
        [Description("function")]
        Function,
        /// <summary>
        /// The array
        /// </summary>
        [Description("array")]
        Array,
        /// <summary>
        /// The alphabetic
        /// </summary>
        [Description("alphabetic")]
        Alphabetic,
        /// <summary>
        /// The alphanumeric
        /// </summary>
        [Description("alphanumeric")]
        Alphanumeric,
        /// <summary>
        /// The decimal
        /// </summary>
        [Description("decimal")]
        Decimal,
        /// <summary>
        /// The rate
        /// </summary>
        [Description("rate")]
        Rate,
        /// <summary>
        /// The percentage
        /// </summary>
        [Description("percentage")]
        Percentage,
        /// <summary>
        /// The email
        /// </summary>
        [Description("email")]
        Email,
        /// <summary>
        /// The GPS
        /// </summary>
        [Description("gps")]
        GPS,
        /// <summary>
        /// The cpu
        /// </summary>
        [Description("cpu")]
        CPU,
        /// <summary>
        /// The memory
        /// </summary>
        [Description("memory")]
        Memory,
        /// <summary>
        /// The netif
        /// </summary>
        [Description("netif")]
        Netif,
        /// <summary>
        /// The disk
        /// </summary>
        [Description("disk")]
        Disk,
        /// <summary>
        /// The temperature
        /// </summary>
        [Description("temperature")]
        Temperature,
        /// <summary>
        /// The humidity
        /// </summary>
        [Description("humidity")]
        Humidity,
        /// <summary>
        /// The body temperature
        /// </summary>
        [Description("body_temp")]
        BodyTemperature,

        /// <summary>
        /// boolean
        /// </summary>
        [Description("boolean")]
        Boolean
    }
    public enum TriggerTypes
    {
        connect,
        disconnect,
        subscribe,
        unsubscribe,
        join,
        leave,
        publish,
        write
    }

    public enum ActionTypes
    {
        publish,
        write,
        sms, // reserved for future use
        email, // reserved for future use
        webhook
    }

    public enum AuthenticationTypes
    {
        APIKeys,
        Token,
        IAMToken
    }

    public enum AdminACLTypes
    {
        [Description("admin:connection:read")]
        ConnectionRead,
        [Description("admin:connection:write")]
        ConnectionWrite,
        [Description("admin:channel:read")]
        ChannelRead,
        [Description("admin:channel:write")]
        ChannelWrite,
        [Description("admin:beerule:read")]
        BeeRuleRead,
        [Description("admin:beerule:write")]
        BeeRuleWrite,
        [Description("admin:iam:read")]
        IAMRead,
        [Description("admin:iam:write")]
        IAMWrite
    }

    public enum DataACLTypes
    {
        [Description("data:read")]
        DataRead,
        [Description("data:write")]
        DataWrite
    }


    /// <summary>
    /// Contains the constants used throughout the system
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The json content type
        /// </summary>
        public const string JsonContentType = "application/json";
        /// <summary>
        /// The default protocol
        /// </summary>
        public const string DefaultProtocol = "https";
        /// <summary>
        /// The default host name
        /// </summary>
        public const string DefaultHostName = "api.beebotte.com";
        /// <summary>
        /// The default port
        /// </summary>
        public const int DefaultPort = 80;
        /// <summary>
        /// The raw source value
        /// </summary>
        public const string Raw = "raw";
        /// <summary>
        /// The hour stats source value
        /// </summary>
        public const string HourStats = "hour-stats";
        /// <summary>
        /// The day stats source value
        /// </summary>
        public const string DayStats = "day-stats";
        /// <summary>
        /// The default API version
        /// </summary>
        public const string DefaultVersion = "v1";

        public const string ChannelResourceSchema = @"^\w+$";
        public const string PrivateChannelSchema = @"^(private-)?\w+$";
        public const string TriggerChannelSchema = @"^(\*|(private-)?(\w\w+))$";

        public const string TriggerResourceSchema = @"^(\*|\w\w+)$";

        public const string IAMTokenPrefix = "iamtkn_";
        public const string SDKVersion = "2.2.1";
    }
}
