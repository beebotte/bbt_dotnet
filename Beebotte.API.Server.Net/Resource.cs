// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-16-2014
// ***********************************************************************
// <copyright file="Resource.cs" company="Beebotte">
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
    /// A resource belongs to a channel, it provides the data to record in the platform.
    /// For example, a temperature sensor of a weather station (channel) will emit a temperature event (resource) to record in the platform.
    /// A resource can be an attribute like the temperature, the humidity level, etc. It can also be an event like to indicate low battery level.
    /// It can also be a function like a face identification service where the user sends an image and expects the number of human faces in return.
    /// </summary>
    [DataContract]
    public class Resource : EntityBase
    {
        #region Fields

        /// <summary>
        /// The _serialized message
        /// </summary>
        private string _serializedMessage;
        /// <summary>
        /// The _channel
        /// </summary>
        private string _channel;

        #endregion

        #region Properties
        /// <summary>
        /// Indicates the data type for the resource. Can be one of ['any', 'number', 'string', 'object'].
        /// </summary>
        /// <value>The type.</value>
        [DataMember(IsRequired = true, Name = "vtype", Order = 4)]
        public string Type { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Resource" /> is inherit.
        /// </summary>
        /// <value><c>true</c> if inherit; otherwise, <c>false</c>.</value>
        [DataMember(IsRequired = false, EmitDefaultValue = false, Name = "inherit", Order = 5)]
        public bool Inherit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        [DataMember(IsRequired = false, EmitDefaultValue = false, Name = "ispublic", Order = 6)]
        public bool IsPublic { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        [DataMember(IsRequired = false, EmitDefaultValue = false, Name = "channel", Order = 7)]
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        [DataMember(IsRequired = false, EmitDefaultValue = false, Name = "owner", Order = 8)]
        public string Owner { get; set; }

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

        /// <summary>
        /// Gets the base URI.
        /// </summary>
        /// <value>The base URI.</value>
        protected override string BaseUri
        {
            get { return OperationUri.ManageResource.GetOperationUri().Replace("channelName", _channel); }
        }

        protected override string PublicBaseUri
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="label">The label.</param>
        /// <param name="description">The description.</param>
        /// <param name="type">The type.</param>
        public Resource(string name, string label, string description, string type)

        {
            Name = name;
            Label = label;
            Description = description;
            Type = type;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Resource" /> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="name">The name.</param>
        /// <param name="label">The label.</param>
        /// <param name="description">The description.</param>
        /// <param name="type">The type.</param>
        public Resource(string channel, string name, string label, string description, string type)
            : this(channel, name, type)
        {
            Label = label;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource" /> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        public Resource(string channel, string name, string type): this(channel, name)
        {
            Name = name;
            _channel = channel;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource" /> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="name">The name.</param>
        public Resource(string channel, string name):this(channel)
        {
            Name = name ;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource" /> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public Resource(string channel)
        {
            _channel = channel;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Validates the schema.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal override bool ValidateSchema()
        {
            return Utilities.ValidateResourceFormat(Name) && Utilities.ValidateLabelFormat(Label) &&
                   Utilities.ValidateTypeFormat(Type);
        }

        #endregion

    }
}
