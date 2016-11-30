using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class FileManager : MonoBehaviour {
    
    string m_strPath = "Assets/Resources/";
    public Text nametxt;
    public Textfield namefield; 
    int s_fscore;

    public void Awake()
    {
        namefield = new Textfield();
    }

    public void GameClear()
    {
        s_fscore = (int)GameManager.fscore;
        // GameClear_name(nametxt.text);
        // GameClear_score(s_fscore);
        //  DataLoad_name();                    DataLoad_name()+" : "+
        nametxt.text = s_fscore.ToString();  // 텍스트에 표시
    }

    public void SaveScore()
    {
        GameClear_name(namefield.Textfieldstring);
        GameClear_score(s_fscore);
        SceneManager.LoadScene("Main Scene");
    }

    //void OnGUI()
    //{
    //    name_textfield = GUI.TextField(new Rect(450, 200, 150, 30), name_textfield, 10);
    //}

    public void GameClear_name(string strData)
    {
        FileStream f = new FileStream(m_strPath + "Data_name.txt", FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        writer.WriteLine(strData);
        writer.Close();
    }

    public void GameClear_score(int strData)
    {
        FileStream f = new FileStream(m_strPath + "Data_score.txt", FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        writer.WriteLine(strData);
        writer.Close();
    }

    public string DataLoad_name()
    {
        TextAsset data = Resources.Load("Data_name", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        // 먼저 한줄을 읽는다. 

        string source = sr.ReadLine();
        string[] values;                // 쉼표로 구분된 데이터들을 저장할 배열 (values[0]이면 첫번째 데이터 )

        
            values = source.Split(',');  // 쉼표로 구분한다. 저장시에 쉼표로 구분하여 저장하였다.
            if (values.Length == 0)
            {
                sr.Close();
            }
            source = sr.ReadLine();   // 한줄 읽는다.
            return source;
        
    }

    public string DataLoad_score(int count)
    {
        TextAsset data = Resources.Load("Data_score", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        // 먼저 한줄을 읽는다. 

        string source = sr.ReadLine();
        string[] values;                // 쉼표로 구분된 데이터들을 저장할 배열 (values[0]이면 첫번째 데이터 )

       
        while (count>0)
        {
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

   
    int FindScore(string name)
    {
        TextAsset data = Resources.Load("Data_name", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);

        // 먼저 한줄을 읽는다. 
        int i=0;   
        string source = sr.ReadLine();
        string[] values;                // 쉼표로 구분된 데이터들을 저장할 배열 (values[0]이면 첫번째 데이터 )


        while (true)
        {
            values = source.Split(',');  // 쉼표로 구분한다. 저장시에 쉼표로 구분하여 저장하였다.
            if (values.Length == 0)
            {
                sr.Close();
            }
            source = sr.ReadLine();   // 한줄 읽는다.
            i++;
            if(name==source)
                return i;
        }
    }





}