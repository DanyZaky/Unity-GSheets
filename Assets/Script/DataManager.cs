using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public TMP_InputField nameInput;
    public TMP_InputField hpInput;
    public CSVManager csvManager;

    public Button submitButton;

    private void Start()
    {
        ResetForm();
    }

    private void Update()
    {
        FormCondition();
    }

    private void FormCondition()
    {
        if(nameInput.text == "" || hpInput.text == "")
        {
            submitButton.interactable = false;
        }
        else
        {
            submitButton.interactable = true;
        }
    }

    public void ResetForm()
    {
        nameInput.text = "";
        hpInput.text = "";
    }

    public void SimpanDataKeCSV()
    {
        string nama = nameInput.text.Trim();
        string nomorHP = hpInput.text.Trim();

        Debug.Log(nameInput.text + hpInput.text);

        csvManager.SimpanData(nama, nomorHP);
    }
}
