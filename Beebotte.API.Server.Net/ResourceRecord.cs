// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-01-2014
// ***********************************************************************
// <copyright file="ResourceRecord.cs" company="Beebotte">
//     Copyright (c) Beebotte. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// The Net namespace.
/// </summary>
namespace Beebotte.API.Server.Net
{
    /// <summary>
    /// Represents a Resource record as returned by Beebotte
    /// </summary>
    [DataContract]
    public class ResourceRecord 
    {
        #region Properties

        /// <summary>
        /// Gets or sets the resource name.
        /// </summary>
        /// <value>The resource name.</value>
        [DataMember(Name = "resource", Order = 1)]
        public string Resource { get; set; }

        /// <summary>
        /// Gets or sets the resource data.
        /// </summary>
        /// <value>The data.</value>
        [DataMember(Name = "data", Order = 2)]
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the time span.
        /// </summary>
        /// <value>The time span.</value>
        [DataMember(Name = "ts", Order = 3)]
        public double TimeSpan { get; set; }

        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>The device identifier.</value>
        [DataMember(Name = "device_id", Order = 4)]
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the resource type.
        /// </summary>
        /// <value>The resource type.</value>
        [DataMember(Name = "type", Order = 5)]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DataMember(Name = "_id", Order = 6)]
        public string Id { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deserializes the specified json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>List&lt;ResourceRecord&gt;.</returns>
        internal static List<ResourceRecord> Deserialize(string json)
        {
            return JsonHelper.JsonDeserialize<List<ResourceRecord>>(json);
        }

        #endregion
    }
}
