using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {
    // Start is called before the first frame update
    public Color empty;
    float timeRemained;
    float totalTime;

    Scrollbar scrolObj;


    public float TimeRemaining { get { return timeRemained; } private set { timeRemained = value; } }


    bool running = false;

    private void Awake() {
        scrolObj = this.GetComponent<Scrollbar>();
    }

    public void Initialize(bool runTimer, float _totalTime, float _timeLeft) {
        totalTime = _totalTime;
        timeRemained = _timeLeft;
        running = runTimer;

        ColorBlock cb = scrolObj.colors;
        cb.normalColor = Color.green;
        scrolObj.colors = cb;
    }

    // Update is called once per frame
    void Update() {
        if (!running) return;

        timeRemained -= Time.deltaTime;


        if (timeRemained >= 0)
        {
           scrolObj.size = (float)timeRemained / totalTime;
        }
        else
        {
            ColorBlock cb = scrolObj.colors;
            cb.normalColor = Color.black;
            scrolObj.colors = cb;
            GameManager.instance.ForceBack();
        }

    }
}
