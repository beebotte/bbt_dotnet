using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Beebotte.API.Server.Net
{
    public class ValidateObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var results = new List<ValidationResult>();
                if (value is IEnumerable)
                {

                    foreach (var obj in value as IEnumerable)
                    {
                        var context = new ValidationContext(obj, null, null);

                        Validator.TryValidateObject(obj, context, results, true);

                        if (results.Count != 0)
                        {
                            var compositeResults = new CompositeValidationResult(String.Format("Invalid schema for {0}", validationContext.DisplayName));
                            results.ForEach(compositeResults.AddResult);
                            return compositeResults;
                        }
                    }

                }
                else
                {
                    var context = new ValidationContext(value, null, null);
                    Validator.TryValidateObject(value, context, results, true);

                    if (results.Count != 0)
                    {
                        var compositeResults = new CompositeValidationResult(String.Format("Invalid schema for {0}", validationContext.DisplayName));
                        results.ForEach(compositeResults.AddResult);

                        return compositeResults;
                    }
                }
            }
            return ValidationResult.Success;
        }
    }

    public class CompositeValidationResult : ValidationResult
    {
        private readonly List<ValidationResult> _results = new List<ValidationResult>();

        public IEnumerable<ValidationResult> Results
        {
            get
            {
                return _results;
            }
        }

        public CompositeValidationResult(string errorMessage) : base(errorMessage) { }
        public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) { }
        protected CompositeValidationResult(ValidationResult validationResult) : base(validationResult) { }

        public void AddResult(ValidationResult validationResult)
        {
            _results.Add(validationResult);
        }
    }

    [AttributeUsage(AttributeTargets.Property |
    AttributeTargets.Field, AllowMultiple = false)]
    sealed public class UriAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Uri.IsWellFormedUriString(value.ToString(), UriKind.Absolute);
        }
    }

    [AttributeUsage(AttributeTargets.Property |
    AttributeTargets.Field, AllowMultiple = false)]
    sealed public class TriggerTypeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Enum.IsDefined(typeof(TriggerTypes), value);
        }
    }

    [AttributeUsage(AttributeTargets.Property |
    AttributeTargets.Field, AllowMultiple = false)]
    sealed public class AdminACLActionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool valid = false;
            foreach (AdminACLTypes t in Enum.GetValues(typeof(AdminACLTypes)))
            {
                if (String.Equals(t.GetDescription(), value.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    valid = true;
                    break;
                }
            }
            return valid;
        }
    }


    [AttributeUsage(AttributeTargets.Property |
   AttributeTargets.Field, AllowMultiple = false)]
    sealed public class DataACLActionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool valid = false;
            foreach (DataACLTypes t in Enum.GetValues(typeof(DataACLTypes)))
            {
                if (String.Equals(t.GetDescription(), value.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    valid = true;
                    break;
                }
            }
            return valid;
        }
    }

    [AttributeUsage(AttributeTargets.Property |
  AttributeTargets.Field, AllowMultiple = false)]
    sealed public class ACLResourcesAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            var list = value as List<string>;
            var valid = list != null && list.Count > 0;
            if (valid)
            {
                foreach (var r in list)
                {
                    if (!Regex.IsMatch(r, @"^((\*|(\w\w+))(\.(\*|(\w\w+)))?)$") || r.Length > 129)
                    {
                        valid = false;
                        break;
                    }

                }
            }
            return valid;
        }
    }
}
