using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/*

This script determines video player controls for the current video being played, in 
particular pausing/playing video. 


*/

public class VideoController : MonoBehaviour
{

    public VideoPlayer[] Videos;
    private int currentVideoIndex = 0;
    private long lastFrame;
   
    public GameObject SkipButton;
    public GameObject PauseButton;
    public GameObject PlayButton;

    public GameObject FactPanel;
    public GameObject MenuPanel;
    public GameObject MultipleQuestionPanel; // Question with multiple choices
    public GameObject TrueFalseQuestionPanel;
    public GameObject PatientProfile;
    public GameObject KnowledgeCheck;

    public void MenuClicked(){
        MenuPanel.SetActive(true);
        if (currentVideoIndex <= Videos.Length - 1){
            Time.timeScale = 0;
            Videos[currentVideoIndex].Pause();
            SkipButton.SetActive(false);
            PauseButton.SetActive(false);
            PlayButton.SetActive(false);
        }
    }

    public void CloseMenu(){
        if (FactPanel.activeSelf || MultipleQuestionPanel.activeSelf || TrueFalseQuestionPanel.activeSelf || PatientProfile.activeSelf || KnowledgeCheck.activeSelf){ 
            MenuPanel.SetActive(false);
        }
        else{
            MenuPanel.SetActive(false);
            Time.timeScale = 1;
            Videos[currentVideoIndex].Play();
            SkipButton.SetActive(true);
            PauseButton.SetActive(true);
            PlayButton.SetActive(false);
        }
    }
    public void PauseClicked(){
        Time.timeScale = 0;
        Videos[currentVideoIndex].Pause();
        SkipButton.SetActive(false);
        PlayButton.SetActive(true);
    }
    public void UnPauseClicked(){
        Time.timeScale = 1;
        Videos[currentVideoIndex].Play();
        SkipButton.SetActive(true);
        PlayButton.SetActive(false);
    }
    public void ContinueClicked(){
        currentVideoIndex++;
    }
    public void ReplayScene(){
        SkipButton.SetActive(true);
        PauseButton.SetActive(true);
        PlayButton.SetActive(false);
        Videos[currentVideoIndex].time = 0;
        Videos[currentVideoIndex].Play();
        FactPanel.SetActive(false);
        KnowledgeCheck.SetActive(false);
    }  
}
