using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Beebotte.API.Server.Net
{
    [DataContract]
    internal class BulkPersistentMessage : WriteMessageBase
    {
        #region Fields

        private string _serializedMessage;

        #endregion

        #region ctor

        internal BulkPersistentMessage(string channel)
        {
            Channel = channel;
            Records = new List<ResourceData>();
        }

        #endregion

        #region Properties

        internal void Add(string resource, object data, int timespan)
        {
            var message = new ResourceData(resource, data, timespan);
            Records.Add(message);
        }

        internal void Add(string resource, object data)
        {
            var message = new ResourceData(resource, data);
            Records.Add(message);
        }

        internal string Channel { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false, Name = "records", Order = 0)]
        internal List<ResourceData> Records { get; set; }

        internal override string Uri
        {
            get { return String.Format("{0}/{1}", OperationUri.Write.GetOperationUri(), Channel); }
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

            var valid = Utilities.ValidateChannelFormat(Channel) && Records != null && Records.Count > 0;
            if (valid)
                foreach (var message in Records)
                {
                    valid = message.ValidateSchema();
                    if (!valid)
                        break;
                }

            return valid;
        }


        #endregion
    }
}
