using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class MicrophoneController : MonoBehaviour
{
    public AudioSource audioSource;

    public GameObject audioImage;

    public string selectedDevice;

    public float maxValue;

    public static float[] samples = new float[128];

    public GameObject[] star;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = true;
        maxValue = PlayerPrefs.GetInt("Difficulty");

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


    void Update()
    {
        getOutputData();
    }

    void getOutputData()
    {
        audioSource.GetOutputData(samples, 0);

        float vals = 0.0f;

        for (int i = 0; i < 128; i++)
        {
            vals += Mathf.Abs(samples[i]);
        }
        vals /= 128.0f;

        float db = 1.0f + (vals * 10.0f);
        float convertedValue = (db -1) / (maxValue -1);

        //Debug.Log(convertedValue + "/" + db);

        audioImage.GetComponent<Image>().fillAmount = convertedValue;

        if (convertedValue <= 0)
        {
            for (int i = 0; i < star.Length; i++)
            {
                star[i].SetActive(false);
            }
        }
        else if (convertedValue > 0 && convertedValue <= 0.2)
        {
            for (int i = 0; i < star.Length; i++)
            {
                star[i].SetActive(false);
            }
            star[0].SetActive(true);
        }
        else if (convertedValue > 0.2 && convertedValue <= 0.4)
        {
            for (int i = 0; i < star.Length; i++)
            {
                star[i].SetActive(false);
            }
            star[0].SetActive(true);
            star[1].SetActive(true);
        }
        else if (convertedValue > 0.4 && convertedValue <= 0.6)
        {
            for (int i = 0; i < star.Length; i++)
            {
                star[i].SetActive(false);
            }
            star[0].SetActive(true);
            star[1].SetActive(true);
            star[2].SetActive(true);
        }
        else if (convertedValue > 0.6 && convertedValue <= 0.8)
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
        else if (convertedValue > 0.8 && convertedValue <= 1)
        {
            for (int i = 0; i < star.Length; i++)
            {
                star[i].SetActive(true);
            }
        }
    }

    public void IsToMainGame()
    {
        audioSource.mute = false;
    }

    public void IsNotToMainGame()
    {
        audioSource.mute = true;
    }

}
