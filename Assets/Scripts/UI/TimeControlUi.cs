using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

using UnityEngine.SceneManagement;
public class TimeControlUi : MonoBehaviour {

    public Image backIcon;
    public GameObject backwardText;

    public Image forwardIcon;
    public GameObject forwardText;


    //called only when the first level is loaded for the first time
    private void Awake() {
        //set up our on level load callback
        SceneManager.sceneLoaded += this.OnLevelLoad;

        //configure scene 1
        backIcon.color = new Color(1, 1, 1, 0);

        GameManager.instance.onUnlockScene += OnCrystalPickup;

    }


    void OnCrystalPickup(System.Object sender, EventArgs args) {
        if (GameManager.instance.CurrentScene == 1) {
            //hide the back icon
            backIcon.color = new Color(1, 1, 1, 0);
            backwardText.SetActive(false);

            //show the front icon if it's unlocked
            if (GameManager.instance.UnlockedScene >= 2) {
                forwardIcon.color = new Color(1, 1, 1, 1);
                forwardText.SetActive(true);
            } else {
                forwardIcon.color = new Color(1, 1, 1, 0);
                forwardText.SetActive(false);
            }
        }
    }

    //our callback for updating the ui
    private void OnLevelLoad(Scene scene, LoadSceneMode sceneMode) {

        if (GameManager.instance.CurrentScene == 1) {
            //hide the back icon
            backIcon.color = new Color(1, 1, 1, 0);
            backwardText.SetActive(false);

            //show the front icon if it's unlocked
            if (GameManager.instance.UnlockedScene >= 2) {
                forwardIcon.color = new Color(1, 1, 1, 1);
                forwardText.SetActive(true);
            } else {
                forwardIcon.color = new Color(1, 1, 1, 0);
                forwardText.SetActive(false);
            }
        }

        if (GameManager.instance.CurrentScene == 2) {
            backIcon.color = new Color(1, 1, 1, 1);
            backwardText.SetActive(true);
            //show the front icon if it's unlocked
            if (GameManager.instance.UnlockedScene >= 3) {
                forwardIcon.color = new Color(1, 1, 1, 1);
                forwardText.SetActive(true);
            } else {
                forwardIcon.color = new Color(1, 1, 1, 0);
                forwardText.SetActive(false);
            }
        }

        if (GameManager.instance.CurrentScene == 3) {
            backIcon.color = new Color(1, 1, 1, 1);
            backwardText.SetActive(true);
            forwardIcon.color = new Color(1, 1, 1, 0);
            forwardText.SetActive(false);
        }

    }


}
