using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject mainCamere;
    public GameObject player;
    public GameObject AllMap;
    public GameObject ReStartBtn;
    public GameObject GoMenuBtn;
    public GameObject Panel;
    public GameObject UI;
    public GameObject ClearUI;
    public GameObject[] s1_doors;
    public GameObject[] s2_doors;
    public GameObject[] s1_rooms;
    public GameObject[] s2_rooms;
    public GameObject[] s1_steps;
    public GameObject[] s2_steps;
    public Text scoretxt;
    MiniMapGenerator minimapGenerator = new MiniMapGenerator();
    MiniMapDisPlay mapDisPlay = new MiniMapDisPlay();
    MiniMap miniMap;
    CreateStep createstep = new CreateStep();
    static int mapsize;
    public int stage;
    static public float fscore;

    void Awake()
    {
        stage = 1;
        mapsize = 0;
        fscore = 10000;
        miniMap = minimapGenerator.Generate(stage);
        mapsize = minimapGenerator.Mapsize(stage);
    }

    void Start()
    {
        LoadStage();
        CameraMove(stage);
    }

    void Update()
    {
        ScoreDisplay();
    }
    

    void LoadStage()
    {
        if (stage == 1)
        {
            createstep.CreateSteps(miniMap, s1_steps, mapsize);
            mapDisPlay.minimapDisPlay(s1_rooms, s1_steps, mapsize, miniMap);
        }
        else if (stage == 2)
        {
            createstep.CreateSteps(miniMap, s2_steps, mapsize);
            mapDisPlay.minimapDisPlay(s2_rooms, s2_steps, mapsize, miniMap);
        }
    }

    void ScoreDisplay()
    {
        int iscore = (int)fscore;
        scoretxt.text = iscore.ToString();
        fscore -= 0.1f;
    }


    void CameraMove(int stage)
    {
        if (stage == 1)
        {
            for (int i = 0; i < mapsize; i++)
            {
                for (int j = 0; j < mapsize; j++)
                {
                    int p = 0;
                    if (i == 0 && j == 0) p = 0;
                    else if (i == 0 && j == 1) p = 1;
                    else if (i == 0 && j == 2) p = 2;
                    else if (i == 1 && j == 0) p = 3;
                    else if (i == 1 && j == 1) p = 4;
                    else if (i == 1 && j == 2) p = 5;
                    else if (i == 2 && j == 0) p = 6;
                    else if (i == 2 && j == 1) p = 7;
                    else  p = 8;                        // (i == 2 && j == 2) 
                    if (miniMap[i, j] != null)
                    {
                        if (miniMap[i, j].isSource == true)
                        {
                            //s1_rooms[p].GetComponent<SpriteRenderer>().color = Color.red;
                            mainCamere.transform.Translate(new Vector2(i * 20, j * 20));
                            player.transform.Translate(new Vector2(i * 20, j * 20));
                        }
                        if (miniMap[i, j].hasUp == true)
                        {
                            s1_doors[p * 4 + 0].SetActive(true);
                        }
                        if (miniMap[i, j].hasRight == true)
                        {
                            s1_doors[p * 4 + 1].SetActive(true);
                        }
                        if (miniMap[i, j].hasLeft == true)
                        {
                            s1_doors[p * 4 + 2].SetActive(true);
                        }
                        if (miniMap[i, j].hasDown == true)
                        {
                            s1_doors[p * 4 + 3].SetActive(true);
                        }
                    }
                }
            }
        }
        else  if (stage == 2)
        {
            for (int i = 0; i < mapsize; i++)
            {
                for (int j = 0; j < mapsize; j++)
                {
                    int p = 0;
                    if (i == 0 && j == 0) p = 0;
                    else if (i == 0 && j == 1) p = 1;
                    else if (i == 0 && j == 2) p = 2;
                    else if (i == 0 && j == 3) p = 3;
                    else if (i == 1 && j == 0) p = 4;
                    else if (i == 1 && j == 1) p = 5;
                    else if (i == 1 && j == 2) p = 6;
                    else if (i == 1 && j == 3) p = 7;
                    else if (i == 2 && j == 0) p = 8;
                    else if (i == 2 && j == 1) p = 9;
                    else if (i == 2 && j == 2) p = 10;
                    else if (i == 2 && j == 3) p = 11;
                    else if (i == 3 && j == 0) p = 12;
                    else if (i == 3 && j == 1) p = 13;
                    else if (i == 3 && j == 2) p = 14;
                    else p = 15;                        // (i == 3 && j == 3)
                    if (miniMap[i, j] != null)
                    {
                        if (miniMap[i, j].isSource == true)
                        {
                            mainCamere.transform.Translate(new Vector2(i * 20, j * 20));
                            player.transform.Translate(new Vector2(i * 20, j * 20));
                        }
                        if (miniMap[i, j].hasUp == true)
                        {
                            s2_doors[p * 4 + 0].SetActive(true);
                        }
                        if (miniMap[i, j].hasRight == true)
                        {
                            s2_doors[p * 4 + 1].SetActive(true);
                        }
                        if (miniMap[i, j].hasLeft == true)
                        {
                            s2_doors[p * 4 + 2].SetActive(true);
                        }
                        if (miniMap[i, j].hasDown == true)
                        {
                            s2_doors[p * 4 + 3].SetActive(true);
                        }
                    }
                }
            }
        }
    }

    public void NextRoom(int Doordirec)
    {
        if (Doordirec == (int)DoorDirec.Up)
        {
            mainCamere.transform.Translate(new Vector2(0, 20));
            player.transform.Translate(new Vector2(0, 13));
        }
        else if (Doordirec == (int)DoorDirec.Right)
        {
            mainCamere.transform.Translate(new Vector2(20, 0));
            player.transform.Translate(new Vector2(10, 0));
        }
        else if (Doordirec == (int)DoorDirec.Left)
        {
            mainCamere.transform.Translate(new Vector2(-20, 0));
            player.transform.Translate(new Vector2(-10, 0));
        }
        else if (Doordirec == (int)DoorDirec.Down)
        {
            mainCamere.transform.Translate(new Vector2(0, -20));
            player.transform.Translate(new Vector2(0, -13));
        }
        AllMap.SetActive(false);
    }

    public void NextStage()
    {
        int i=0;
        stage++;
        while (i < mapsize * mapsize)
        {
            if (stage == 2)
            {
                s1_rooms[i].SetActive(false);
                i++;
            }
        }
        miniMap = minimapGenerator.Generate(stage);
        mapsize = minimapGenerator.Mapsize(stage);
        LoadStage();
        mainCamere.transform.position = new Vector3(100, -100, -10);
        player.transform.position = new Vector3(96, -100, -1);         
        CameraMove(stage);
    }

    void Pause()
    {

    }

    void OnEnable()
    {
        Player.OnDie += GameOver;
    }

    void OnDisable()
    {
        Player.OnDie -= GameOver;
    }

    public void GameOver()
    {
        Panel.SetActive(true);
    }

    public void ReStart()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void GameClear()
    {
        UI.SetActive(false);
        ClearUI.SetActive(true);
        mainCamere.transform.position = new Vector3(1000, 0, -10);
    }


}
