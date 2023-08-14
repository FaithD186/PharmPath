using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{

    public GameObject IntroScreen;

    void Start()
    {
        IntroScreen.SetActive(false);
    }
    public void StartGame(){
        IntroScreen.SetActive(true);
    }
    public void Continue(){
        SceneManager.LoadScene("Home");
    }

}
