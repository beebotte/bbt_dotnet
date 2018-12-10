// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-16-2014
// ***********************************************************************
// <copyright file="Utilities.cs" company="Beebotte">
//     Copyright (c) Beebotte. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// The Net namespace.
/// </summary>
namespace Beebotte.API.Server.Net
{
    /// <summary>
    /// Helper class used througout the system
    /// </summary>
    public static class Utilities
    {
        #region Common Methods

        /// <summary>
        /// Gets the description attribute of an enum value if found, else the name of the enum value.
        /// </summary>
        /// <param name="value">The enum value</param>
        /// <returns>The value of the description attribute if found else the name of the enum value</returns>
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }


        public static class EnumUtil
        {
            public static IEnumerable<T> GetValues<T>()
            {
                return Enum.GetValues(typeof(T)).Cast<T>();
            }
        }

        /// <summary>
        /// Gets the operation URI.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string GetOperationUri(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value.ToString() : String.Format(attribute.Description, Connector.APIVersion);
        }


        /// <summary>
        /// Converts an enum to a dictionary
        /// </summary>
        /// <param name="enumType">The enum value</param>
        /// <returns>A dictionary of the enum's value and description</returns>
        public static Dictionary<int, string> ToDictionary(this Type enumType)
        {
            return Enum.GetValues(enumType)
                       .Cast<object>()
                       .ToDictionary(k => (int)k, v => ((Enum)v).GetDescription());

        }

        /// <summary>
        /// Constructs a full Url given the protocol, the hostname, the port and the uri
        /// </summary>
        /// <param name="protocol">Name of the protocol (e.g. http)</param>
        /// <param name="hostname">API hostname (e.g. api.beebotte.com)</param>
        /// <param name="port">Internet socket port (e.g. 222)</param>
        /// <param name="uri">Path of the api (e.g. /Channels)</param>
        /// <returns>System.String.</returns>
        public static string GenerateUrl(string protocol, string hostname, int port, string uri)
        {
            return port.Equals(80) ? String.Format("{0}://{1}{2}", protocol, hostname, uri) : String.Format("{0}://{1}:{2}{3}", protocol, hostname, port, uri);
        }

        /// <summary>
        /// Generates an HMAC hash for a given string using a security key
        /// </summary>
        /// <param name="stringToSign">String to sign</param>
        /// <param name="secureKey">Security key</param>
        /// <returns>HMAC hash of the string</returns>
        public static string GenerateHMACHash(string stringToSign, string secureKey)
        {
            var ae = new UTF8Encoding();
            var keyByte = ae.GetBytes(secureKey);
            var data = ae.GetBytes(stringToSign);
            var hmacsha1 = new HMACSHA1(keyByte);
            var digest = hmacsha1.ComputeHash(data);
            return Convert.ToBase64String(digest);
        }

        /// <summary>
        /// Generates an MD5 hash for a given string
        /// </summary>
        /// <param name="stringToHash">String to hash</param>
        /// <returns>MD5 Hash value of the string</returns>
        public static string GenerateMD5Hash(string stringToHash)
        {
            var encoding = new UTF8Encoding();
            var inputBytes = encoding.GetBytes(stringToHash);
            var md5Hasher = new MD5CryptoServiceProvider();
            var hashedDataBytes = md5Hasher.ComputeHash(inputBytes);
            var output = Convert.ToBase64String(hashedDataBytes, 0, hashedDataBytes.Length);
            return output;
        }

        /// <summary>
        /// Appends a querystring parameter to a Url based on a condition.
        /// </summary>
        /// <param name="url">The original Url.</param>
        /// <param name="param">The querystring parameter to append to the original Url</param>
        /// <param name="condition">if set to <c>true</c> append param to the url</param>
        /// <returns>System.String. representing the full url</returns>
        public static string AppendIf(this string url, string param, bool condition)
        {
            return !condition ? url : String.Format(url.Contains("?") ? "{0}&{1}" : "{0}?{1}", url, param);
        }


        internal static string GetUserAgent()
        {
            return String.Format("beebotte .Net SDK v{0}", Constants.SDKVersion);
        }

        #endregion

        #region Format Validation 

        /// <summary>
        /// Validates the channel format.
        /// </summary>
        /// <param name="channel">The name of the channel to validate</param>
        /// <returns><c>true</c> if channel name is not null and is less than or equal to 30 characters, <c>false</c> otherwise.</returns>
        public static bool ValidateChannelFormat(string channel)
        {
            return !String.IsNullOrEmpty(channel) && Regex.IsMatch(channel, @"^[\s\S]{0,30}$");
        }

        /// <summary>
        /// Validates the resource format.
        /// </summary>
        /// <param name="resource">The name of the resource to validate</param>
        /// <returns><c>true</c> if the resource name is between 2 and 30 characters, <c>false</c> otherwise.</returns>
        public static bool ValidateResourceFormat(string resource)
        {
            return !String.IsNullOrEmpty(resource) && Regex.IsMatch(resource, @"^[\s\S]{2,30}$");
        }

        /// <summary>
        /// Validates the label format.
        /// </summary>
        /// <param name="label">The label value.</param>
        /// <returns><c>true</c> if label value is less than or equal to 30 characters, <c>false</c> otherwise.</returns>
        public static bool ValidateLabelFormat(string label)
        {
            return String.IsNullOrEmpty(label) || Regex.IsMatch(label, @"^[\s\S]{0,30}$");
        }

        /// <summary>
        /// Validates the source format.
        /// </summary>
        /// <param name="source">The source value.</param>
        /// <returns><c>true</c> if source value is one of 'raw', 'hour-stats' and 'day-stats', <c>false</c> otherwise.</returns>
        public static bool ValidateSourceFormat(string source)
        {
            return String.IsNullOrEmpty(source) || source.Equals(Constants.Raw) || source.Equals(Constants.DayStats) || source.Equals(Constants.HourStats);
        }

        /// <summary>
        /// Validates the time range format.
        /// </summary>
        /// <param name="timeRange">The time range.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ValidateTimeRangeFormat(string timeRange)
        {
            //TODO add validation
            return String.IsNullOrEmpty(timeRange);
        }

        /// <summary>
        /// Validates the type format.
        /// </summary>
        /// <param name="type">The type value.</param>
        /// <returns><c>true</c> if type value is one of Beebotte.API.Server.Net.BBTDataType enum description values, <c>false</c> otherwise.</returns>
        public static bool ValidateTypeFormat(string type)
        {
            if (!String.IsNullOrEmpty(type))
            {
                var fields = typeof(BBTDataType).GetFields();
                return (from field in fields
                        let attribute =
                            Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute
                        select attribute == null ? field.ToString() : attribute.Description).Any(
                            enumDescription =>
                            String.Equals(enumDescription, type, StringComparison.InvariantCultureIgnoreCase));
            }
            return false;
        }



        #endregion

        #region Extension Methods

        public static string ToDescErrorsString(this IEnumerable<ValidationResult> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source), $"The property {nameof(source)}, has null value");

            StringBuilder result = new StringBuilder();

            if (source.Count() > 0)
            {
                result.AppendLine("Schema validation errors:");
                source.ToList()
                    .ForEach(
                        s =>
                            result.AppendFormat("  {0} --> {1}{2}", s.MemberNames.FirstOrDefault(), s.ErrorMessage,
                                Environment.NewLine));
            }
            else
                result.AppendLine(string.Empty);

            return result.ToString();
        }

        #endregion

    }
}
