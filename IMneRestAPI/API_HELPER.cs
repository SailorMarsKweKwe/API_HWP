using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Extensions;

namespace IMneRestAPI
{
    public static class API_HELPER
    {
        // Метод, который отправляет запрос.
        public static string SendApiRequest(object body, Dictionary<string, string> headers, string link, Method type)
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
            return HandleApiError(response);
        }
        
        // Метод, который загружает файл.
        public static string UploadFile(string link, string filePath, Dictionary<string, string> headers, string fileFormat, string fileParam)
        {
            RestClient client = new RestClient(link)
            {
                Timeout = 300000
            };
            RestRequest request = new RestRequest(Method.POST);
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            request.AddFile(fileParam, @filePath, fileFormat);
            
            IRestResponse response = client.Execute(request);
            return HandleApiError(response);
        }
        
        // Метод который скачивает файл.
        public static string DownloadFile(string link, string nameFile)
        {
            RestClient restClient = new RestClient(link);
            RestRequest request = new RestRequest(Method.GET);
            IRestResponse response = restClient.Execute(request);
            byte[] bytes = response.RawBytes; 
            // Сохранение в файл.
            bytes.SaveAs($"../../../images/{nameFile}"); 

            return HandleApiError(response);
        }
        
        // Обработчик ответов (ошибок).
        public static string HandleApiError(IRestResponse response)
        {
            if (response.IsSuccessful)
            {
                return response.Content;
            }
            else
            {
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.ErrorMessage);
                Console.WriteLine(response.Content);
                throw new NotImplementedException(response.Content);
            }
        }
        
        /*
        // Метод, который вытягивает куки в res.
        public static Cookie ExtractCookie(IRestResponse response, string cookieName)
        {
            Cookie res = null;
            foreach (var cookie in response.Cookies)
                if (cookie.Name.Equals(cookieName))
                    res = new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null);
            return res;
        }
        
        // Метод, который вытягивает все куки в res.
        public static List<Cookie> ExtractAllCookies(IRestResponse response)
        {
            List<Cookie> res = new List<Cookie>();
            foreach (var cookie in response.Cookies)
                res.Add(new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null));
            return res;
        }
        */
    }
}
