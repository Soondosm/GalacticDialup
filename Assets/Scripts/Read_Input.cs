using UnityEngine;
using System.Collections;
using FrostweepGames.Plugins.Native;

public class Read_Input : MonoBehaviour {
 
     AudioClip myAudioClip; 
 
     void Start() {}
     void Update () {}
     
    async void OnGUI()
    {
         if (GUI.Button(new Rect(10,10,60,50),"Record"))
     { 
         myAudioClip = CustomMicrophone.Start ( null, false, 10, 44100 );
        //  myAudioClip = AudioClip.Create("output", 44100, 1, 44100, false);
     }
     if (GUI.Button(new Rect(10,70,60,50),"Send"))
     {
         SavWav.Save("output", myAudioClip);
         string input = await TestChunkUpload.Transcribe_Input.Run();
        Debug.Log(input);
        PlayerMovement.ChangeInput(input);

 //        audio.Play();
         }
    }
 }