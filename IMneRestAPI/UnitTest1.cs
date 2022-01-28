using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections;
using System.Collections.Generic;
using RestSharp;
using Xunit;
using System.Text.Json;
using System.Net;
using RestSharp.Extensions;
using System.IO;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace IMneRestAPI
{
    public class UnitTest1
    {
        [Fact]
        public void ExtractAndAddCookiesTest()
        {
            // Set data for API request.
            var body = new Dictionary<string, string>
            {
                {"ulogin", "art1613122" },
                {"upassword", "505558545"}
            };
            var headers = new Dictionary<string, string>
            {
                {"Content-Type", "application/x-www-form-urlencoded"}
            };

            // Send API login.
            var response = API_HELPER.SendApiRequest(body, headers, "https://my.soyuz.in.ua", Method.POST);

            // Get Cookie from API response.
            var cookie = API_HELPER.ExtractCookie(response, "zbs_lang");
            var cookie2 = API_HELPER.ExtractCookie(response, "ulogin");
            var cookie3 = API_HELPER.ExtractCookie(response, "upassword");

            // Open browser.
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://my.soyuz.in.ua");
            System.Threading.Thread.Sleep(3000);

            // Set Cookie to browser.
            driver.Manage().Cookies.AddCookie(cookie);
            driver.Manage().Cookies.AddCookie(cookie2);
            driver.Manage().Cookies.AddCookie(cookie3);

            // Open Site (profile page) & close browser.
            driver.Navigate().GoToUrl("https://my.soyuz.in.ua/index.php");
            System.Threading.Thread.Sleep(15000);
            driver.Close();
        }
        [Fact]
        public void DownLoadImageTest()
        {
            RestClient client = new RestClient("https://imageup.ru/img1/3874179/dsc_2700.jpg.html");
            var imageRequest = new RestRequest(Method.GET);
            byte[] result = client.DownloadData(imageRequest);
            File.WriteAllBytes(Path.Combine("/Users/innasukhina/Projects/IMneRestAPI/IMneRestAPI/img.JPG"), result);

        }
        [Fact]
        public void UploadImageTest()
        {
            var body = new Dictionary<string, object>
            {
                {"login", "NimeriaTheDirewolf" },
                {"password", "291188a" },
                 {"img", "/Users/innasukhina/Desktop/DSC_3009.JPG" }
            };
            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" }
            };

            var response = API_HELPER.UploadImageApiRequest(body, headers, "https://imageup.ru/", Method.POST);
        }
        [Fact]
        public void UploadImageTest2()
        {
            var client = new RestClient("https://imgbb.com/");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddFile("content", "/Users/innasukhina/Desktop/DSC_3009.JPG");
            IRestResponse response = client.Execute(request);
        }
        [Fact]
        public void ResponseStatusTest()
        {
            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json"},
                { "authority","api.newbookmodels.com"}
            };
            var body = new Dictionary<string, string>
            {
                { "password", "123qweR!"},
                { "email", "innasukhina@gmail.com"}
            };

            var response = API_HELPER.SendApiRequest(body, headers, "https://api.newbookmodels.com/api/v1/auth/signin/", Method.POST);
            ResponseStatus contents = JsonSerializer.Deserialize<ResponseStatus>(response.Content);
        }
    }

}
