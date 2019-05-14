using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BloodTrace.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BloodTrace.Services
{
    public class ApiServices
    {
        public async Task<bool> Register(string email, string password, string confirmpassword)
        {
            var registerModel = new RegisterModel()
            {
                Email = email,
                Password = password,
                ConfrimPassword = confirmpassword
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://localhost:59304/api/Account/Register", content);

            return response.IsSuccessStatusCode;
        }
        //Login User
        public async Task<bool> LoginUser(string email, string password)
        {
            var keyvalues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("email",email),
                new KeyValuePair<string, string>("password",password),
                new KeyValuePair<string, string>("grant_type","password")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:59304/Token");
            request.Content = new FormUrlEncodedContent(keyvalues);
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(request);
            var content = response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }

        public async Task<List<BloodUser>> FindBlod(string _bloodGroup, string _country)
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "We will pass tocked later");
            var json = await httpclient.GetStringAsync("http://localhost:59304/api/BloodUsers?_bloodgroup="+ _bloodGroup + "&_country=" + _country + "");
            return JsonConvert.DeserializeObject<List<BloodUser>>(json);
        }

        public async Task<bool> RegisterDonar(BloodUser _blooduser)
        {
            var json = JsonConvert.SerializeObject(_blooduser);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "We will pass tocked later");
            var response= await httpClient.PostAsync("http://localhost:59304/api/BloodUsers", content);
            return response.IsSuccessStatusCode; 
        }
    }
}
