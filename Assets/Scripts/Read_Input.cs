using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
// using FrostweepGames.Plugins.Native;

public class Read_Input : MonoBehaviour {
 
     AudioClip myAudioClip; 
 
     void Start() { 
         GameObject.Find ("Circle").transform.localScale = new Vector3(0, 0, 0); // hide 
     }
     void Update () {}
     
    async void OnGUI()
    {
         if (GUI.Button(new Rect(10,10,120,100),"Record"))
     { 
         myAudioClip = Microphone.Start ( null, false, 10, 44100 );
        //  myAudioClip = AudioClip.Create("output", 44100, 1, 44100, false);
     }
     if (GUI.Button(new Rect(10,130,120,100),"Send"))
     {
        SavWav.Save("output", myAudioClip);
        GameObject.Find ("Circle").transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // show 
        string input = await TestChunkUpload.Transcribe_Input.Run();
        Debug.Log(input);
        if(SceneManager.GetActiveScene().buildIndex == 1) {
            PlayerMovement.ChangeInput(input);
            Debug.Log("in lvl1");
        } else {
            Debug.Log("in level2");
            PlayerMovement2.ChangeInput(input);
        }
        
        // 
        GameObject.Find ("Circle").transform.localScale = new Vector3(0, 0, 0); // hide 

 //        audio.Play();
         }
    }
 }