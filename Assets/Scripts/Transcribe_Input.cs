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
    public static class Transcribe_Input
    {
        private static async Task<string> SendFile(HttpClient client, string filePath) {
            try {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "upload");
                request.Headers.Add("Transer-Encoding", "chunked");

                var fileReader = System.IO.File.OpenRead(filePath);
                var streamContent = new StreamContent(fileReader);
                request.Content = streamContent;

                HttpResponseMessage response = await client.SendAsync(request);
                string outs = await response.Content.ReadAsStringAsync();
                return outs;
            }
            catch (Exception ex) {
                System.Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }


        public static void Run() {
            string fileName = "./Assets/Scripts/assembly_key.json";
            Json_Parse jfile = new Json_Parse();
            string jsonString = System.IO.File.ReadAllText(fileName);
            jfile = JsonUtility.FromJson<Json_Parse>(jsonString);
            // jsonString = jsonString.Remove(0, 3);
            // JObject obj = JObject.Parse(jsonString);
            // string name = (string) obj["key"];
            Debug.Log(jfile.key);
            // HttpClient is normally created once, then used for all message sending
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.assemblyai.com/v2/");
            client.DefaultRequestHeaders.Add("authorization", jfile.key);

            // string jsonResult = SendFile(client, @"./Assets/Scripts/output.wav").Result;
            // Debug.Log(jsonResult);
        }

        // static void Main(string[] args) {
        //     string fileName = "assembly_key.json";
        //     string jsonString = System.IO.File.ReadAllText(fileName);
        //     System.Console.WriteLine(jsonString);
        //     // HttpClient is normally created once, then used for all message sending
        //     HttpClient client = new HttpClient();
        //     client.BaseAddress = new Uri("https://api.assemblyai.com/v2/");
        //     client.DefaultRequestHeaders.Add("authorization", "YOUR-API-TOKEN");

        //     string jsonResult = SendFile(client, @"./output.wav").Result;
        //     System.Console.WriteLine(jsonResult);
        // }


        

    }

}
