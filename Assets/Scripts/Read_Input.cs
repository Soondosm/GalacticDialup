using UnityEngine;
using System.Collections;

public class Read_Input : MonoBehaviour {
 
     AudioClip myAudioClip; 
 
     void Start() {}
     void Update () {}
     
    void OnGUI()
    {
         if (GUI.Button(new Rect(10,10,60,50),"Record"))
     { 
         myAudioClip = Microphone.Start ( null, false, 10, 44100 );
     }
     if (GUI.Button(new Rect(10,70,60,50),"Save"))
     {
         SavWav.Save("output", myAudioClip);
         TestChunkUpload.Transcribe_Input.Run();
 
 //        audio.Play();
         }
    }
 }