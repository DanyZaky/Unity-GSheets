using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FormManager : MonoBehaviour
{
    public TMP_InputField nameInput, noHPInput;
    public Button submitButton;

    public TextMeshProUGUI countdownText;
    public GameObject countDownPopUp;
    private float countDownValue;
    private bool isCountdown;

    public bool isInGame;

    public GameObject scene2, scene3;

    [Header("Data")]
    public string nama;
    public string noHP;
    public int score;
    public string sisaWaktu;

    private void Start()
    {
        ResetForm();

        isInGame = false;
        isCountdown = false;
        countDownValue = 5;

        scene2.SetActive(false);
        scene3.SetActive(false);
    }

    private void Update()
    {
        if (nameInput.text == "" || noHPInput.text == "")
        {
            submitButton.interactable = false;
        }
        else
        {
            submitButton.interactable = true;

            if (scene2.activeInHierarchy == true)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SubmitButton();
                }
            }
        }

        CountdownToInGame();
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void ResetForm()
    {
        nameInput.text = "";
        noHPInput.text = "";
        nama = null;
        noHP = null;
        score = 0;
        sisaWaktu = null;
    }

    public void SubmitButton()
    {
        nama = nameInput.text;
        noHP = noHPInput.text;

        nameInput.readOnly = true;
        noHPInput.readOnly = true;

        scene2.SetActive(false);
        scene3.SetActive(true);

        StartCoroutine((countDown(5f)));
    }

    private IEnumerator countDown(float time)
    {
        isCountdown = true;

        yield return new WaitForSeconds(time);

        isInGame = true;
    }

    private void CountdownToInGame()
    {
        if (isCountdown)
        {
            countDownPopUp.SetActive(true);
            countDownValue -= 1 * Time.deltaTime;
            countdownText.text = countDownValue.ToString("0");

            if (countDownValue <= 0)
            {
                countDownValue = 0;
                isCountdown = false;
            }
        }
        else
        {
            countDownPopUp.SetActive(false);
        }
    }
}
