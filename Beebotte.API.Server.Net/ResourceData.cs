// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-16-2014
// ***********************************************************************
// <copyright file="ResourceMessage.cs" company="Beebotte">
//     Copyright (c) Beebotte. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/// <summary>
/// The Net namespace.
/// </summary>
namespace Beebotte.API.Server.Net
{
    /// <summary>
    /// Represents resource data as sent to Beebotte. This class is used with bulk write and publish operations.
    /// </summary>
    [DataContract]
    public class ResourceData 
    {
        #region Properties

        /// <summary>
        /// Gets or sets the resource name.
        /// </summary>
        /// <value>The resource name.</value>
        [DataMember(EmitDefaultValue = false, IsRequired = false, Name = "resource", Order = 1)]
        public string Resource { get; set; }

        /// <summary>
        /// Gets or sets the resource data.
        /// </summary>
        /// <value>The resource data.</value>
        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "data", Order = 2)]
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>The time stamp.</value>
        [DataMember(EmitDefaultValue = false, IsRequired = false, Name = "ts", Order = 3)]
        public int TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [DataMember(EmitDefaultValue = false, IsRequired = false, Name = "source", Order = 3)]
        public string Source { get; set; }

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceData"/> class.
        /// </summary>
        /// <param name="resource">The resource name.</param>
        /// <param name="data">The data.</param>
        /// <param name="timestamp">The timestamp.</param>
        public ResourceData(string resource, object data, int timestamp)
            : this(resource, data)
        {
            TimeStamp = timestamp > 0 ? timestamp : 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceData"/> class.
        /// </summary>
        /// <param name="resource">The resource name.</param>
        /// <param name="data">The data.</param>
        public ResourceData(string resource, object data)
        {
            Resource = resource;
            Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceData"/> class.
        /// </summary>
        /// <param name="resource">The resource name.</param>
        /// <param name="data">The data.</param>
        /// <param name="source">The source.</param>
        public ResourceData(string resource, object data, string source)
            : this(resource, data)
        {
            Source = source;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceData"/> class.
        /// </summary>
        /// <param name="resource">The resource name.</param>
        /// <param name="data">The data.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="source">The source.</param>
        public ResourceData(string resource, object data, int timestamp, string source)
            : this(resource, data, timestamp)
        {
            Source = source;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validates the schema.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool ValidateSchema()
        {
            var valid =
                Utilities.ValidateResourceFormat(Resource) && Data != null;
            return valid;
        }

        #endregion
    }
}
