using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Beebotte.API.Server.Net.JsonSubTypes;

namespace Beebotte.API.Server.Net
{
    //  MIT License
    //  
    //  Copyright (c) 2017 Emmanuel Counasse
    internal class JsonSubtypesConverter : JsonSubtypes
    {
        private readonly bool _serializeDiscriminatorProperty;
        private readonly Dictionary<Type, object> _supportedTypes = new Dictionary<Type, object>();
        private readonly Type _baseType;
        private readonly Dictionary<object, Type> _subTypeMapping;

        [ThreadStatic] private static bool _isInsideWrite;
        [ThreadStatic] private static bool _allowNextWrite;
        private bool _addDiscriminatorFirst;

        internal JsonSubtypesConverter(Type baseType, string discriminatorProperty,
            Dictionary<object, Type> subTypeMapping, bool serializeDiscriminatorProperty, bool addDiscriminatorFirst) : base(discriminatorProperty)
        {
            _serializeDiscriminatorProperty = serializeDiscriminatorProperty;
            _baseType = baseType;
            _subTypeMapping = subTypeMapping;
            _addDiscriminatorFirst = addDiscriminatorFirst;
            foreach (var type in _subTypeMapping)
            {
                _supportedTypes.Add(type.Value, type.Key);
            }
        }

        protected override Dictionary<object, Type> GetSubTypeMapping(Type type)
        {
            return _subTypeMapping;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == _baseType || _supportedTypes.ContainsKey(objectType);
        }

        public override bool CanWrite
        {
            get
            {
                if (!_serializeDiscriminatorProperty)
                    return false;

                if (!_isInsideWrite)
                    return true;

                if (_allowNextWrite)
                {
                    _allowNextWrite = false;
                    return true;
                }

                _allowNextWrite = true;
                return false;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject jsonObj;
            _isInsideWrite = true;
            _allowNextWrite = false;
            try
            {
                jsonObj = JObject.FromObject(value, serializer);
            }
            finally
            {
                _isInsideWrite = false;
            }

            var supportedType = _supportedTypes[value.GetType()];
            var typeMappingPropertyValue = JToken.FromObject(supportedType, serializer);
            if (_addDiscriminatorFirst)
            {
                jsonObj.AddFirst(new JProperty(JsonDiscriminatorPropertyName, typeMappingPropertyValue));
            }
            else
            {
                jsonObj.Add(JsonDiscriminatorPropertyName, typeMappingPropertyValue);
            }
            jsonObj.WriteTo(writer);
        }
    }
}

