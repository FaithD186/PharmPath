using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

/*

This script controls how GameObjects react to various events in the First Scenario (i.e. beginning of the scenario, 
and the end of videos, knowledge checks), including: how specific text elements (fact panels, question panels) react to 
events in the gameplay.

*/

public class EventScript : MonoBehaviour
{
    
    // Panel GameObjects (i.e. different "screens")
    public GameObject PatientProfile;
    public GameObject MultipleQuestionPanel; // Question with multiple choices
    public GameObject TrueFalseQuestionPanel; // T/F Question
    public GameObject EndPanel;
    public GameObject KnowledgeCheck;
    public GameObject FactPanel;

    public GameObject MenuButton;
    public GameObject MenuPanel;

    // Video player control GameObjects
    public GameObject SkipButton;
    public GameObject PauseButton;
    public GameObject PlayButton;
    public VideoPlayer[] Videos;

    // Texts
    public TextMeshProUGUI QuestionText;
    public TextMeshProUGUI ExplanationText;
    public TextMeshProUGUI FactText;

    // Variable Trackers
    private int currentVideoIndex = 0;
    private int currentQuestionIndex = 0;
    

    void Start()
    {   PatientProfile.SetActive(true);
        FactPanel.SetActive(false);
        MultipleQuestionPanel.SetActive(false);
        TrueFalseQuestionPanel.SetActive(false);
        KnowledgeCheck.SetActive(false);
        EndPanel.SetActive(false);

        SkipButton.SetActive(false);
        PauseButton.SetActive(false);
        PlayButton.SetActive(false);      
    }

    public void StartVideo()
    {
        PatientProfile.SetActive(false);

        MenuButton.SetActive(true);
        SkipButton.SetActive(true);
        PauseButton.SetActive(true);

        Videos[currentVideoIndex].loopPointReached += LoadFactPanel;
    }

    public void LoadFactPanel(VideoPlayer vp){
        SkipButton.SetActive(false);
        PauseButton.SetActive(false);
        PlayButton.SetActive(false);
        if (currentVideoIndex >= Videos.Length || currentVideoIndex < 0){
            return;
        }
        else if (currentVideoIndex == Videos.Length - 1){ 
            // start knowledge check 
            KnowledgeCheck.SetActive(true);
        }
        else{
            FactPanel.SetActive(true);
            FactText.text = GetFactText(currentVideoIndex);
        }

    }

    public void ContinueClicked(){ // Continue in Fact Panel
            FactPanel.SetActive(false);
            SkipButton.SetActive(true);
            PauseButton.SetActive(true);
            currentVideoIndex++;
    }

    public void StartKnowledgeCheck(){
        Videos[currentVideoIndex].Stop();
        KnowledgeCheck.SetActive(false);
        MultipleQuestionPanel.SetActive(true);
        currentQuestionIndex++;
    }

    public void QuestionContinueClicked(){ // Continue in Knowledge Check (question panel)
        if (currentQuestionIndex == 4){ // last question panel
            TrueFalseQuestionPanel.SetActive(false);
            EndPanel.SetActive(true);
        }else{
            TrueFalseQuestionPanel.SetActive(true);
            QuestionText.text = GetQuestionText(currentQuestionIndex);
            ExplanationText.text = GetExplanationText(currentQuestionIndex);
            currentQuestionIndex++;
        }
        
    }
    
    // Text events 

    public string GetFactText(int index){
        string[] FactTexts = {"It is within the scope of practice of a Registered Pharmacy Technician (RPhT) to collect information, and it is important to remember that the patient has placed their trust in us to provide them with care.\n\nAn appropriate next action that the RPhT should take is to collect information from Wyatt, and then ask the pharmacist to make a clinical recommendation.",
        "While the Registered Pharmacy Technician (RPhT) can perform triages (assign degree of urgency to determine treatment) and highlight available over-the-counter (OTC) product options, they cannot recommend a product clinically for a specific patient as that is outside their scope of practice.",  
        "It is within a Registered Pharmacy Technician’s (RPhT) scope of practice to perform technical checks on prescriptions. \n\nThis includes performing technical checks of new prescriptions and refill prescriptions.", 
        "Both Registered Pharmacists (RPh) and Registered Pharmacy Technicians (RPhT) are bound to uphold the Ontario College of Pharmacists Code of Ethics. \n\nThe Ontario College of Pharmacists Code of Ethics is a set of guidelines that aims to ensure the delivery of safe, effective, and ethical pharmaceutical care."};
        if (index >= 0 && index < FactTexts.Length)
            return FactTexts[index];
        else
            return "";
    }
    public string GetQuestionText(int index)
    {
        string[] questionTexts = { "None", 
        "It is within the Registered Pharmacy Technician’s (RPhT) scope of practice to perform a triage (assign degree of urgency to determine treatment) in order to gather appropriate products and to provide a clinical recommendation to the patient.", 
        "A Registered Pharmacy Technician (RPhT) is allowed to perform the technical check on prescriptions.", 
        "Registered Pharmacists (RPh) and Registered Pharmacy Technicians (RPhT) are bound to uphold the Ontario College of Pharmacists Code of Ethics."};
        if (index >= 0 && index < questionTexts.Length)
            return questionTexts[index];
        else
            return "";
    }

    public string GetExplanationText(int index)
    {
        string[] explanationTexts = { "None", 
        "While the RPhT can perform triages and highlight available over-the-counter (OTC) product options, they cannot recommend a product clinically for a specific patient as that is outside their scope of practice.\n\nOnce the pharmacist has completed their clinical assessment of the patient, they may make a clinical recommendation (e.g., for an OTC product) or refer the patient for further evaluation by another health care professional (e.g., by a physician, nurse practitioner).", 
        "It is within a Registered Pharmacy Technician’s (RPhT) scope of practice to perform the technical checks on prescriptions. \n\nThis includes performing technical checks of new prescriptions and refill prescriptions.", 
        "Both Registered Pharmacists (RPh) and Registered Pharmacy Technicians (RPhT) are bound to uphold the Ontario College of Pharmacists Code of Ethics. \n\nThe Ontario College of Pharmacists Code of Ethics is a set of guidelines that aims to ensure the delivery of safe, effective, and ethical pharmaceutical care." };
        if (index >= 0 && index < explanationTexts.Length)
            return explanationTexts[index];
        else
            return "";
    }

    public void SkipVideo(){
        Videos[currentVideoIndex].Stop();
        LoadFactPanel(null);
    }
}
