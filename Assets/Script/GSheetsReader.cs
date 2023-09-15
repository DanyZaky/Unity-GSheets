using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using SimpleJSON;
using TMPro;
using System.Linq;

public class GSheetsReader : MonoBehaviour
{
    [TextArea]
    public string baseUrl = "https://sheets.googleapis.com/v4/spreadsheets/1ix6FL9oyxaZJ51SZ44HOm-TcKCh6fQSsFVrUUD8V6OE/values/FormResponses?key=AIzaSyDiO9vN0skQQz1r6Da_dSCuzrXRf029e_w";

    [SerializeField]
    private TextMeshProUGUI nameText; // Reference to the Text component for displaying name
    [SerializeField]
    private TextMeshProUGUI scoreText; // Reference to the Text component for displaying score

    public GameObject ScrollViewObj;

    [System.Serializable]
    public class AllUserScoreData
    {
        public string Nama;
        public int Score; // Change the data type to int

        public AllUserScoreData(string nama, string score)
        {
            Nama = nama;
            Score = int.Parse(score); // Parse the score as an integer
        }
    }

    [System.Serializable]
    public class WinUserScoreData
    {
        public string Nama;
        public int Score; // Change the data type to int

        public WinUserScoreData(string nama, string score)
        {
            Nama = nama;
            Score = int.Parse(score); // Parse the score as an integer
        }
    }

    public List<AllUserScoreData> allUserScoreDataList = new List<AllUserScoreData>();
    public List<WinUserScoreData> winUserScoreDataList = new List<WinUserScoreData>();

    private void Start()
    {
        StartCoroutine(GetJSONFromURL());
        StartCoroutine(DelayActive());
    }

    public IEnumerator DelayActive()
    {
        ScrollViewObj.SetActive(false);
        yield return new WaitForSeconds(1f);
        ScrollViewObj.SetActive(true);
    }

    private IEnumerator GetJSONFromURL()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(baseUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching JSON: " + webRequest.error);
            }
            else
            {
                string jsonContent = webRequest.downloadHandler.text;
                JSONNode jsonData = JSON.Parse(jsonContent);

                JSONArray valueData = jsonData["values"].AsArray;

                for (int i = 1; i < valueData.Count; i++)
                {
                    JSONNode innerArray = valueData[i];
                    string nama = innerArray[1];
                    string score = innerArray[3];

                    AllUserScoreData newAllScoreData = new AllUserScoreData(nama, score);
                    allUserScoreDataList.Add(newAllScoreData);

                    if(score == "100")
                    {
                        WinUserScoreData newWinScoreData = new WinUserScoreData(nama, score);
                        winUserScoreDataList.Add(newWinScoreData);
                    }
                }

                allUserScoreDataList.Sort((a, b) => b.Score.CompareTo(a.Score)); // Sort in descending order
                winUserScoreDataList.Sort((a, b) => a.Nama.CompareTo(b.Nama)); // Sort in ascending order

                UpdateTextAllUserDisplay();
            }
        }
    }

    public void UpdateTextAllUserDisplay()
    {
        string nameTextContent = "";
        string scoreTextContent = "";

        foreach (AllUserScoreData scoreData in allUserScoreDataList)
        {
            nameTextContent += scoreData.Nama + "\n";
            scoreTextContent += scoreData.Score + "\n";
        }

        nameText.text = nameTextContent;
        scoreText.text = scoreTextContent;
    }

    public void UpdateTextWinUserDisplay()
    {
        string nameTextContent = "";
        string scoreTextContent = "";

        foreach (WinUserScoreData scoreData in winUserScoreDataList)
        {
            nameTextContent += scoreData.Nama + "\n";
            scoreTextContent += scoreData.Score + "\n";
        }

        nameText.text = nameTextContent;
        scoreText.text = scoreTextContent;
    }
}
