using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SystemManager : MonoBehaviour
{
    public GameObject[] scenesUI;
    public GameObject loadingPanel;
    public TextMeshProUGUI gameoverText;

    public FormManager fm;
    public float currentTimer;
    private float currentScore;

    public TextMeshProUGUI textTimer;
    public TextMeshProUGUI textScore;

    public Image indicatorValueImage;

    public AudioSource audioSource;
    public string selectedDevice;

    public static float[] samples = new float[128];
    public GameObject[] star;
    public float difficultyValue;

    [Header("Data")]
    public float timer;
    public float timeLeft;
    public float score;

    private void Start()
    {
        for (int i = 0; i < star.Length; i++)
        {
            star[i].SetActive(false);
        }
        
        for (int i = 0; i < scenesUI.Length; i++)
        {
            scenesUI[i].SetActive(false);
        }
        scenesUI[0].SetActive(true);

        currentTimer = PlayerPrefs.GetFloat("AdminTime", timer);
        

        loadingPanel.SetActive(false);

        MicrophoneStarter();
    }

    private void Update()
    {
        Debug.Log(PlayerPrefs.GetFloat("AdminTime"));
        if (fm.isInGame)
        {
            currentTimer -= 1 * Time.deltaTime;
            textTimer.text = currentTimer.ToString("0");

            float convertedScore = currentScore * 100;

            if(score < convertedScore)
            {
                score = convertedScore;
                textScore.text = score.ToString("0");
            }
            
            if(currentTimer <= 0)
            {
                currentTimer = 0;
                timeLeft = currentTimer;
                scenesUI[2].SetActive(false);
                scenesUI[3].SetActive(true);
                fm.isInGame = false;
                GetStar();

                gameoverText.text = PlayerPrefs.GetString("AdminLose");

                Debug.Log("lose");
            }

            if(score >= 100)
            {
                score = 100;
                timeLeft = currentTimer;
                scenesUI[2].SetActive(false);
                scenesUI[3].SetActive(true);
                fm.isInGame = false;
                GetStar();

                gameoverText.text = PlayerPrefs.GetString("AdminWin");

                Debug.Log("win");
            }

            audioSource.mute = false;
        }
        else
        {
            audioSource.mute = true;
        }

        GetOutputData();
    }

    private void MicrophoneStarter()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = true;

        if (Microphone.devices.Length > 0)
        {
            selectedDevice = Microphone.devices[0].ToString();
            audioSource.clip = Microphone.Start(selectedDevice, true, 1, AudioSettings.outputSampleRate);
            audioSource.loop = true;

            while (!(Microphone.GetPosition(selectedDevice) > 0))
            {
                audioSource.Play();
            }
        }
    }

    void GetOutputData()
    {
        audioSource.GetOutputData(samples, 0);

        float vals = 0.0f;

        for (int i = 0; i < 128; i++)
        {
            vals += Mathf.Abs(samples[i]);
        }
        vals /= 128.0f;

        float db = 1.0f + (vals * 10.0f);
        currentScore = (db - 1) / (difficultyValue - 1);
        indicatorValueImage.GetComponent<Image>().fillAmount = currentScore;
        Debug.Log("value = " + currentScore);
    }

    private void GetStar()
    {
        if (score <= 0)
        {
            for (int i = 0; i < star.Length; i++)
            {
                star[i].SetActive(false);
            }
        }
        else if (score > 0 && score <= 20)
        {
            for (int i = 0; i < star.Length; i++)
            {
                star[i].SetActive(false);
            }
            star[0].SetActive(true);
        }
        else if (score > 20 && score <= 40)
        {
            for (int i = 0; i < star.Length; i++)
            {
                star[i].SetActive(false);
            }
            star[0].SetActive(true);
            star[1].SetActive(true);
        }
        else if (score > 40 && score <= 60)
        {
            for (int i = 0; i < star.Length; i++)
            {
                star[i].SetActive(false);
            }
            star[0].SetActive(true);
            star[1].SetActive(true);
            star[2].SetActive(true);
        }
        else if (score > 60 && score <= 80)
        {
            for (int i = 0; i < star.Length; i++)
            {
                star[i].SetActive(false);
            }
            star[0].SetActive(true);
            star[1].SetActive(true);
            star[2].SetActive(true);
            star[3].SetActive(true);
        }
        else if (score > 80 && score >= 100)
        {
            for (int i = 0; i < star.Length; i++)
            {
                star[i].SetActive(true);
            }
        }
    }

    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfOLtcg2yRV9v7WxVl7I1NI8rmVchsrGWF46fVqCpMYnmG77A/formResponse";

    IEnumerator Post(string name, string noHP, string score, string timeLeft)
    {
        loadingPanel.SetActive(true);

        WWWForm form = new WWWForm();
        form.AddField("entry.1186336207", name);
        form.AddField("entry.1234230737", noHP);
        form.AddField("entry.484645608", score);
        form.AddField("entry.1469875099", timeLeft);
        byte[] rawData = form.data;

        UnityWebRequest www = UnityWebRequest.Post(BASE_URL, form);
        yield return www.SendWebRequest();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SubmitButton()
    {
        StartCoroutine(Post(fm.nama, fm.noHP, score.ToString("0"), timeLeft.ToString("0")));
    }
}
