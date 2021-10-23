using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestChunkUpload
{
    class Transcribe_Input
    {
        static void Main(string[] args) {
            string fileName = "assembly_key.json";
            string jsonString = System.IO.File.ReadAllText(fileName);
            System.Console.WriteLine(jsonString);
            // HttpClient is normally created once, then used for all message sending
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.assemblyai.com/v2/");
            client.DefaultRequestHeaders.Add("authorization", "YOUR-API-TOKEN");

            string jsonResult = SendFile(client, @"./output.wav").Result;
            System.Console.WriteLine(jsonResult);
        }


        private static async Task SendFile(HttpClient client, string filePath) {
            try {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "upload");
                request.Headers.Add("Transer-Encoding", "chunked");

                var fileReader = System.IO.File.OpenRead(filePath);
                var streamContent = new StreamContent(fileReader);
                request.Content = streamContent;

                HttpResponseMessage response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex) {
                System.Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

    }

}
