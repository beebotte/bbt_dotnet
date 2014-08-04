// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-01-2014
// ***********************************************************************
// <copyright file="InvalidParameterSchemaException.cs" company="Beebotte">
//     Copyright (c) Beebotte. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

/// <summary>
/// The Net namespace.
/// </summary>
namespace Beebotte.API.Server.Net
{
    /// <summary>
    /// Represents invalid parameter schema exception.
    /// </summary>
    public class InvalidParameterSchemaException : ExceptionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionBase" /> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public InvalidParameterSchemaException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidParameterSchemaException"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="parameters">The parameters.</param>
        public InvalidParameterSchemaException(string method, string parameters)
            : base(
                string.Format(String.Format(@"Invalid parameters schema. Method: {0}. Parameters {1}", method, parameters.Replace("{" , "{{").Replace("}", "}}"))))
            
        {
        }

    }
}