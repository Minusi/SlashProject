using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.IO;

public class RankingTab : MonoBehaviour
{
    public GameObject RankingOb;
    public Text RankingText_name;
    public Text RankingText_score;
    public Text[] Names;
    public Text[] Scores;
    int[] intArray = new int[100];

    void Awake()
    {
        for(int i = 1; i < 101; i++)
        {
            if (DataLoad_score(i) != null){
                intArray[i - 1] = int.Parse(DataLoad_score(i)) ;
            }
        }
    }

    public void DisplayRanking()
    {
        RankingOb.SetActive(true);
       // RankingText.text = GameManager.fscore.ToString();

    }

    public void noDisplayRanking()
    {
        RankingOb.SetActive(false);
    }

    public void sort()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = i + 1; j < 100; j++)
            {
                if (intArray[i] < intArray[j])
                {
                    int temp = intArray[i];
                    intArray[i] = intArray[j];
                    intArray[j] = temp;
                }
            }
        }


        Names[0].text = DataLoad_name(FindName(intArray[0]));
        Names[1].text = DataLoad_name(FindName(intArray[1]));
        Names[2].text = DataLoad_name(FindName(intArray[2]));
        Scores[0].text = intArray[0].ToString();
        Scores[1].text = intArray[1].ToString();
        Scores[2].text = intArray[2].ToString();
        RankingText_name.text = DataLoad_name();
        RankingText_score.text = DataLoad_score();
    }


    public string DataLoad_name(int count)
    {
        TextAsset data = Resources.Load("Data_name", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        // 먼저 한줄을 읽는다. 

        string source = sr.ReadLine();
        string[] values;                // 쉼표로 구분된 데이터들을 저장할 배열 (values[0]이면 첫번째 데이터 )


        while (count > 0)
        {
            if (source == null)
            {
                break;
            }
            values = source.Split(',');  // 쉼표로 구분한다. 저장시에 쉼표로 구분하여 저장하였다.
            if (values.Length == 0)
            {
                sr.Close();
            }
            source = sr.ReadLine();   // 한줄 읽는다.
            count--;
        }
        return source;

    }
    public string DataLoad_name()
    {
        TextAsset data = Resources.Load("Data_name", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        // 먼저 한줄을 읽는다. 

        string source = sr.ReadLine();
        string[] values;                // 쉼표로 구분된 데이터들을 저장할 배열 (values[0]이면 첫번째 데이터 )
        string a=null;
        values = source.Split(',');  // 쉼표로 구분한다. 저장시에 쉼표로 구분하여 저장하였다.
        if (values.Length == 0)
        {
            sr.Close();
        }
        while (true) { 
        source = sr.ReadLine();   // 한줄 읽는다.
        if (source == null) return a;
        a = source;
        }
    }


    public string DataLoad_score(int count)
    {
        TextAsset data = Resources.Load("Data_score", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        // 먼저 한줄을 읽는다. 

        string source = sr.ReadLine();
        string[] values;                // 쉼표로 구분된 데이터들을 저장할 배열 (values[0]이면 첫번째 데이터 )


        while (count > 0)
        {
            if (source == null)
            {
                break;
            }
            values = source.Split(',');  // 쉼표로 구분한다. 저장시에 쉼표로 구분하여 저장하였다.
            if (values.Length == 0)
            {
                sr.Close();
            }
            source = sr.ReadLine();   // 한줄 읽는다.
            count--;
        }
        return source;

    }
    public string DataLoad_score()
    {
        TextAsset data = Resources.Load("Data_score", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        // 먼저 한줄을 읽는다. 
        

        string source = sr.ReadLine();
        string[] values;                // 쉼표로 구분된 데이터들을 저장할 배열 (values[0]이면 첫번째 데이터 )
        string a = null;
        values = source.Split(',');  // 쉼표로 구분한다. 저장시에 쉼표로 구분하여 저장하였다.
        if (values.Length == 0)
        {
            sr.Close();
        }
        while (true)
        {
            source = sr.ReadLine();   // 한줄 읽는다.
            if (source == null) return a;
            a = source;
        }

    }

    int FindName(int score)
    {
        TextAsset data = Resources.Load("Data_score", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        // 먼저 한줄을 읽는다. 
        int i = 0;
        string source = sr.ReadLine();
        string[] values;                // 쉼표로 구분된 데이터들을 저장할 배열 (values[0]이면 첫번째 데이터 )


        while (true)
        {
            if (source == null) break;
            values = source.Split(',');  // 쉼표로 구분한다. 저장시에 쉼표로 구분하여 저장하였다.
            if (values.Length == 0)
            {
                sr.Close();
            }
            source = sr.ReadLine();   // 한줄 읽는다.
            i++;
            if (score.ToString() == source)
                return i;
        }
        return i;
    }

}
