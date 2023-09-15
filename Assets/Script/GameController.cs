using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public FormController fc;
    public MicrophoneController mc;
    public TextMeshProUGUI conditionText, scoreText, timeText;
    public GameObject winPanel, losePanel;
    public TextMeshProUGUI winDesc, loseDesc;
    public Button AdminButton;
    public GameObject leaderboard;
    public float startTime;

    private int score;
    public bool isInGame, isGameOver;
    private int isWin;

    public float currentTime;

    private void Start()
    {
        isInGame = false;
        isGameOver = false;
        startTime = PlayerPrefs.GetInt("TimeLeft");
        currentTime = startTime;

        isWin = 2;

        losePanel.SetActive(false);
        winPanel.SetActive(false);

        StartCoroutine(delayActive());
    }

    private void Update()
    {
        

        if (!isInGame)
        {
            conditionText.text = "Please Input to Start the Game!";
            scoreText.text = "";
            timeText.text = "";
            AdminButton.interactable = true;
        }
        else
        {
            if(score < (mc.audioImage.GetComponent<Image>().fillAmount * 100f))
            {
                score = ((int)((mc.audioImage.GetComponent<Image>().fillAmount) * 100));
            }

            if (score == 100)
            {
                isWin = 0;
                isGameOver = true;
            }
            
            conditionText.text = "Get 5 Stars to Get Reward";
            scoreText.text = "Your Score : " + score.ToString("0");
            timeText.text = "";

            if(!isGameOver)
            {
                mc.audioSource.mute = false;
                currentTime -= 1 * Time.deltaTime;
                timeText.text = currentTime.ToString("0") + " waktu tersisa";

                if (currentTime <= 0)
                {
                    isGameOver = true;
                    currentTime = 0;
                    isWin = 1;
                }
            }
            else
            {
                mc.audioSource.mute = true;
                if (isWin == 0)
                {
                    winPanel.SetActive(true);
                    winDesc.text = fc.nama + " , kamu mendapatkan hadiah!";
                    fc.sisaWaktu = currentTime.ToString("0");
                    fc.score = score;
                }
                else if(isWin == 1)
                {
                    losePanel.SetActive(true);
                    loseDesc.text = fc.nama + " , kamu memperoleh skor " + score.ToString("0") + "!";
                    fc.sisaWaktu = currentTime.ToString("0");
                    fc.score = score;
                }
            }

            AdminButton.interactable = false;
        }
    }

    public IEnumerator delayActive()
    {
        leaderboard.SetActive(false);
        yield return new WaitForSeconds(1f);
        leaderboard.SetActive(true);
    }
}
