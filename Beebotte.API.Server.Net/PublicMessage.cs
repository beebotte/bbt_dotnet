using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beebotte.API.Server.Net
{
    internal class PublicMessage : ReadMessageBase
    {
        #region Fields

        private string _serializedMessage;

        #endregion

        #region ctor

        internal PublicMessage(string username, string channel, string resource, int limit, string source, string timeRange)
            : base(channel, resource, limit, source, timeRange)
        {
            Username = username;
        }


        #endregion

        #region Properties

        internal string Username { get; set; }

        internal override string Uri
        {
            get
            {
                return String.Format("{0}/{1}/{2}/{3}?limit={4}&source={5}&time-range={6}",
                                     OperationUri.PublicRead.GetOperationUri(), Username, Channel, Resource, Limit, Source, TimeRange);
            }
        }

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

        #region Methods

        internal override bool ValidateSchema()
        {
            return !String.IsNullOrEmpty(Username) && Utilities.ValidateChannelFormat(Channel) &&
                   Utilities.ValidateResourceFormat(Resource) && Limit.CompareTo(0) >= 0 &&
                   Utilities.ValidateSourceFormat(Source);
        }

        #endregion
    }
}
