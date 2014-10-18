// ***********************************************************************
// Assembly         : Beebotte.API.Server.Net
// Author           : sw09
// Created          : 07-01-2014
//
// Last Modified By : sw09
// Last Modified On : 07-01-2014
// ***********************************************************************
// <copyright file="RestClient.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

/// <summary>
/// The Net namespace.
/// </summary>
namespace Beebotte.API.Server.Net
{

    /// <summary>
    /// Class used to send requests to Beebotte Rest APIs
    /// </summary>
    public class RestClient
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Rest API end point.
        /// </summary>
        /// <value>The Rest API end point.</value>
        public string EndPoint { get; set; }
        /// <summary>
        /// Gets or sets the http method.
        /// </summary>
        /// <value>The method.</value>
        public string Method { get; set; }
        /// <summary>
            /// Gets or sets the content type header
            /// </summary>
        /// <value>The type of the content.</value>
        public string ContentType { get; set; }
        /// <summary>
        /// Gets or sets the post data.
        /// </summary>
        /// <value>The post data.</value>
        public string PostData { get; set; }
        /// <summary>
        /// Gets or sets the request date.
        /// </summary>
        /// <value>The request date.</value>
        public DateTime Date { get; set; }

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        public RestClient()
        {
            EndPoint = "";
            Method = HttpVerb.GET.ToString();
            ContentType = Constants.JsonContentType;
            PostData = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        /// <param name="endpoint">The Rest API endpoint.</param>
        public RestClient(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET.ToString();
            ContentType = Constants.JsonContentType;
            PostData = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="method">The method.</param>
        public RestClient(string endpoint, string method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = Constants.JsonContentType;
            PostData = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        /// <param name="endpoint">The Rest API endpoint.</param>
        /// <param name="method">The method.</param>
        /// <param name="postData">The post data.</param>
        /// <param name="contentType">The header content type</param>
        /// <param name="date">The request date.</param>
        public RestClient(string endpoint, string method, string postData, string contentType, DateTime date)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
            PostData = postData;
            Date = date;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Makes the request to the Rest API.
        /// </summary>
        /// <param name="parameters">The request parameters.</param>
        /// <param name="headers">The request headers.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ApplicationException"></exception>
        /// <exception cref="System.Net.WebException"></exception>
        public string MakeRequest(string parameters, Dictionary<string, string> headers)
        {
            var request = (HttpWebRequest)WebRequest.Create(String.Concat(EndPoint, parameters));

            request.Method = Method;
            request.ContentLength = 0;
#if NET_45_OR_GREATER
            request.Date = Date;
#else
            var priMethod = request.Headers.GetType().GetMethod("AddWithoutValidate", BindingFlags.Instance | BindingFlags.NonPublic);
            priMethod.Invoke(request.Headers, new[] { "Date", Date.ToString("R") });
#endif

            request.ContentType = ContentType;
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    if (request.Headers[header.Key] != null)
                    {
                        request.Headers[header.Key] = header.Value;
                    }
                    else
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }
            }

            if (!string.IsNullOrEmpty(PostData) && String.Equals(Method, HttpVerb.POST.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                var encoding = new UTF8Encoding();
                var bytes = encoding.GetBytes(PostData);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            var responseValue = string.Empty;

            try
            {
                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                var wex = (WebException) ex;
                var stream = wex.Response.GetResponseStream();
                if (stream != null)
                {
                    var message = String.Empty;
                    var lastNum = 0;
                    do
                    {
                        lastNum = stream.ReadByte();
                        message += (char) lastNum;
                    } while (lastNum != -1);
                    stream.Close();
                    stream = null;
                    throw new WebException(String.Format("Bad request. Error Message: {0}", message));
                }
            }
            return responseValue;
        }

        #endregion
    }
}
