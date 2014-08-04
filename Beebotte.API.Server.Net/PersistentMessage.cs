using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Beebotte.API.Server.Net
{
    [DataContract]
    internal class PersistentMessage : WriteMessageBase
    {
        #region Fields

        private string _serializedMessage;

        #endregion

        #region ctor

        internal PersistentMessage(string channel, string resource, object data, int timeStamp)
        {
            Channel = channel;
            Resource = resource;
            Data = data;
            TimeStamp = timeStamp > 0 ? timeStamp : 0;
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
            get { return String.Format("{0}/{1}/{2}", OperationUri.Write.GetOperationUri(), Channel, Resource); }
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
                        Data != null;
        }

        #endregion
    }
}
