﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Beebotte.API.Server.Net
{
    internal class PrivateMessage : ReadMessageBase
    {

        #region Fields

        private string _uri;
        private string _serializedMessage;

        #endregion

        #region ctor

        public PrivateMessage(string channel, string resource, int limit, string source, string timeRange)
            : base(channel, resource, limit, source, timeRange)
        {
        }


        #endregion

        #region Properties

        internal override string Uri
        {
            get
            {
                if (String.IsNullOrEmpty(_uri))
                {
                    _uri = String.Format("{0}/{1}/{2}",
                                         OperationUri.PrivateRead.GetOperationUri(), Channel, Resource);
                    _uri = _uri.AppendIf(String.Format("limit={0}", Limit), Limit > 0);
                    _uri = _uri.AppendIf(String.Format("source={0}", Source), !String.IsNullOrEmpty(Source));
                    _uri = _uri.AppendIf(String.Format("time-range={0}", TimeRange), !String.IsNullOrEmpty(TimeRange));
                }
                return _uri;
            }
        }

        internal override bool RequireAuthentication
        {
            get { return true; }
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
            return Utilities.ValidateChannelFormat(Channel) && Utilities.ValidateResourceFormat(Resource) &&
                   Limit.CompareTo(0) >= 0 &&
                   Utilities.ValidateSourceFormat(Source);
        }

        #endregion
    }
}
