using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMain : MonoBehaviour
{
    public void BackToMain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -3); // back to title screen

    }
}
