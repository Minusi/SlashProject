using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text scoretxt;

    public static GameManager GetInstance()
    {
        return null;
    }

    public Stage[] stages;

    int stageLevel;
    public float fscore = 10000;

    MiniMapGenerator minimapGenerator;
    MiniMap map;

    void LoadStage()
    {
        if (stageLevel == 1)
        {
            // 스테이지 1일때
        }
    }

    void ScoreDisplay()
    {
        int iscore = (int)fscore;
        scoretxt.text = iscore.ToString();
        fscore -= 0.1f;
    }

    void Update()
    {
        ScoreDisplay();
    }
}
