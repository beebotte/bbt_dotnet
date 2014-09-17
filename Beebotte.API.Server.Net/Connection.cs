using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Beebotte.API.Server.Net
{
    [DataContract]
    public class Connection <T>: RequestBase
    {
        #region Fields

        /// <summary>
        /// The _serialized message
        /// </summary>
        private string _serializedMessage;

        private string _uri;

        private string _verb;

        #endregion

        #region Properties

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "owner", Order = 1)]
        public string Owner { get; set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "sid", Order = 2)]
        public string SID { get; set; }

        [DataMember(EmitDefaultValue = true, IsRequired = false, Name = "userinfo", Order = 3)]
        public T UserInfo { get; set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "userid", Order = 4)]
        public string UserId { get; set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "subscriptions", Order = 5)]
        public List<Subscription> Subscriptions { get; set; }

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
        
        internal override string Verb
        {
            get {return _verb; }
        }

        internal override string Uri
        {
            get { return _uri; }
        }

        internal override bool RequireAuthentication
        {
            get { return true; }
        }

        #endregion

        #region ctor

        public Connection() { }
        public Connection(string Owner)
        { }

        #endregion

        #region Methods
        internal override bool ValidateSchema()
        {
            throw new NotImplementedException();
        }

        internal void SetGetAllMode()
        {
            _uri = OperationUri.ManageConnection.GetOperationUri();
            _verb = HttpVerb.GET.ToString();
        }

        internal void SetGetMode(string userId, string sessionId)
        {
            _uri = String.Concat(OperationUri.ManageConnection.GetOperationUri(), userId);
            _uri = _uri.AppendIf(String.Format("sid={0}", sessionId), !String.IsNullOrEmpty(sessionId));
            _verb = HttpVerb.GET.ToString();
        }

        internal void SetDeleteMode(string userId, string sessionId)
        {
            _uri = String.Concat(OperationUri.ManageConnection.GetOperationUri(), "drop/", userId);
            _uri = _uri.AppendIf(String.Format("sid={0}", sessionId), !String.IsNullOrEmpty(sessionId));
            _verb = HttpVerb.DELETE.ToString();
        }

        #endregion

    }

    [DataContract]
    public class UserInfo
    {
        [DataMember(EmitDefaultValue = true, IsRequired = false, Name = "username")]
        public string Username { get; set; }

        [DataMember(EmitDefaultValue = true, IsRequired = false, Name = "password")]
        public string Password { get; set; }

        [DataMember(EmitDefaultValue = true, IsRequired = false, Name = "email")]
        public string Email { get; set; }

        [DataMember(EmitDefaultValue = true, IsRequired = false, Name = "avatar")]
        public string Avatar { get; set; }

        [DataMember(EmitDefaultValue = true, IsRequired = false, Name = "url")]
        public string URL { get; set; }

        [DataMember(EmitDefaultValue = true, IsRequired = false, Name = "token")]
        public string Token { get; set; }
    }

    [DataContract]
    public class Subscription
    {
        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "channel", Order = 1)]
        public string Channel { get; set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "resource", Order = 2)]
        public string Resource { get; set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "read", Order = 3)]
        public bool Read { get; set; }

        [DataMember(EmitDefaultValue = true, IsRequired = true, Name = "write", Order = 4)]
        public bool Write { get; set; }
    }
}
