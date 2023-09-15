using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DashboardManager : MonoBehaviour
{
    public GSheetsReader gsr;
    public SystemManager sm;
    public TMP_InputField difficultyInput, timeInput, winInput, loseInput;
    public Button allUser, winUser, saveButton;

    public TMP_InputField passwordInput;
    public GameObject passwordPanel;

    private void Start()
    {
        difficultyInput.text = PlayerPrefs.GetFloat("AdminDifficulty", 5).ToString("0");
        timeInput.text = PlayerPrefs.GetFloat("AdminTime", 3).ToString("0");
        winInput.text = PlayerPrefs.GetString("AdminWin", "Selamat, kamu mendapatkan hadiah!");
        loseInput.text = PlayerPrefs.GetString("AdminLose", "Kamu belum mendapatkan skor maksimal");

        sm.difficultyValue = PlayerPrefs.GetFloat("AdminDifficulty", 5);
        sm.timer = PlayerPrefs.GetFloat("AdminTime");

        SetVisibleScoreboard(0);
    }
        
    public void OnValueChangedAdmin()
    {
        saveButton.interactable = true;
    }

    public void SaveButton()
    {
        saveButton.interactable = false;

        PlayerPrefs.SetFloat("AdminDifficulty", float.Parse(difficultyInput.text));
        PlayerPrefs.SetFloat("AdminTime", float.Parse(timeInput.text));
        PlayerPrefs.SetString("AdminWin", winInput.text);
        PlayerPrefs.SetString("AdminLose", loseInput.text);

        sm.difficultyValue = PlayerPrefs.GetFloat("AdminDifficulty");
        sm.currentTimer = PlayerPrefs.GetFloat("AdminTime");
    }

    public void ResetDefault()
    {
        difficultyInput.text = 5.ToString("0");
        timeInput.text = 3.ToString("0");
        winInput.text = "Selamat, kamu mendapatkan hadiah!";
        loseInput.text = "Kamu belum mendapatkan skor maksimal";

        PlayerPrefs.SetFloat("AdminDifficulty", float.Parse(difficultyInput.text));
        PlayerPrefs.SetFloat("AdminTime", float.Parse(timeInput.text));
        PlayerPrefs.SetString("AdminWin", winInput.text);
        PlayerPrefs.SetString("AdminLose", loseInput.text);

        sm.difficultyValue = PlayerPrefs.GetFloat("AdminDifficulty");
        sm.timer = PlayerPrefs.GetFloat("AdminTime");

        SetVisibleScoreboard(0);
    }

    public void SetVisibleScoreboard(int index)
    {
        if(index == 0)
        {
            gsr.UpdateTextAllUserDisplay();
            allUser.interactable = false;
            winUser.interactable = true;
            StartCoroutine(gsr.DelayActive());
        }
        else if(index == 1)
        {
            gsr.UpdateTextWinUserDisplay();
            allUser.interactable = true;
            winUser.interactable = false;
            StartCoroutine(gsr.DelayActive());
        }
    }

    public void OnClickPassword()
    {
        passwordPanel.SetActive(true);
    }

    public void Login()
    {
        if (passwordInput.text == "#hexosbuzz")
        {
            passwordPanel.SetActive(false);
            passwordInput.text = "";
        }
        else
        {
            passwordPanel.SetActive(true);
        }
    }
}
