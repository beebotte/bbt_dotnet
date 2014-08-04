// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-01-2014
// ***********************************************************************
// <copyright file="ExceptionBase.cs" company="Beebotte">
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
    /// Represents base class for Beebotte custom exceptions
    /// </summary>
    public abstract class ExceptionBase : Exception
    {
        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionBase" /> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ExceptionBase(string message)
            : base(message)
        {
        }

        #endregion
    }
}
