using System;
using System.Collections.Generic;
using RestSharp;
using Xunit;

namespace IMneRestAPI
{
    public class UnitTest1
    {
        [Fact]
        public void AuthorizationWithTokenTest()
        {
            GetAuthorizToken authorizToken = new GetAuthorizToken();
            authorizToken.GetToken();
        }

        // Тест на добавление нового списка в избранное.
        [Fact]
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
                {"description", "beauty"},
                {"name", "LovelyPretty"}
            };
            // Отправка запроса на добавление нового списка в избранное.
            var responseAddNewList = API_HELPER.SendApiRequest(bodyFav, headersFavorites,
                "https://api.newbookmodels.com/api/v1/folders/", Method.POST);
            Console.WriteLine(responseAddNewList);

        }

        // Проверка загрузки изображения с помощью токена.
        [Fact]
        public void UploadImageWithTokenTest()
        {
            GetAuthorizToken authorizToken = new GetAuthorizToken();
            authorizToken.GetToken();
            var headers = new Dictionary<string, string>
            {
                {"content-type", "multipart/form-data"},
                {"authorization", authorizToken.Token}
            };
            var responseUpload = API_HELPER.UploadFile("https://api.newbookmodels.com/api/images/upload/",
                "../../../images/Einstein.jpeg", headers, "image/jpeg", "file");
            Console.WriteLine(responseUpload);
        }

        // Проверка скачивания изображения с помощью токена.
        [Fact]
        public void DownLoadImageWithTokenTest()
        {
            GetAuthorizToken authorizToken = new GetAuthorizToken();
            authorizToken.GetToken();

            var headers = new Dictionary<string, string>
            {
                {"Content-Type", "multipart/form-data"},
                {"authorization", authorizToken.Token}
            };

            var responseDownload =
                API_HELPER.DownloadFile(
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQRVJulmlLEMi2ob7AwqLtPikIXbqLoFXR_ck7_wxIfMZuDS6UZrDo0xAV8KSLDjjQ0x64&usqp=CAU",
                    "Test.jpeg");
            Console.WriteLine(responseDownload);
        }
    }
}
