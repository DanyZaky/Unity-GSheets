using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FormController : MonoBehaviour
{
    public TMP_InputField nameInput, noHPInput;
    public Button submitButton;
    public GameController gc;

    public TextMeshProUGUI countdownText;
    public GameObject countDownPopUp;
    private float countDownValue;

    private bool isCountdown;

    [Header("Data")]
    public string nama;
    public string noHP;
    public int score;
    public string sisaWaktu;

    private void Start()
    {
        ResetForm();

        countDownValue = 5;
        isCountdown = false;
    }

    private void Update()
    {
        if(nameInput.text == "" || noHPInput.text == "")
        {
            submitButton.interactable = false;
        }
        else
        {
            if (gc.isInGame == true)
            {
                submitButton.interactable = false;
            }
            else
            {
                submitButton.interactable = true;
            }
        }

        if(isCountdown)
        {
            countDownPopUp.SetActive(true);
            countDownValue -= 1 * Time.deltaTime;
            countdownText.text = countDownValue.ToString("0");

            if(countDownValue <= 0)
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

    private void ResetForm()
    {
        nameInput.text = "";
        noHPInput.text = "";
        nama = null;
        noHP = null;
        score = 0;
        sisaWaktu = null;
        nameInput.readOnly = false;
        noHPInput.readOnly = false;
    }

    public void SubmitButton()
    {
        nama = nameInput.text;
        noHP = noHPInput.text;

        nameInput.readOnly = true;
        noHPInput.readOnly = true;

        StartCoroutine((countDown(5f)));
    }

    private IEnumerator countDown(float time)
    {
        isCountdown = true;

        yield return new WaitForSeconds(time);
        
        gc.isInGame = true;
    }
}
