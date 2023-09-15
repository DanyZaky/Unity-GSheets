using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AdminManager : MonoBehaviour
{
    public int difficulty;
    public int timeLeft;
    public bool isAllUser;

    [Header("========")]

    public TMP_InputField difficultyInput, timeInput;
    public Button allUser, winUser;

    public GameController gc;
    public MicrophoneController mc;
    public GSheetsReader gsr;

    private void Awake()
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
        PlayerPrefs.SetInt("TimeLeft", timeLeft);
        if(isAllUser)
        {
            PlayerPrefs.SetInt("ScoreBoardShowing", 0);
            allUser.interactable = false;
            winUser.interactable = true;
        }
        else
        {
            PlayerPrefs.SetInt("ScoreBoardShowing", 1);
            allUser.interactable = true;
            winUser.interactable = false;
        }

        SetInputField();
    }

    public void SetDefaultSetting()
    {
        difficulty = 3;
        timeLeft = 3;
        isAllUser = true;
    }

    private void SetInputField()
    {
        difficultyInput.text = PlayerPrefs.GetInt("Difficulty").ToString();
        timeInput.text = PlayerPrefs.GetInt("TimeLeft").ToString();
    }

    public void OnClickAllUser()
    {
        PlayerPrefs.SetInt("ScoreBoardShowing", 0);
        allUser.interactable = false;
        winUser.interactable = true;
        isAllUser = true;
        gsr.UpdateTextAllUserDisplay();
        StartCoroutine(gc.delayActive());
    }

    public void OnClickWinUser()
    {
        PlayerPrefs.SetInt("ScoreBoardShowing", 1);
        allUser.interactable = true;
        winUser.interactable = false;
        isAllUser = false;
        gsr.UpdateTextWinUserDisplay();
        StartCoroutine(gc.delayActive());
    }

    public void OnChange()
    {
        if (difficultyInput.text == "")
        {
            difficultyInput.text = PlayerPrefs.GetInt("Difficulty").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("Difficulty", int.Parse(difficultyInput.text));
            mc.maxValue = PlayerPrefs.GetInt("Difficulty");
        }

        if (timeInput.text == "")
        {
            timeInput.text = PlayerPrefs.GetInt("TimeLeft").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("TimeLeft", int.Parse(timeInput.text));
            gc.startTime = PlayerPrefs.GetInt("TimeLeft");
            gc.currentTime = gc.startTime;
        }
    }
}
