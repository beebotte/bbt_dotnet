// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-01-2014
// ***********************************************************************
// <copyright file="MissingParameterException.cs" company="Beebotte">
//     Copyright (c) Beebotte. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
/// <summary>
/// The Net namespace.
/// </summary>
namespace Beebotte.API.Server.Net
{
    /// <summary>
    /// Represents missing method parameter exception 
    /// </summary>
    public class MissingParameterException : ExceptionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionBase" /> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public MissingParameterException(string message) : base(message)
        {
        }
    }
}
