using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Newtonsoft.Json;

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
                return upload.upload_url;
                
            }
            catch (Exception ex) {
                Debug.Log($"Exception: {ex.Message}");
                throw;
            }
        }


        public static async Task<string> Run() {
            string fileName = "./Assets/Scripts/assembly_key.json";
            Json_Parse jfile = new Json_Parse();
            AI_Parse parsed = new AI_Parse();
            string jsonString = System.IO.File.ReadAllText(fileName);
            jfile = JsonUtility.FromJson<Json_Parse>(jsonString);
            Debug.Log(jfile.key);
            // HttpClient is normally created once, then used for all message sending
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.assemblyai.com/v2/");
            client.DefaultRequestHeaders.Add("authorization", jfile.key);
            string jsonResult = await SendFile(client, @"./Assets/Scripts/output.wav");
            // string jsonResult =  SendFile(client, @"./Assets/Scripts/output.wav").Result;
            Debug.Log("url received!!!!");
            Debug.Log(jsonResult);

            var json = new {audio_url = jsonResult, punctuate = false};
            StringContent payload = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "./json");

            // submitting upload for transcription
            HttpResponseMessage response = await client.PostAsync("https://api.assemblyai.com/v2/transcript", payload);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            parsed = JsonUtility.FromJson<AI_Parse>(responseJson);
            Debug.Log(parsed.status); //debug

            // submitting gets till completed
            string endpoint = "https://api.assemblyai.com/v2/transcript/" + parsed.id;

            while((parsed.status != "completed" && (parsed.status != "error"))) {
                response = await client.GetAsync(endpoint); //GET REQUEST
                response.EnsureSuccessStatusCode();
                responseJson = await response.Content.ReadAsStringAsync();
                parsed = JsonUtility.FromJson<AI_Parse>(responseJson);
                Debug.Log("Waiting");
            }
            // Debug.Log(parsed.status);
            // Debug.Log(responseJson);
            // Debug.Log(parsed.text);
            return parsed.text;
        }

    }

}
