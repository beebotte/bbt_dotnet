using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Beebotte.API.Server.Net
{
    [DataContract]
    public class BeeRuleInvocation : EntityBase
    {
        #region Fields

        /// <summary>
        /// The _serialized message
        /// </summary>
        private string _serializedMessage;
        internal string _beeruleId;

        #endregion


        [DataMember(IsRequired = true, Name = "channel")]
        [RegularExpression(Constants.PrivateChannelSchema, ErrorMessage = "Invalid BeeRuleInvocation schema - Invalid channel name")]
        [Required, StringLength(64, MinimumLength = 2)]
        public string Channel { get; set; }

        [DataMember(IsRequired = true, Name = "resource")]
        [RegularExpression(Constants.ChannelResourceSchema, ErrorMessage = "Invalid BeeRuleInvocation schema - Invalid resource name")]
        [Required, StringLength(64, MinimumLength = 2)]
        public string Resource { get; set; }

        [DataMember(EmitDefaultValue = false, Name = "ispublic")]
        public bool IsPublic { get; set; }

        [DataMember(EmitDefaultValue = false, Name = "clientid")]
        public string ClientID { get; set; }

        [DataMember(EmitDefaultValue = false, Name = "data")]
        public dynamic Data { get; set; }

        [DataMember(EmitDefaultValue = false, Name = "debug")]
        public bool Debug { get; set; }

        protected override string BaseUri
        {
            get
            {
                return String.Format("{0}/{1}/invoke", OperationUri.InvokeBeeRule.GetOperationUri(), _beeruleId);
            }
        }

        protected override string PublicBaseUri => throw new NotImplementedException();

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
    }
}

