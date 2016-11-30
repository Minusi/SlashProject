using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {

    MiniMapGenerator mg=null;

    int stage = 1;

    public void SetMiniMap()
    {
        //mg.show(mg.Generate(stage));
        stage++;
    }
    
}
