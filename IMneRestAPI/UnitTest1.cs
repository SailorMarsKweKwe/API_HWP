using System;
using System.Collections.Generic;
using RestSharp;
using Xunit;
using System.Net;
using System.IO;

namespace IMneRestAPI
{
    public class UnitTest1
    {
        [Fact]
        
        // Тест на добавление нового списка в избранное.
        public void AddNewListToFavoritsTest()
        {
            GetAuthorizToken authorizToken = new GetAuthorizToken();
            authorizToken.GetToken();
              
              var headersFavorites = new Dictionary<string, string>
            {
                {"authorization", authorizToken.Token}
            };
            var bodyFav = new Dictionary<string, string>
            {
                {"description", "beauty" },
                {"name", "LovelyBoys"}
            };
            // Отправка запроса на добавление нового списка в избранное.
            var responseAddNewList = API_HELPER.SendApiRequest(bodyFav, headersFavorites, "https://api.newbookmodels.com/api/v1/folders/", Method.POST);
            Console.WriteLine(responseAddNewList);
            
        }
        [Fact]
        public void DownLoadImageTest()
        {
            RestClient client = new RestClient("https://flowers.ua/images/Flowers/71.jpg");
            var imageRequest = new RestRequest(Method.GET);
            byte[] result = client.DownloadData(imageRequest);
            File.WriteAllBytes(Path.Combine("/Users/innasukhina/Projects/API_HWP/API_HWP/IMneRestAPI/img.JPG"), result);

        }
        // Проверка загрузки изображения с помощью токена.
        [Fact]
        public void UploadImageWithTokenTest()
        {
            GetAuthorizToken authorizToken = new GetAuthorizToken();
            authorizToken.GetToken(); 
            
            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "multipart/form-data" },
                {"authorization", authorizToken.Token}
            };
            var responseUpload = API_HELPER.UploadFile("https://api.newbookmodels.com/api/images/upload/",
                "../../../images/Einstein.jpeg", headers, "image/jpeg", "file");
            Console.WriteLine(responseUpload);
        }

        [Fact]
        public void DownloadImageTest2()
        {
            WebClient client = new WebClient();
            client.DownloadFile("https://flowers.ua/images/Flowers/thumbnail/1079.jpg", @"/Users/innasukhina/Projects/API_HWP/API_HWP/IMneRestAPI/img1.JPG");
        }

         // Проверка скачивания изображения с помощью токена.
        [Fact]
        public void DouwnLoadImageWithTokenTest()
        {
            GetAuthorizToken authorizToken = new GetAuthorizToken();
            authorizToken.GetToken();
            
            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "multipart/form-data" },
                {"authorization", authorizToken.Token}
            };

            var responseDownload =
                API_HELPER.DownloadFile(
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQRVJulmlLEMi2ob7AwqLtPikIXbqLoFXR_ck7_wxIfMZuDS6UZrDo0xAV8KSLDjjQ0x64&usqp=CAU",
                    "TroTto.jpeg");
            Console.WriteLine(responseDownload);


        }
    }

}
