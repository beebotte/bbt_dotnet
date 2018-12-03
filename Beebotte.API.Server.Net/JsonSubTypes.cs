﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Beebotte.API.Server.Net
{
    //  MIT License
    //  
    //  Copyright (c) 2017 Emmanuel Counasse
    internal class JsonSubTypes
    {
        public class JsonSubtypes : JsonConverter
        {
            [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
            public class KnownSubTypeAttribute : Attribute
            {
                public Type SubType { get; private set; }
                public object AssociatedValue { get; private set; }

                public KnownSubTypeAttribute(Type subType, object associatedValue)
                {
                    SubType = subType;
                    AssociatedValue = associatedValue;
                }
            }

            [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
            public class KnownSubTypeWithPropertyAttribute : Attribute
            {
                public Type SubType { get; private set; }
                public string PropertyName { get; private set; }

                public KnownSubTypeWithPropertyAttribute(Type subType, string propertyName)
                {
                    SubType = subType;
                    PropertyName = propertyName;
                }
            }

            protected bool Contains { get; set; }

            protected readonly string JsonDiscriminatorPropertyName;

            [ThreadStatic] private static bool _isInsideRead;

            [ThreadStatic] private static JsonReader _reader;

            public override bool CanRead
            {
                get
                {
                    if (!_isInsideRead)
                        return true;

                    return !string.IsNullOrEmpty(_reader.Path);
                }
            }

            public override bool CanWrite
            {
                get { return false; }
            }

            public JsonSubtypes()
            {
            }

            public JsonSubtypes(string jsonDiscriminatorPropertyName)
            {
                Contains = jsonDiscriminatorPropertyName.StartsWith("%");
                JsonDiscriminatorPropertyName = Contains ? jsonDiscriminatorPropertyName.TrimStart('%') : jsonDiscriminatorPropertyName;
            }

            public override bool CanConvert(Type objectType)
            {
                return false;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                JsonSerializer serializer)
            {
                return ReadJson(reader, objectType, serializer);
            }

            private object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer)
            {
                while (reader.TokenType == JsonToken.Comment)
                    reader.Read();

                object value;
                switch (reader.TokenType)
                {
                    case JsonToken.Null:
                        value = null;
                        break;
                    case JsonToken.StartObject:
                        value = ReadObject(reader, objectType, serializer);
                        break;
                    case JsonToken.StartArray:
                        value = ReadArray(reader, objectType, serializer);
                        break;
                    default:
                        var lineNumber = 0;
                        var linePosition = 0;
                        var lineInfo = reader as IJsonLineInfo;
                        if (lineInfo != null && lineInfo.HasLineInfo())
                        {
                            lineNumber = lineInfo.LineNumber;
                            linePosition = lineInfo.LinePosition;
                        }

                        throw new JsonReaderException(string.Format("Unrecognized token: {0}", reader.TokenType), reader.Path, lineNumber, linePosition, null);
                }

                return value;
            }

            private IList ReadArray(JsonReader reader, Type targetType, JsonSerializer serializer)
            {
                var elementType = GetElementType(targetType);

                var list = CreateCompatibleList(targetType, elementType);
                while (reader.Read() && reader.TokenType != JsonToken.EndArray)
                {
                    list.Add(ReadJson(reader, elementType, serializer));
                }

                if (!targetType.IsArray)
                    return list;

                var array = Array.CreateInstance(targetType.GetElementType(), list.Count);
                list.CopyTo(array, 0);
                return array;
            }

            private static IList CreateCompatibleList(Type targetContainerType, Type elementType)
            {
                var typeInfo = GetTypeInfo(targetContainerType);
                if (typeInfo.IsArray || typeInfo.IsAbstract)
                {
                    return (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
                }

                return (IList)Activator.CreateInstance(targetContainerType);
            }

            private static Type GetElementType(Type arrayOrGenericContainer)
            {
                if (arrayOrGenericContainer.IsArray)
                {
                    return arrayOrGenericContainer.GetElementType();
                }

                var genericTypeArguments = GetGenericTypeArguments(arrayOrGenericContainer);
                return genericTypeArguments.FirstOrDefault();
            }

            private object ReadObject(JsonReader reader, Type objectType, JsonSerializer serializer)
            {
                var jObject = JObject.Load(reader);

                var targetType = GetType(jObject, objectType) ?? objectType;

                return ThreadStaticReadObject(reader, serializer, jObject, targetType);
            }

            private static JsonReader CreateAnotherReader(JToken jToken, JsonReader reader)
            {
                var jObjectReader = jToken.CreateReader();
                jObjectReader.Culture = reader.Culture;
                jObjectReader.CloseInput = reader.CloseInput;
                jObjectReader.SupportMultipleContent = reader.SupportMultipleContent;
                jObjectReader.DateTimeZoneHandling = reader.DateTimeZoneHandling;
                jObjectReader.FloatParseHandling = reader.FloatParseHandling;
                jObjectReader.DateFormatString = reader.DateFormatString;
                jObjectReader.DateParseHandling = reader.DateParseHandling;
                return jObjectReader;
            }

            private Type GetType(JObject jObject, Type parentType)
            {
                if (JsonDiscriminatorPropertyName == null)
                {
                    return GetTypeByPropertyPresence(jObject, parentType);
                }
                return GetTypeFromDiscriminatorValue(jObject, parentType);
            }

            private static Type GetTypeByPropertyPresence(IDictionary<string, JToken> jObject, Type parentType)
            {
                var knownSubTypeAttributes = GetAttributes<KnownSubTypeWithPropertyAttribute>(parentType);

                return knownSubTypeAttributes
                    .Select(knownType =>
                    {
                        JToken ignore;
                        if (TryGetValueInJson(jObject, knownType.PropertyName, out ignore))
                            return knownType.SubType;

                        return null;
                    })
                    .FirstOrDefault(type => type != null);
            }

            private Type GetTypeFromDiscriminatorValue(IDictionary<string, JToken> jObject, Type parentType)
            {
                JToken discriminatorValue;
                if (!TryGetValueInJson(jObject, JsonDiscriminatorPropertyName, out discriminatorValue))
                    return null;

                if (discriminatorValue.Type == JTokenType.Null)
                    return null;

                var typeMapping = GetSubTypeMapping(parentType);
                if (typeMapping.Any())
                {
                    return GetTypeFromMapping(typeMapping, discriminatorValue, true);
                }

                return GetTypeByName(discriminatorValue.Value<string>(), parentType);
            }

            private static bool TryGetValueInJson(IDictionary<string, JToken> jObject, string propertyName, out JToken value)
            {
                if (jObject.TryGetValue(propertyName, out value))
                {
                    return true;
                }

                var matchingProperty = jObject
                    .Keys
                    .FirstOrDefault(jsonProperty => string.Equals(jsonProperty, propertyName, StringComparison.OrdinalIgnoreCase));

                if (matchingProperty == null)
                {
                    return false;
                }

                value = jObject[matchingProperty];
                return true;
            }

            private static Type GetTypeByName(string typeName, Type parentType)
            {
                if (typeName == null)
                    return null;

                var insideAssembly = GetTypeInfo(parentType).Assembly;

                var typeByName = insideAssembly.GetType(typeName);
                if (typeByName != null)
                    return typeByName;

                var searchLocation = parentType.FullName.Substring(0, parentType.FullName.Length - parentType.Name.Length);
                return insideAssembly.GetType(searchLocation + typeName, false, true);
            }

            private static Type GetTypeFromMapping(Dictionary<object, Type> typeMapping, JToken discriminatorToken, bool contains)
            {
                var targetlookupValueType = typeMapping.First().Key.GetType();
                var lookupValue = discriminatorToken.ToObject(targetlookupValueType);

                Type targetType = null;
                if (!contains)
                {
                    if (typeMapping.TryGetValue(lookupValue, out targetType))
                        return targetType;
                    return null;
                }
                else
                {
                    foreach (KeyValuePair<object, System.Type> entry in typeMapping)
                    {
                        if (lookupValue.ToString().Contains(entry.Key.ToString()))
                        {
                            return entry.Value;
                        }
                    }
                    return null;
                }

            }

            protected virtual Dictionary<object, Type> GetSubTypeMapping(Type type)
            {
                return GetAttributes<KnownSubTypeAttribute>(type)
                    .ToDictionary(x => x.AssociatedValue, x => x.SubType);
            }

            private static object ThreadStaticReadObject(JsonReader reader, JsonSerializer serializer, JToken jToken, Type targetType)
            {
                _reader = CreateAnotherReader(jToken, reader);
                _isInsideRead = true;
                try
                {
                    return serializer.Deserialize(_reader, targetType);
                }
                finally
                {
                    _isInsideRead = false;
                }
            }

            private static IEnumerable<T> GetAttributes<T>(Type type) where T : Attribute
            {
                return GetTypeInfo(type)
                    .GetCustomAttributes(false)
                    .OfType<T>();
            }

            private static IEnumerable<Type> GetGenericTypeArguments(Type type)
            {

                var genericTypeArguments = type.GenericTypeArguments;
                return genericTypeArguments;
            }

            private static TypeInfo GetTypeInfo(Type type)
            {
                return type.GetTypeInfo();
            }
        }
    }
}
