using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other){
        Debug.Log("We're here??");
        SceneManager.LoadScene(2);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // void OnCollisionEnter(Collision collision){
    //     Debug.Log("Weee?????");
    //     SceneManager.LoadScene(2);
    //     // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);;
    // }

}
