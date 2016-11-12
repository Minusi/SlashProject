using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    public GameObject AchievementTab;
    public GameObject RankingTab;
    public GameObject[] Btn;
    public GameObject BackBtn;

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void FloatRankingTab()
    {
        RankingTab.GetComponent<SpriteRenderer>().sortingOrder = 2;
        for (int i = 0; i < Btn.Length; i++)
        {
            Btn[i].SetActive(false);
        }
        BackBtn.SetActive(true);
    }

    public void FloatAchievementTab()
    {
        AchievementTab.GetComponent<SpriteRenderer>().sortingOrder = 2;
        for(int i=0;i<Btn.Length;i++)
        {
            Btn[i].SetActive(false);
        }
        BackBtn.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        AchievementTab.GetComponent<SpriteRenderer>().sortingOrder = 0;
        RankingTab.GetComponent<SpriteRenderer>().sortingOrder = 0;
        for (int i = 0; i < Btn.Length; i++)
        {
            Btn[i].SetActive(true);
        }
        BackBtn.SetActive(false);
    }
}
