using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;

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
                Url_Parse upload = new Url_Parse();
                upload = JsonUtility.FromJson<Url_Parse>(outs);

                Debug.Log(upload.upload_url);
                return upload.upload_url;
                
            }
            catch (Exception ex) {
                Debug.Log($"Exception: {ex.Message}");
                throw;
            }
        }


        public static async void Run() {
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
            string jsonResult = await SendFile(client, @"./Assets/Scripts/output.wav");
            // string jsonResult =  SendFile(client, @"./Assets/Scripts/output.wav").Result;
            Debug.Log("url received!!!!");
        }

    }

}
