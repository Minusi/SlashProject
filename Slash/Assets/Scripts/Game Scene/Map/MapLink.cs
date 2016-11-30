using UnityEngine;
using System.Collections;
using System;

public class MapLink        // 부를 클래스
{
    MiniMapGenerator minimapGenerator;
    public void Link(MiniMap miniMap, int stage)
    {
        minimapGenerator = new MiniMapGenerator();


        int mapsize = minimapGenerator.Mapsize(stage);
        for (int x = 0; x < mapsize; x++)
        {
            for (int y = 0; y < mapsize; y++)
            {
                if (miniMap[x, y] != null)
                {

                    //if(miniMap[x, y].isSource== true){
                    //    CreateStep(miniMap, x, y);
                    //}

                    if (x == 0)
                    {
                        if (y == 0)
                        {
                            if (miniMap[x + 1, y] != null)
                            {
                                miniMap[x, y].hasRight = true;
                                miniMap[x + 1, y].hasLeft = true;
                            }
                            if (miniMap[x, y + 1] != null)
                            {
                                miniMap[x, y].hasUp = true;
                                miniMap[x, y + 1].hasDown = true;
                            }
                        }
                        else if (y == mapsize - 1)
                        {
                            if (miniMap[x + 1, y] != null)
                            {
                                miniMap[x, y].hasRight = true;
                                miniMap[x + 1, y].hasLeft = true;
                            }
                            if (miniMap[x, y - 1] != null)
                            {
                                miniMap[x, y].hasDown = true;
                                miniMap[x, y - 1].hasUp = true;
                            }
                        }
                        else
                        {
                            if (miniMap[x + 1, y] != null)
                            {
                                miniMap[x, y].hasRight = true;
                                miniMap[x + 1, y].hasLeft = true;
                            }
                            if (miniMap[x, y - 1] != null)
                            {
                                miniMap[x, y].hasDown = true;
                                miniMap[x, y - 1].hasUp = true;
                            }
                            if (miniMap[x, y + 1] != null)
                            {
                                miniMap[x, y].hasUp = true;
                                miniMap[x, y + 1].hasDown = true;
                            }
                        }
                    }


                    else if (x == mapsize - 1)
                    {
                        if (y == 0)
                        {
                            if (miniMap[x - 1, y] != null)
                            {
                                miniMap[x, y].hasLeft = true;
                                miniMap[x - 1, y].hasRight = true;
                            }
                            if (miniMap[x, y + 1] != null)
                            {
                                miniMap[x, y].hasUp = true;
                                miniMap[x, y + 1].hasDown = true;
                            }
                        }
                        else if (y == mapsize - 1)
                        {
                            if (miniMap[x - 1, y] != null)
                            {
                                miniMap[x, y].hasLeft = true;
                                miniMap[x - 1, y].hasRight = true;
                            }
                            if (miniMap[x, y - 1] != null)
                            {
                                miniMap[x, y].hasDown = true;
                                miniMap[x, y - 1].hasUp = true;
                            }
                        }
                        else
                        {
                            if (miniMap[x - 1, y] != null)
                            {
                                miniMap[x, y].hasLeft = true;
                                miniMap[x - 1, y].hasRight = true;
                            }
                            if (miniMap[x, y + 1] != null)
                            {
                                miniMap[x, y].hasUp = true;
                                miniMap[x, y + 1].hasDown = true;
                            }
                            if (miniMap[x, y - 1] != null)
                            {
                                miniMap[x, y].hasDown = true;
                                miniMap[x, y - 1].hasUp = true;
                            }
                        }
                    }


                    else
                    {
                        if (y == 0)
                        {
                            if (miniMap[x - 1, y] != null)
                            {
                                miniMap[x, y].hasLeft = true;
                                miniMap[x - 1, y].hasRight = true;
                            }
                            if (miniMap[x + 1, y] != null)
                            {
                                miniMap[x, y].hasRight = true;
                                miniMap[x + 1, y].hasLeft = true;
                            }
                            if (miniMap[x, y + 1] != null)
                            {
                                miniMap[x, y].hasUp = true;
                                miniMap[x, y + 1].hasDown = true;
                            }
                        }
                        else if (y == mapsize-1)
                        {
                            if (miniMap[x - 1, y] != null)
                            {
                                miniMap[x, y].hasLeft = true;
                                miniMap[x - 1, y].hasRight = true;
                            }
                            if (miniMap[x + 1, y] != null)
                            {
                                miniMap[x, y].hasRight = true;
                                miniMap[x + 1, y].hasLeft = true;
                            }
                            if (miniMap[x, y - 1] != null)
                            {
                                miniMap[x, y].hasDown = true;
                                miniMap[x, y - 1].hasUp = true;
                            }
                        }
                        else
                        {
                            if (miniMap[x - 1, y] != null)
                            {
                                miniMap[x, y].hasLeft = true;
                                miniMap[x - 1, y].hasRight = true;
                            }
                            if (miniMap[x + 1, y] != null)
                            {
                                miniMap[x, y].hasRight = true;
                                miniMap[x + 1, y].hasLeft = true;
                            }
                            if (miniMap[x, y - 1] != null)
                            {
                                miniMap[x, y].hasDown = true;
                                miniMap[x, y - 1].hasUp = true;
                            }
                            if (miniMap[x, y + 1] != null)
                            {
                                miniMap[x, y].hasUp = true;
                                miniMap[x, y + 1].hasDown = true;
                            }
                        }
                    }
                }
            }
        }
    }


    //void CreateStep(MiniMap miniMap,GameObject[] steps, int maxsize)
    //{
    //    Point p = new Point();
    //    Point p_s = new Point();
    //    Point p_result = new Point();
    //    int result=0;
    //    int tmp=0;
    //    for (int i = 0; i < maxsize; i++)
    //    {
    //        for (int j = 0; j < maxsize; j++)
    //        {
    //            if (miniMap[i, j] != null)
    //            {
    //                if (miniMap[i, j].isSource == true)
    //                {
    //                    p_s.x = i;
    //                    p_s.y = j;
    //                }
    //            }
    //        }
    //    }

    //    for (int i = 0; i < maxsize; i++)
    //    {
    //        for (int j = 0; j < maxsize; j++)
    //        {
    //            if (miniMap[i, j] != null)
    //            {
    //                p.x = i;
    //                p.y = j;
    //                tmp = Math.Abs(p_s.x - p.x) + Math.Abs(p_s.y - p.y);
    //                if (result < tmp)
    //                {
    //                    result = tmp;
    //                    p_result.x = p_s.x;
    //                    p_result.y = p_s.y;
    //                }
    //            }
    //        }
    //    }

    //    miniMap[p_result.x, p_result.y].isStep = true;
    //    if (p_result.x == 0)
    //    {
    //        if (p_result.y == 0)
    //        {
    //            steps[0].SetActive(true);
    //        }
    //    }
    //}  
}
