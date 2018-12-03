// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-16-2014
// ***********************************************************************
// <copyright file="RequestBase.cs" company="Beebotte">
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
    /// Represents the base class of Beebotte Request
    /// </summary>
    [DataContract]
    public abstract class RequestBase
    {
        /// <summary>
        /// The _hashed content
        /// </summary>
        private string _hashedContent;

        #region Properties

        /// <summary>
        /// Gets the Http verb.
        /// </summary>
        /// <value>The verb.</value>
        internal abstract string Verb { get; }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>The URI.</value>
        internal abstract string Uri { get; }

        /// <summary>
        /// Checks if request requires authentication or not.
        /// </summary>
        /// <value>Boolean value. True if request requires authentication; false in the otherwise.</value>
        internal abstract bool RequireAuthentication { get; }

        /// <summary>
        /// Gets the content of the hashed.
        /// </summary>
        /// <value>The content of the hashed.</value>
        internal string HashedContent
        {
            get
            {
                if (String.IsNullOrEmpty(_hashedContent) &&
                    String.Equals(Verb, HttpVerb.POST.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    _hashedContent = Utilities.GenerateMD5Hash(SerializedContent);
                }
                return _hashedContent;
            }
        }

        internal string ModelSchemaError { get; set; }

        /// <summary>
        /// Gets the content of the serialized.
        /// </summary>
        /// <value>The content of the serialized.</value>
        internal abstract string SerializedContent
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generates the string to sign.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>System.String.</returns>
        internal string GenerateStringToSign(DateTime date)
        {
            return String.Concat(Verb, "\n", HashedContent, "\n",
                               Constants.JsonContentType, "\n", date.ToString("r"), "\n", Uri);
        }

        /// <summary>
        /// Validates the schema.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal abstract bool ValidateSchema();

        #endregion
    }
}
