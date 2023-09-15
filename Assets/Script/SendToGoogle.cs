using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    public FormController fc;

    public GameObject loadingPanel;
    public GameObject[] gameConditionPanel;

    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfOLtcg2yRV9v7WxVl7I1NI8rmVchsrGWF46fVqCpMYnmG77A/formResponse";

    IEnumerator Post(string name, string noHP, string score, string timeLeft)
    {
        loadingPanel.SetActive(true);
        for (int i = 0; i < gameConditionPanel.Length; i++)
        {
            gameConditionPanel[i].SetActive(false);
        }

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
    public void Send()
    {
        StartCoroutine(Post(fc.nama, fc.noHP, fc.score.ToString(), fc.sisaWaktu));
    }
}
