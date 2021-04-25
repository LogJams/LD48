using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float totalTime = 30;
    public Color empty;
    float timeRemained; 
    void Start()
    {
        timeRemained = totalTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeRemained -= Time.deltaTime;
        Scrollbar scrolObj = this.GetComponent<Scrollbar>();


        if (timeRemained >= 0)
        {
           scrolObj.size = (float)timeRemained / totalTime;
        }
        else
        {
            ColorBlock cb = scrolObj.colors;
            cb.normalColor = Color.black;
            scrolObj.colors = cb;
        }

    }
}
