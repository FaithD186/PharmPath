using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScript : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject AboutPopup;

    public void FirstCase(){
        SceneManager.LoadScene("Case1_Start");
    }
    public void SecondCase(){
        SceneManager.LoadScene("Case2_Start");
    }
    public void ThirdCase(){
        SceneManager.LoadScene("Case3_Start");
    }

}
