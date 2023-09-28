using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageConverter : MonoBehaviour
{
    [Header("Main Menu Texts")]
    public TextMeshProUGUI gameTitle;
    public TextMeshProUGUI playButton;
    public TextMeshProUGUI languageButton;
    public TextMeshProUGUI quitGameButton;

    [Header("HUD Texts")]
    public TextMeshProUGUI goodJobText;
    public TextMeshProUGUI tryAgainText;

    [Header("End Menu Texts")]
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI playAgainButton;
    public TextMeshProUGUI quitButton;


    public void LanguageUpdate()
    {

        if (PlayerPrefs.GetInt("Language") == 0)
        {
            ConvertToEnglish();
        }
        else if (PlayerPrefs.GetInt("Language") == 1)
        {
            ConvertToSpanish();
        }
        else if (PlayerPrefs.GetInt("Language") == 2)
        {
            ConvertToGreek();
        }
        else
        {
            PlayerPrefs.SetInt("Language", 0);
        }

    }


    private void ConvertToEnglish()
    {
        //Main Menu Text Conversions
        gameTitle.text = "Floating Numbers";
        playButton.text = "Play";
        languageButton.text = "Language";
        quitGameButton.text = "Quit Game";

        //HUD Text Conversions
        goodJobText.text = "Good Job!";
        tryAgainText.text = "Try Again!";

        //End Menu Text Conversions
        gameOverText.text = "Great Job!";
        playAgainButton.text = "Play Again";
        quitButton.text = "Main Menu";
        //scoreText.text = "";
    }

    private void ConvertToSpanish()
    {
        //Main Menu Text Conversions
        gameTitle.text = "Números Flotantes";
        playButton.text = "Empezar";
        languageButton.text = "Idioma";
        quitGameButton.text = "Salida";

        //HUD Text Conversions
        goodJobText.text = "¡Muy Bien!";
        tryAgainText.text = "Pruebalo Otra Vez";

        //End Menu Text Conversions
        gameOverText.text = "¡Enorabuena!";
        playAgainButton.text = "Empezar De Nuevo";
        quitButton.text = "Salida";
        //scoreText.text = "";
    }

    private void ConvertToGreek()
    {
        //Main Menu Text Conversions
        gameTitle.text = "Αιωρούμενοι Αριθμοί";
        playButton.text = "Παίξε";
        languageButton.text = "Γλώσσα";
        quitGameButton.text = "Έξοδος";

        //HUD Text Conversions
        goodJobText.text = "Μπραβο!";
        tryAgainText.text = "Ξαναπροσπάθησε";

        //End Menu Text Conversions
        gameOverText.text = "Συγχαρητήρια";
        playAgainButton.text = "Ξαναπαίξε";
        quitButton.text = "Έξοδος";
        //scoreText.text = "";
    }
}


