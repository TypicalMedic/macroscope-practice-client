﻿using ClientSide.PalindromeValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.PalindromeValidator.FromServer
{
    class PalindromeValidatorFromServer(HttpClient client) : IPalindromeValidator
    {
        private const string endpointUrl = "/palindrome/check";
        private readonly HttpClient client = client;
        public bool IsValid(string value)
        {
            string resultText;
            var request = new HttpRequestMessage(HttpMethod.Post, endpointUrl)
            {
                Content = JsonContent.Create(value)
            };
            var responce = client.SendAsync(request).Result;
            resultText = responce.Content.ReadAsStringAsync().Result;
            bool isPalindrome = bool.Parse(resultText);
            return isPalindrome;
        }
        public async Task<bool> IsValidAsync(string value)
        {
            string resultText;
            var request = new HttpRequestMessage(HttpMethod.Post, endpointUrl)
            {
                Content = JsonContent.Create(value)
            };
            var responce = await client.SendAsync(request).ConfigureAwait(false);
            resultText = await responce.Content.ReadAsStringAsync().ConfigureAwait(false);
            bool isPalindrome = bool.Parse(resultText);
            return isPalindrome;
        }

        //public async Task<bool> IsValidAsync(string value)
        //{
        //    bool operationSuccessful;
        //    string resultText;
        //    do
        //    {
        //        var request = new HttpRequestMessage(HttpMethod.Post, serverUrl + endpointUrl)
        //        {
        //            Content = JsonContent.Create(value)
        //        };
        //        var responce = await client.SendAsync(request).ConfigureAwait(false);
        //        resultText = await responce.Content.ReadAsStringAsync().ConfigureAwait(false);
        //        operationSuccessful = responce.IsSuccessStatusCode;
        //        if (!operationSuccessful)
        //        {
        //            await Task.Delay(1000);
        //        }
        //    }
        //    while (!operationSuccessful);
        //    bool isPalindrome = bool.Parse(resultText);
        //    return isPalindrome;
        //}
    }
}
