using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class DisplayHighscores : MonoBehaviour
{

    public GameObject[] highscoreFields;
    Highscores highscoresManager;

    void Start()
    {
        for (int i = 0; i < highscoreFields.Length; i++)
        {
            highscoreFields[i].GetComponent<TextMeshProUGUI>().SetText(i + 1 + ". Unknown");
        }


        highscoresManager = GetComponent<Highscores>();
        StartCoroutine("RefreshHighscores");
    }

    public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreFields.Length; i++)
        {
            //highscoreFields[i].GetComponent<TextMeshProUGUI>().SetText( i + 1 + ". ");
            if (i < highscoreList.Length)
            {
                highscoreFields[i].GetComponent<TextMeshProUGUI>().SetText(i + 1 + ". " + highscoreList[i].username + " - " + highscoreList[i].score);
            }
        }
    }

    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highscoresManager.DownloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }
}