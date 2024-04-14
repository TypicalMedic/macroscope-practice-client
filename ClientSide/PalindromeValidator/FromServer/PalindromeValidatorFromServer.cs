using ClientSide.PalindromeValidator.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

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
            try
            {
                var responce = client.SendAsync(request).Result;
                if (!responce.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Не удалось выполнить запрос на получение соответствия палиндрому.");
                }
                resultText = responce.Content.ReadAsStringAsync().Result;
                bool isPalindrome = bool.Parse(resultText);
                return isPalindrome;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> IsValidAsync(string value)
        {
            string resultText;
            var request = new HttpRequestMessage(HttpMethod.Post, endpointUrl)
            {
                Content = JsonContent.Create(value)
            };
            try
            {
                var responce = await client.SendAsync(request).ConfigureAwait(false);
                if (!responce.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Не удалось выполнить запрос на получение соответствия палиндрому.");
                }
                resultText = await responce.Content.ReadAsStringAsync().ConfigureAwait(false);
                bool isPalindrome = bool.Parse(resultText);
                return isPalindrome;
            }
            catch
            {
                throw;
            }
        }
    }
}
