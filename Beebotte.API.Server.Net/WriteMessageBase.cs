// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-16-2014
// ***********************************************************************
// <copyright file="WriteMessageBase.cs" company="Beebotte">
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
    /// Represents the base class of Beebotte write message request
    /// </summary>
    [DataContract]
    public abstract class WriteMessageBase : RequestBase
    {
        /// <summary>
        /// Gets the Http verb.
        /// </summary>
        /// <value>The verb.</value>
        internal override string Verb
        {
            get { return HttpVerb.POST.ToString(); }
        }
    }
}
