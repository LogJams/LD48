using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class TimeControlUi : MonoBehaviour {

    public Image backIcon;
    public Image forwardIcon;


    //called only when the first level is loaded for the first time
    private void Awake() {
        //set up our on level load callback
        SceneManager.sceneLoaded += this.OnLevelLoad;

        //configure scene 1
        backIcon.color = new Color(1, 1, 1, 0);

    }


    //our callback for updating the ui
    private void OnLevelLoad(Scene scene, LoadSceneMode sceneMode) {

        if (GameManager.instance.CurrentScene == 1) {
            //hide the back icon
            backIcon.color = new Color(1, 1, 1, 0);

            //show the front icon if it's unlocked
            if (GameManager.instance.UnlockedScene >= 2) {
                forwardIcon.color = new Color(1, 1, 1, 1);
            } else {
                forwardIcon.color = new Color(1, 1, 1, 0);
            }
        }

        if (GameManager.instance.CurrentScene == 2) {
            backIcon.color = new Color(1, 1, 1, 1);
            //show the front icon if it's unlocked
            if (GameManager.instance.UnlockedScene >= 3) {
                forwardIcon.color = new Color(1, 1, 1, 1);
            } else {
                forwardIcon.color = new Color(1, 1, 1, 0);
            }
        }

        if (GameManager.instance.CurrentScene == 3) {
            backIcon.color = new Color(1, 1, 1, 1);
            forwardIcon.color = new Color(1, 1, 1, 0);
        }

    }


}
