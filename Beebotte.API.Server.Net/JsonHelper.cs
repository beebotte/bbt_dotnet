using System;
// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : SAW
// Created          : 07-01-2014
//
// Last Modified By : SAW
// Last Modified On : 07-15-2014
// ***********************************************************************
// <copyright file="JsonHelper.cs" company="Beebotte">
//     Copyright (c) Beebotte. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

/// <summary>
/// The Net namespace.
/// </summary>
namespace Beebotte.API.Server.Net
{

    /// <summary>
    /// JSON Serialization and Deserialization Assistant Class
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// JSON Serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// <returns>System.String.</returns>
        public static string JsonSerialize<T>(T t)
        {
     
            var settings = new DataContractJsonSerializerSettings
                {
                    EmitTypeInformation = System.Runtime.Serialization.EmitTypeInformation.Never
                };
            var ser = new DataContractJsonSerializer(typeof(T),  settings);
            string jsonString;
            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, t);
                jsonString = Encoding.UTF8.GetString(ms.ToArray());
            }
            return jsonString;
        }
        /// <summary>
        /// JSON Deserialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">The json string.</param>
        /// <returns>T.</returns>
        public static T JsonDeserialize<T>(string jsonString)
        {
            var ser = new DataContractJsonSerializer(typeof(T));
            T obj = Activator.CreateInstance<T>(); 
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                obj = (T) ser.ReadObject(ms);
            }
            return obj;
        }
    }
}
