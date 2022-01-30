using System;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;


namespace IMneRestAPI
{
    public class GetAuthorizToken 
    {
        public string Token = "";

        public void GetToken()
        {
            // Set data for API request.
            var body = new Dictionary<string, string>
            {
                {"email", "innasukhina@gmail.com"},
                {"password", "123qweR!"}
            };
            var headers = new Dictionary<string, string>
            {
                {"Content-Type", "application/x-www-form-urlencoded"}
            };

            // Send API login.
            var response = API_HELPER.SendApiRequest(body, headers, "https://api.newbookmodels.com/api/v1/auth/signin/",
                Method.POST);
            Console.WriteLine(response);

            var result = JsonConvert.DeserializeObject<dynamic>(response);
            string token = result.token_data.token;

            Token = token;
        } 
    }
}