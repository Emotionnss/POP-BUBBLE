using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameScore : MonoBehaviour
{
    public static int score;
    public GameObject currentscore, newScore, death;
    public bool isDead = false;
    public GameObject namae;

    private float timer;

    void Start()
    {
        score = 0;
    }

    void Update()
    {
        currentscore.GetComponent<TextMeshProUGUI>().SetText("Score: " + score);
        newScore.GetComponent<TextMeshProUGUI>().SetText("Score: " + score);
    }

    public IEnumerator AddScore()
    {
        while(isDead == false)
        {
            yield return new WaitForSeconds(1);
            ++score;
        }
    }
    public void Submit()
    {
        Highscores.AddNewHighscore(namae.GetComponent<TextMeshProUGUI>().text.ToUpper(), score);
        GameObject.FindGameObjectWithTag("Input").SetActive(false);
        death.SetActive(true);
    }
}
