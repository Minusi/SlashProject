using UnityEngine;
using System.Collections;

public class Textfield : FileManager
{
    
    
    public string Textfieldstring = "Name";

    void OnGUI()
    {
        Textfieldstring = GUI.TextField(new Rect(460, 150, 100, 40), Textfieldstring,10);
        
    }
}
