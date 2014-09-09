// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-16-2014
// ***********************************************************************
// <copyright file="EntityBase.cs" company="Beebotte">
//     Copyright (c) Beebotte. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Runtime.Serialization;

/// <summary>
/// The Net namespace.
/// </summary>
namespace Beebotte.API.Server.Net
{
    /// <summary>
    /// Represents the base class of Beebotte entity such as Resource and Channel
    /// </summary>
    [DataContract]
    public abstract class EntityBase : RequestBase
    {
        #region Fields

        /// <summary>
        /// Boolean, True if entity request requires authentication from Beebotte; False in the otherwise.
        /// </summary>
        private bool _requireAuthentication;
        /// <summary>
        /// The _verb
        /// </summary>
        private string _verb;
        /// <summary>
        /// The _uri
        /// </summary>
        private string _uri;

        #endregion

        #region Properties

        /// <summary>
        /// The name of the entity. (e.g. channel1, resource1)
        /// </summary>
        /// <value>The name.</value>
        [DataMember(IsRequired = true, Name = "name", Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// User friendly label for the entity. If not provided, the entity name will be used as label as well
        /// </summary>
        /// <value>The label.</value>
        [DataMember(IsRequired = false, EmitDefaultValue = false, Name = "label", Order = 2)]
        public string Label { get; set; }

        /// <summary>
        /// Description of the entity. Free text
        /// </summary>
        /// <value>The description.</value>
        [DataMember(IsRequired = false, EmitDefaultValue = false, Name = "description", Order = 3)]
        public string Description { get; set; }

        internal override bool RequireAuthentication
        {
            get { return _requireAuthentication; }
        }

        /// <summary>
        /// Gets the verb.
        /// </summary>
        /// <value>The verb.</value>
        internal override string Verb
        {
            get { return _verb; }
        }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>The URI.</value>
        internal override string Uri
        {
            get { return _uri; }
        }

        /// <summary>
        /// Gets the base URI.
        /// </summary>
        /// <value>The base URI.</value>
        protected abstract string BaseUri { get; }


        /// <summary>
        /// Gets the base URI for public methods.
        /// </summary>
        /// <value>The base URI.</value>
        protected abstract string PublicBaseUri { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the create mode.
        /// </summary>
        internal void SetCreateMode()
        {
            _verb = HttpVerb.POST.ToString();
            _uri = BaseUri;
            _requireAuthentication = true;
        }

        /// <summary>
        /// Sets the get mode.
        /// </summary>
        internal void SetGetMode()
        {
            _verb = HttpVerb.GET.ToString();
            _uri = String.Format("{0}/{1}", BaseUri, Name);
            _requireAuthentication = true;
        }

        /// <summary>
        /// Sets the get all mode.
        /// </summary>
        internal void SetGetAllMode()
        {
            _verb = HttpVerb.GET.ToString();
            _uri = BaseUri;
            _requireAuthentication = true;
        }

        /// <summary>
        /// Sets the delete mode.
        /// </summary>
        internal void SetDeleteMode()
        {
            _verb = HttpVerb.DELETE.ToString();
            _uri = String.Format("{0}/{1}", BaseUri, Name);
            _requireAuthentication = true;
        }

        /// <summary>
        /// Sets the public Read all mode.
        /// </summary>
        internal void SetPublicReadAllMode()
        {
            _verb = HttpVerb.GET.ToString();
            _uri = PublicBaseUri;
            _requireAuthentication = true;
        }

        /// <summary>
        /// Sets the public Read all mode.
        /// </summary>
        internal void SetPublicReadMode()
        {
            _verb = HttpVerb.GET.ToString();
            _uri = String.Format("{0}/{1}", PublicBaseUri, Name);
            _requireAuthentication = false;
        }

        #endregion
    }
}
