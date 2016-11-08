using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager GetInstance()
    {
        return null;
    }

    public Stage[] stages;

    int stageLevel;
    int score;

    MiniMapGenerator minimapGenerator;
    MiniMap map;

    void LoadStage()
    {
    }

}
