using UnityEngine;
using System.Collections;

public class MiniMapDisPlay {
   
    public void minimapDisPlay(GameObject[] rooms, GameObject[] steps, int mapsize, MiniMap miniMap)
    {
        int point = 0;

        for (int i=0;i< mapsize; i++)
        {
            for(int j=0;j< mapsize; j++)
            {
                if (miniMap[i, j] != null)
                {
                    rooms[point].SetActive(true);
                    if (miniMap[i, j].isStep == true)
                    {
                        steps[point].SetActive(true);
                    }
                }
                point ++;
            }
        }
    }
}
