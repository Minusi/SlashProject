using UnityEngine;
using System.Collections;
using System;

public class CreateStep  {

    public void CreateSteps(MiniMap miniMap, GameObject[] steps, int mapsize)
    {
        Point p = new Point();
        Point p_s = new Point();
        Point p_result = new Point();
        int result = 0;
        int tmp = 0;
        for (int i = 0; i < mapsize; i++)
        {
            for (int j = 0; j < mapsize; j++)
            {
                if (miniMap[i, j] != null)
                {
                    if (miniMap[i, j].isSource == true)
                    {
                        p_s.x = i;
                        p_s.y = j;
                    }
                }
            }
        }

        for (int i = 0; i < mapsize; i++)
        {
            for (int j = 0; j < mapsize; j++)
            {
                if (miniMap[i, j] != null)
                {
                    p.x = i;
                    p.y = j;
                    tmp = Math.Abs(p_s.x - p.x) + Math.Abs(p_s.y - p.y);
                    if (result < tmp)
                    {
                        result = tmp;
                        p_result.x = p.x;
                        p_result.y = p.y;
                    }
                }
            }
        }

        miniMap[p_result.x, p_result.y].isStep = true;
        int point = 0;
        for (int i = 0; i < mapsize; i++)
        {
            for (int j = 0; j < mapsize; j++)
            {
                if (miniMap[i, j] != null)
                {
                    if (miniMap[i, j].isStep == true)
                    {
                        steps[point].SetActive(true);
                    }
                }
                point++;
            }
        }
    }
}
