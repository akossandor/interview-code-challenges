using FluentAssertions;
using Newtonsoft.Json;
using OneBeyondApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace OneBeyondApi.Tests.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AddApiVersionHeader<T>(this T client, string apiVersion) where T : HttpClient
        {
            client.DefaultRequestHeaders.Add(Header.Version, apiVersion);
        }

        public static async Task<HttpResponseMessage> PostAsync(this HttpClient client, string url, object data)
        {
            using var stringContent = GetStringContent(data);
            return await client.PostAsync(url, stringContent);
        }

        public static async Task<T> PostAsync<T>(this HttpClient client, string url, object data) where T : class
        {
            using var stringContent = GetStringContent(data);
            using var response = await client.PostAsync(url, stringContent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // Helpers

        private static StringContent GetStringContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, MediaTypeNames.Application.Json);
        }
    }
}
