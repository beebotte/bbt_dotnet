using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Beebotte.API.Server.Net
{
    [DataContract]
    internal class TransientMessage : WriteMessageBase
    {
        #region Fields

        private string _serializedMessage;

        #endregion

        #region ctor

        internal TransientMessage(string channel, string resource, object data, int timestamp)
        {
            Channel = channel;
            Resource = resource;
            Data = data;
            TimeStamp = timestamp > 0 ? timestamp : 0;
        }

        #endregion

        #region Properties

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "data", Order = 1)]
        internal object Data { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false, Name = "ts", Order = 2)]
        internal int TimeStamp { get; set; }

        internal string Channel { get; set; }

        internal string Resource { get; set; }

        internal override string Uri
        {
            get
            {
                return String.Format("{0}/{1}/{2}", OperationUri.Publish.GetOperationUri(), Channel, Resource);
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
            return Utilities.ValidateChannelFormat(Channel) && Utilities.ValidateResourceFormat(Resource);
        }

        #endregion
    }
}
