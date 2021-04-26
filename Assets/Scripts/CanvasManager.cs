using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

    public static CanvasManager instance;

    public PopupManager popupManager;
    public Conversation talkIndicator;

    private void Awake() {
        //this is a singleton pattern - it ensures that the GameManager only exists once
        //first if there is no game manager, then it must be us
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        //if we are not the game manager then it must be someone else
        if (instance != this) {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
