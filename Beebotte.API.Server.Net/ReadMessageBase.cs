// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-16-2014
// ***********************************************************************
// <copyright file="ReadMessageBase.cs" company="Beebotte">
//     Copyright (c) Beebotte. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// The Net namespace.
/// </summary>
namespace Beebotte.API.Server.Net
{
    /// <summary>
    /// Represents the base class of Beebotte read message request
    /// </summary>
    public abstract class ReadMessageBase : RequestBase
    {
        /// <summary>
        /// A channel name to read from (e.g. channel1)
        /// </summary>
        /// <value>The channel.</value>
        public string Channel { get; set; }

        /// <summary>
        /// A resource name in the given channel to read from (e.g. resource1)
        /// </summary>
        /// <value>The resource.</value>
        public string Resource { get; set; }

        /// <summary>
        /// Maximum number of resource records to return
        /// </summary>
        /// <value>The limit.</value>
        public int Limit { get; set; }

        /// <summary>
        /// Indicates the source where to read data from (live or statistics)
        /// It accepts the following values:
        /// raw: Instructs to read from the live records database
        /// hour-stats: Instructs to read from the hour based statistics
        /// day-stats: Instructs to read from the day based statistics
        /// </summary>
        /// <value>The source.</value>
        public string Source { get; set; }

        /// <summary>
        /// Indicates the time range for the returned data. If source parameter is not set, the following rule applies.
        /// if the time range is less or equal to 6hours the default source would be live.
        /// if the time range is less or equal to 30 days, the default source would be hour-stats.
        /// if the time range is larger than 30 days then the default source would be day-stats.
        /// If the source parameter is specified the data source will be the specified one. Beware that requesting records for the last 6month from the live source might include a large number of records. In this case, the limit parameter will apply.
        /// It accepts the following values:
        /// Xhour: indicates the last X hours.
        /// Xday: indicates last X days
        /// today: today
        /// yesterday: yesterday
        /// Xweek: last X weeks
        /// current-week: current week
        /// last-week: last week
        /// Xmonth: last X month
        /// current-month: this month
        /// last-month: last month
        /// ytd: year to date
        /// </summary>
        /// <value>The time range.</value>
        public string TimeRange { get; set; }

        /// <summary>
        /// Gets the verb.
        /// </summary>
        /// <value>The verb.</value>
        internal override string Verb
        {
            get { return HttpVerb.GET.ToString(); }
        }


        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMessageBase"/> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="source">The source.</param>
        /// <param name="timeRange">The time range.</param>
        protected ReadMessageBase(string channel, string resource, int limit, string source, string timeRange)
            : this(channel, resource)
        {
            Limit = limit;
            if (!String.IsNullOrEmpty(source))
                Source = source;
            if (!String.IsNullOrEmpty(timeRange))
                TimeRange = timeRange;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMessageBase"/> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="resource">The resource.</param>
        protected ReadMessageBase(string channel, string resource)
        {
            Channel = channel;
            Resource = resource;
        }

        #endregion
    }
}
