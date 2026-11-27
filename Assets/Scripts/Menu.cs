using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator anim;
    public GameObject helpScreen;

    void Update()
    {
        
    }

    public void Play()
    {
        anim.SetTrigger("FadeOut");
    }

    public void FadeComplete()
    {
        SceneManager.LoadScene("Game");
    }

    public void Help()
    {
        helpScreen.SetActive(true);
    }

    public void Quitting()
    {
        Application.Quit();
    }

    public void Back()
    {
        helpScreen.SetActive(false);
    }
}
