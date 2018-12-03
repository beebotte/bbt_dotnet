using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Beebotte.API.Server.Net.JsonSubTypes;

namespace Beebotte.API.Server.Net
{
    [DataContract]
    public class IAMToken : EntityBase
    {
        #region Fields

        /// <summary>
        /// The _serialized message
        /// </summary>
        private string _serializedMessage;

        #endregion

        #region ctor

        public IAMToken() { }
        public IAMToken(string name) { Name = name; }


        #endregion



        #region Data Members

        [DataMember(Name = "token", EmitDefaultValue = false)]
        public string Token { get; set; }

        [DataMember(Name = "_id", EmitDefaultValue = false)]
        public string ID { get; set; }

        [DataMember(Name = "acl")]
        [Required, ValidateObject]
        public List<ACL> ACLList { get; set; }

        #endregion

        #region Properties

        protected override string BaseUri
        {
            get
            {
                return OperationUri.ManageIAMTokens.GetOperationUri();
            }

        }

        protected override string PublicBaseUri => throw new NotImplementedException();

        /// <summary>
        /// Gets the content of the serialized.
        /// </summary>
        /// <value>The content of the serialized.</value>
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
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(this, context, results, true);
            if (!isValid)
            {
                ModelSchemaError = results.ToDescErrorsString();
            }
            return isValid;
        }

        #endregion
    }
    [JsonConverter(typeof(JsonSubtypes), "%action")]
    [JsonSubtypes.KnownSubType(typeof(AdminACL), "admin:")]
    [JsonSubtypes.KnownSubType(typeof(DataACL), "data:")]
    [DataContract]
    public abstract class ACL
    {
        [DataMember(IsRequired = true, Name = "action")]
        public abstract string Action { get; set; }
    }

    [DataContract]
    public class AdminACL : ACL
    {
        [DataMember(IsRequired = true, Name = "action")]
        [Required, AdminACLActionAttribute]
        public override string Action { get; set; }
    }

    [DataContract]
    public class DataACL : ACL
    {
        [DataMember(IsRequired = true, Name = "action")]
        [Required, DataACLActionAttribute]
        public override string Action { get; set; }

        [DataMember(IsRequired = true, Name = "resource")]
        [Required, ACLResourcesAttribute(ErrorMessage = "Invalid resource schema")]
        public List<string> Resources { get; set; }
    }
}

