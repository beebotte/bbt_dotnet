// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-16-2014
// ***********************************************************************
// <copyright file="Channel.cs" company="Beebotte">
//     Copyright (c) Beebotte. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


/// <summary>
/// The Net namespace.
/// </summary>
namespace Beebotte.API.Server.Net
{
    /// <summary>
    /// A Channel represents a logical entity that can be (1) a physical object as a demotic sensor connected to an Arduino or a RaspBerry Pi, or, (2) a virtual object as a Blog, Twitter account, web service, etc.
    /// A channel belongs to one and a unique account, it is expected to connect to Beebotte to write data. It can also connect to read data from the platform.
    /// A channel can be created by the user; it has a unique name that will be used to identify it, a description, an array of resources and an access level indicating if it is public or private.
    /// The channel concept in Beebotte is rather abstract. It is required to be connected to the Internet (through Ethernet, WiFi, 3G, ZigBee, etc.) and expected to read and write to the Beebotte Cloud platform.
    /// </summary>
    [DataContract]
    public class Channel : EntityBase
    {
        #region Fields

        /// <summary>
        /// The _serialized message
        /// </summary>
        private string _serializedMessage;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        [DataMember(IsRequired = true, Name = "ispublic", Order = 4)]
        public bool IsPublic { get; set; }

        /// <summary>
        /// List of Beebotte.API.Server.Net.Resource representing the resources associated with the channel
        /// </summary>
        /// <value>The resources.</value>
        [DataMember(IsRequired = false, EmitDefaultValue = false, Name = "resources", Order = 5)]
        public List<Resource> Resources { get; set; }

        /// <summary>
        /// Represents the user who created the channel
        /// </summary>
        /// <value>The owner.</value>
        [DataMember(IsRequired = false, EmitDefaultValue = false, Name = "owner", Order = 6)]
        public string Owner { get; set; }

        /// <summary>
        /// Gets the base URI.
        /// </summary>
        /// <value>The base URI.</value>
        protected override string BaseUri
        {
            get
            {
                return OperationUri.ManageChannel.GetOperationUri();
            }
        }

        /// <summary>
        /// Gets the base URI for public methods
        /// </summary>
        protected override string PublicBaseUri
        {
            get
            {
                return String.Concat(OperationUri.ReadPublicChannel.GetOperationUri(), Owner);
            }
        }

        /// <summary>
        /// Gets the content of the serialized.
        /// </summary>
        /// <value>The content of the serialized.</value>
        internal override string SerializedContent
        {
            get
            {
                if (String.IsNullOrEmpty(_serializedMessage))
                {
                    _serializedMessage = JsonHelper.JsonSerialize(this);
                }
                return _serializedMessage;
            }
        }

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Channel"/> class.
        /// </summary>
        public Channel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Channel"/> class.
        /// </summary>
        /// <param name="name">The name of the channel.</param>
        public Channel(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Channel"/> class.
        /// </summary>
        /// <param name="owner">The owner of the channel.</param>
        /// <param name="name">The name of the channel.</param>
        public Channel(string owner, string name)
        {
            Owner = owner;
            Name = name;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Validates the schema.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal override bool ValidateSchema()
        {

            var valid = false;
            valid = Utilities.ValidateChannelFormat(Name) && Utilities.ValidateLabelFormat(Label) && Resources != null &&
                    Resources.Count > 0;
            if (valid)
            {
                foreach (var resource in Resources)
                {
                    valid = resource.ValidateSchema();
                    if (!valid)
                        return false;
                }
            }
            return valid;
        }

        #endregion
    }
}