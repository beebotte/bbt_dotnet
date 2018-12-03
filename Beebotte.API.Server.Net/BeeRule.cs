using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using static Beebotte.API.Server.Net.JsonSubTypes;

namespace Beebotte.API.Server.Net
{
    [DataContract]
    public class BeeRule : EntityBase
    {
        #region Fields

        /// <summary>
        /// The _serialized message
        /// </summary>
        private string _serializedMessage;
        private string _action = String.Empty;
        private string _event = String.Empty;
        private string _channel = String.Empty;
        private string _resource = String.Empty;

        #endregion

        #region ctor

        public BeeRule(string name)
        {
            Name = name;
        }

        public BeeRule(string action, string trigger, string channel, string resource)
        {
            _action = action;
            _event = trigger;
            _channel = channel;
            _resource = resource;
        }

        public BeeRule()
        {
        }

        #endregion

        #region Properties

        [DataMember(EmitDefaultValue = false, Name = "_id")]
        public string ID { get; set; }


        [DataMember(Name = "trigger", Order = 3)]
        [Required, ValidateObject]
        public Trigger Trigger { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Name = "condition", Order = 4)]
        public string Condition { get; set; }

        [DataMember(IsRequired = true, Name = "action", Order = 5)]
        [Required, ValidateObject]
        public Action Action { get; set; }

        /// <summary>
        /// Gets the base URI.
        /// </summary>
        /// <value>The base URI.</value>
        protected override string BaseUri
        {
            get
            {
                if (!String.IsNullOrEmpty(_action) || !String.IsNullOrEmpty(_event) || !String.IsNullOrEmpty(_channel) || !String.IsNullOrEmpty(_resource))
                {
                    var uri = OperationUri.ManageBeeRule.GetOperationUri();
                    uri = uri.AppendIf(String.Format("action={0}", _action), !String.IsNullOrEmpty(_action));
                    uri = uri.AppendIf(String.Format("event={0}", _event), !String.IsNullOrEmpty(_event));
                    uri = uri.AppendIf(String.Format("channel={0}", _channel), !String.IsNullOrEmpty(_channel));
                    uri = uri.AppendIf(String.Format("resource={0}", _resource), !String.IsNullOrEmpty(_resource));
                }
                return Action != null && !String.IsNullOrEmpty(Action.Type) ? String.Format("{0}/{1}", OperationUri.ManageBeeRule.GetOperationUri(), Action.Type) :
                    OperationUri.ManageBeeRule.GetOperationUri();
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

    [DataContract]
    public class Trigger
    {
        #region Fields
        private string _event = String.Empty;
        #endregion

        #region Properties

        [DataMember(IsRequired = true, Name = "event")]
        [Required, TriggerTypeAttribute]
        public string Event { get; set; }


        [DataMember(IsRequired = true, Name = "channel")]
        [RegularExpression(Constants.TriggerChannelSchema, ErrorMessage = "Invalid trigger schema - Invalid channel name")]
        [Required, StringLength(64)]
        public string Channel { get; set; }

        [DataMember(IsRequired = true, Name = "resource")]
        [RegularExpression(Constants.TriggerResourceSchema, ErrorMessage = "Invalid trigger schema - Invalid resource name")]
        [Required, StringLength(64)]
        public string Resource { get; set; }

        #endregion

    }

    [DataContract(IsReference = false)]
    [KnownType(typeof(WebhookAction))]
    [KnownType(typeof(PublishAction))]
    [KnownType(typeof(WriteAction))]
    [KnownType(typeof(SMSAction))]
    [KnownType(typeof(EmailAction))]
    [JsonConverter(typeof(JsonSubtypes), "type")]
    [JsonSubtypes.KnownSubType(typeof(PublishAction), "publish")]
    [JsonSubtypes.KnownSubType(typeof(WriteAction), "write")]
    [JsonSubtypes.KnownSubType(typeof(SMSAction), "sms")]
    [JsonSubtypes.KnownSubType(typeof(EmailAction), "email")]
    [JsonSubtypes.KnownSubType(typeof(WebhookAction), "webhook")]
    public abstract class Action
    {
        [DataMember(IsRequired = true, Name = "type")]
        public abstract string Type { get; set; }
    }

    [DataContract]
    public class PublishAction : Action
    {
        #region Properties

        [DataMember(IsRequired = true, Name = "type")]
        public override string Type { get { return "publish"; } set { } }

        [DataMember(IsRequired = true, Name = "channel")]
        [RegularExpression(Constants.PrivateChannelSchema, ErrorMessage = "Invalid action schema - Invalid channel name")]
        [Required, StringLength(64, MinimumLength = 2)]
        public string Channel { get; set; }

        [DataMember(IsRequired = true, Name = "resource")]
        [RegularExpression(Constants.ChannelResourceSchema, ErrorMessage = "Invalid action schema - Invalid resource name")]
        [Required, StringLength(64, MinimumLength = 2)]
        public string Resource { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Name = "value")]
        public string Value { get; set; }

        #endregion
    }

    [DataContract]
    public class WriteAction : Action
    {
        #region Properties

        [DataMember(IsRequired = true, Name = "type")]
        public override string Type
        {
            get { return ActionTypes.write.ToString(); }
            set
            { }
        }

        [DataMember(IsRequired = true, Name = "channel")]
        [RegularExpression(Constants.ChannelResourceSchema, ErrorMessage = "Invalid action schema - Invalid channel name")]
        [Required, StringLength(64, MinimumLength = 2)]
        public string Channel { get; set; }

        [DataMember(IsRequired = true, Name = "resource")]
        [RegularExpression(Constants.ChannelResourceSchema, ErrorMessage = "Invalid action schema - Invalid resource name")]
        [Required, StringLength(64, MinimumLength = 2)]
        public string Resource { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Name = "value")]
        public string Value { get; set; }

        #endregion
    }

    [DataContract]
    public class SMSAction : Action
    {
        #region Properties

        [DataMember(IsRequired = true, Name = "type")]
        public override string Type
        {
            get { return "sms"; }
            set
            { }
        }


        [DataMember(IsRequired = false, Name = "to")]
        [Required]
        public string To { get; set; }

        #endregion
    }

    [DataContract]
    public class EmailAction : Action
    {
        #region Properties

        [DataMember(IsRequired = true, Name = "type")]
        public override string Type
        {
            get { return "email"; }
            set
            { }
        }

        [DataMember(IsRequired = false, Name = "to")]
        [Required]
        [RegularExpression(@"^[a-z0-9](\.?[a-z0-9_-]){0,}@[a-z0-9-]+\.([a-z]{1,6}\.)?[a-z]{2,6}$", ErrorMessage = "Invalid Email Action schema - Invalid email address")]

        public string To { get; set; }

        #endregion
    }

    [DataContract]

    public class WebhookAction : Action
    {
        #region Properties

        [DataMember(IsRequired = true, Name = "type")]
        public override string Type
        {
            get { return "webhook"; }
            set
            { }
        }


        [DataMember(IsRequired = false, Name = "endpoint")]
        [Required, UriAttribute]
        public string Endpoint { get; set; }


        #endregion
    }

}



