﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beebotte.API.Server.Net
{
    //  MIT License
    //  
    //  Copyright (c) 2017 Emmanuel Counasse
    internal class JsonSubtypesConverterBuilder
    {
        private Type _baseType;
        private string _discriminatorProperty;
        private readonly Dictionary<object, Type> _subTypeMapping = new Dictionary<object, Type>();
        private bool _serializeDiscriminatorProperty;
        private bool _addDiscriminatorFirst;

        public static JsonSubtypesConverterBuilder Of(Type baseType, string discriminatorProperty)
        {
            var customConverterBuilder = new JsonSubtypesConverterBuilder
            {
                _baseType = baseType,
                _discriminatorProperty = discriminatorProperty
            };
            return customConverterBuilder;
        }

        public JsonSubtypesConverterBuilder SerializeDiscriminatorProperty()
        {
            return SerializeDiscriminatorProperty(false);
        }

        public JsonSubtypesConverterBuilder SerializeDiscriminatorProperty(bool addDiscriminatorFirst)
        {
            _serializeDiscriminatorProperty = true;
            _addDiscriminatorFirst = addDiscriminatorFirst;
            return this;
        }

        public JsonSubtypesConverterBuilder RegisterSubtype(Type subtype, object value)
        {
            _subTypeMapping.Add(value, subtype);
            return this;
        }

        public JsonConverter Build()
        {
            return new JsonSubtypesConverter(_baseType, _discriminatorProperty, _subTypeMapping, _serializeDiscriminatorProperty, _addDiscriminatorFirst);
        }
    }
}
