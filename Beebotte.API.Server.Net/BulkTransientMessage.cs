using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Beebotte.API.Server.Net
{
    [DataContract]
    internal class BulkTransientMessage: WriteMessageBase
    {
        #region Fields

        private string _serializedMessage;

        #endregion

        #region ctor

        public BulkTransientMessage(string channel)
        {
            Channel = channel;
            Records = new List<ResourceData>();
        }

        #endregion

        #region Properties

        internal string Channel { get; set; }

        [DataMember(EmitDefaultValue = false, IsRequired = false, Name = "records", Order = 0)]
        internal List<ResourceData> Records { get; set; }

        internal override string Uri
        {
            get { return String.Format("{0}/{1}", OperationUri.Publish.GetOperationUri(), Channel); }
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

        internal void Add(string resource, object data, int timeStamp, string source)
        {
            var message = new ResourceData(resource, data, timeStamp, source);
            Records.Add(message);
        }

        internal void Add(string resource, object data, string source)
        {
            var message = new ResourceData(resource, data, source);
            Records.Add(message);
        }

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
