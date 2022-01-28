using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections;
using System.Collections.Generic;
using RestSharp;

namespace IMneRestAPI
{
    public static class API_HELPER
    {
        // The method that sends the request.
        public static IRestResponse SendApiRequest(object body, Dictionary<string, string> headers, string link, Method type)
        {
            RestClient client = new RestClient(link)
            {
                Timeout = 300000
            };
            RestRequest request = new RestRequest(type);
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            bool isBodyJson = false;
            foreach (var header in headers)
            {
                if (header.Value.Contains("application/json"))
                {
                    isBodyJson = true;
                    break;
                }
            }
            if (!isBodyJson)
            {
                foreach (var data in (Dictionary<string, string>)body)
                {
                    request.AddParameter(data.Key, data.Value);
                }
            }
            else
            {
                request.AddJsonBody(body);
                request.RequestFormat = DataFormat.Json;
            }
            IRestResponse response = client.Execute(request);
            return response;
        }
        // Method that extract cookies in res.
        public static Cookie ExtractCookie(IRestResponse response, string cookieName)
        {
            Cookie res = null;
            foreach (var cookie in response.Cookies)
                if (cookie.Name.Equals(cookieName))
                    res = new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null);
            return res;
        }
        // Method that extract all cookies in res.
        public static List<Cookie> ExtractAllCookies(IRestResponse response)
        {
            List<Cookie> res = new List<Cookie>();
            foreach (var cookie in response.Cookies)
                res.Add(new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null));
            return res;
        }
        // Method that upload image to website.
        public static IRestResponse UploadImageApiRequest(object body, Dictionary<string, string> headers, string link, Method type)
        {
            RestClient client = new RestClient(link)
            {
                Timeout = 300000
            };
            RestRequest request = new RestRequest(type);
            foreach (var header in headers)
                request.AddHeader(header.Key, header.Value);

            request.AddJsonBody(body);
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);
            return response;
        }

    }
}
