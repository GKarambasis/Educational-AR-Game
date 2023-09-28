using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Numbers
    [Header("Numbers")]
    [SerializeField] int newNumber;
    [SerializeField] List<int> numbersList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    //AR Meshes
    [Header("AR Meshes")]
    [SerializeField] GameObject[] numberMeshes;
    private GameObject currentmesh;

    //In-Game HUD
    [Header("HUD")]
    public TextMeshProUGUI[] buttons;
    [SerializeField] ParticleSystem[] confettiParticles;
    [SerializeField] GameObject goodJobText, tryAgainText;
    private bool buttonDisabled = false;

    //Menu Panels
    [Header("Menu Panels")]
    [SerializeField] GameObject mainmenuPanel;
    [SerializeField] GameObject hudPanel;
    [SerializeField] GameObject endPanel;
    [SerializeField] GameObject languagePanel;
    [SerializeField] GameObject menuPanel;

    //Audio
    [Header("Audio")]
    [SerializeField] AudioClip bgMusic;
    [SerializeField] AudioClip[] numberVoiceOvers;
    [SerializeField] AudioClip[] feedbackVoiceOvers;
    private AudioSource audioSouce;

    //Score
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    private int score = 0;
    private bool wrongAnswer = false;

    //Script
    private LanguageConverter converter;

    void Start()
    {
        audioSouce = gameObject.GetComponent<AudioSource>();
        converter = gameObject.GetComponent<LanguageConverter>();
    }

    //Generates a new Number on the screen
    public void PrintNewNumber()
    {
        if (numbersList.Count != 0)
        {
            int randomIndex = Random.Range(0, numbersList.Count);
            newNumber = numbersList[randomIndex];

            //Show the correct AR Mesh for the number
            currentmesh = numberMeshes[newNumber - 1];
            currentmesh.SetActive(true);

            //Play the Right Audio Clip
            audioSouce.clip = numberVoiceOvers[newNumber - 1];
            if (audioSouce.loop)
            {
                audioSouce.loop = false;
            }
            audioSouce.Play();

            ButtonSetUp();
        }
        else
        {
            hudPanel.SetActive(false);
            endPanel.SetActive(true);
            UpdateScoreText();
            numbersList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }

    }


    //Randomly sets up the button numbers
    private void ButtonSetUp()
    {
        int randomButton = Random.Range(0, 3);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == 0)
            {
                do
                {
                    buttons[i].text = Random.Range(0, 10).ToString();
                }
                while (buttons[i].text == newNumber.ToString());
            }

            else if (i == 1)
            {
                do
                {
                    buttons[i].text = Random.Range(0, 10).ToString();
                }
                while (buttons[i].text == newNumber.ToString() || buttons[i].text == buttons[i - 1].text);
            }

            else if (i == 2)
            {
                do
                {
                    buttons[i].text = Random.Range(0, 10).ToString();
                }
                while (buttons[i].text == newNumber.ToString() || buttons[i].text == buttons[i - 1].text || buttons[i].text == buttons[i - 2].text);
            }
        }

        buttons[randomButton].text = newNumber.ToString();
    }


    //Button Trigger Functions
    public void Button1()
    {
        if (!buttonDisabled)
        {
            if (buttons[0].text == newNumber.ToString())
            {
            CorrectAnswer();
            confettiParticles[0].Play();
            }
            else
            {
            WrongAnswer();
            }

        }
    }

    public void Button2()
    {
        if (!buttonDisabled)
        {
            if (buttons[1].text == newNumber.ToString())
        {
                CorrectAnswer();
            confettiParticles[1].Play();
        }
        else
        {
            WrongAnswer();
        }

        }
    }

    public void Button3()
    {
        if (!buttonDisabled)
        {
            if (buttons[2].text == newNumber.ToString())
            {
            CorrectAnswer();
            confettiParticles[2].Play();
            }
            else
            {
            WrongAnswer();
            }

        }
    }


    //Correct and Wrong Answer Functions
    private void CorrectAnswer()
    {
        
        goodJobText.SetActive(true);
        buttonDisabled = true;

        //Good Job Audio
        audioSouce.clip = feedbackVoiceOvers[0];
        if (audioSouce.loop)
        {
            audioSouce.loop = false;
        }
        audioSouce.Play();

        //check If they have selected the wrong answer
        if (!wrongAnswer)
        {
            Debug.Log("Correct!");
            score += 1;
        }
        else
        {
            Debug.Log("Correct, but you got one wrong");
            wrongAnswer = false;
        }

        Invoke("HideText", 3f);
    }
    private void WrongAnswer()
    {
        Debug.Log("Try Again!");
        wrongAnswer = true;
        buttonDisabled = true;
        tryAgainText.SetActive(true);
        
        //Try Again Audio
        audioSouce.clip = feedbackVoiceOvers[1];
        if (audioSouce.loop)
        {
            audioSouce.loop = false;
        }
        audioSouce.Play();

        Invoke("HideText", 3f);

    }
    //Hide Messages upon button press
    private void HideText()
    {
        if (tryAgainText.activeSelf)
        {
            buttonDisabled = false;
            tryAgainText.SetActive(false);
        }

        if (goodJobText.activeSelf)
        {
            buttonDisabled = false;
            currentmesh.SetActive(false);
            numbersList.Remove(newNumber);
            goodJobText.SetActive(false);
            PrintNewNumber();
        }

    }

    //Menu Button Functions
    public void PlayGame()
    {
        audioSouce.Stop();
        score = 0;
        mainmenuPanel.SetActive(false);
        hudPanel.SetActive(true);
        PrintNewNumber();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        score = 0;
        endPanel.SetActive(false);
        hudPanel.SetActive(true);
        PrintNewNumber();
    }
    public void GoToMenu()
    {
        audioSouce.clip = bgMusic;
        if (!audioSouce.loop)
        {
            audioSouce.loop = true;
        }
        audioSouce.Play();

        endPanel.SetActive(false);
        mainmenuPanel.SetActive(true);
    }
    public void ToggleLanguageMenu()
    {
        if (languagePanel.activeSelf)
        {
            languagePanel.SetActive(false);
            menuPanel.SetActive(true);
            Debug.Log("actvie");
            
        }
        else
        {
            menuPanel.SetActive(false);
            languagePanel.SetActive(true);
            Debug.Log("nonactive");
        }
    }
    public void EnglishButton()
    {
        PlayerPrefs.SetInt("Language", 0);
        converter.LanguageUpdate();
    }
    public void SpanishButton()
    {
        PlayerPrefs.SetInt("Language", 1);
        converter.LanguageUpdate();
    }
    public void GreekButton()
    {
        PlayerPrefs.SetInt("Language", 2);
        converter.LanguageUpdate();
    }

    //Score Script
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

}
