using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PollyRetry.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PollyRetry.Services
{
    public interface IGithubService
    {
        Task<GithubUser> GetUserByUserName(string userName);
    }
    public class GithubService : IGithubService
    {
        private const int MaxRetries = 3;
        private static readonly Random Random = new Random();
        private readonly HttpClient _httpClient;
        
        public GithubService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            this._httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            this._httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");//Set the User Agent to "r
        }
        public async Task<GithubUser> GetUserByUserName(string userName)
        {            
            var retriesLeft = MaxRetries;
            GithubUser user = null;
            while (retriesLeft > 0)
            {
                try
                {
                    if (Random.Next(1, 3) == 1)
                        throw new HttpRequestException("this is a fake request exception");

                    var result = await _httpClient.GetAsync($"users/{userName}");
                    if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        break;
                    }

                    var resultString = await result.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<GithubUser>(resultString);
                    break;
                }
                catch (HttpRequestException exception)
                {
                    retriesLeft--;
                    if (retriesLeft == 0)
                    {
                        throw;
                    }
                }
            }

            return user;    
        }
    }
   
}
