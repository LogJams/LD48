using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //we will have a single static GameManager instance
    public static GameManager instance = null;

    //scene information
    private int currentScene = 1;
    private uint sceneCount = 3;

    //data stored about the player's teleportation
    private bool firstScene = true; //when false the player will override their position with LastPlayerPosition on Awake()
    private Vector3 lastPlayerPosition = new Vector3();


    //these are public methods of the above private variables that allow getting but not setting from outside
    public bool FirstScene { get { return firstScene; } private set { firstScene = value; } }
    public Vector3 LastPlayerPosition {  get { return lastPlayerPosition; } private set { lastPlayerPosition = value; } }


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



    public void Teleport(Vector3 position, int dir) {
        FirstScene = false;
        LastPlayerPosition = position;

        currentScene += dir;

        StartCoroutine(TeleportCoroutine(currentScene));
    }


    public bool CanTeleport(int dir) {
        //ensure we're moving to a valid scene
        //we teleport between scenes 1, 2, and 3 (0 is the title scene)
        return (currentScene + dir <= sceneCount) && (currentScene + dir >= 1);
    }


    //load the scene at index (vector of scenes loaded in build settings)
    IEnumerator TeleportCoroutine(int idx) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(idx);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }



}
