using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;


namespace Announce {
    public enum EventTypes {
        teleport, idle, unlock

    }
}


public class GameManager : MonoBehaviour {

    //event handlers for messages
    public event EventHandler<EventArgs> onTeleport = (sender, args) => { }; //trigger when player teleports
    public event EventHandler<EventArgs> onIdle = (sender, args) => { }; //trigger after 30 seconds of idling
    public event EventHandler<EventArgs> onUnlockScene = (sender, args) => { }; //trigger after a scene is unlocked


    public SceneTransition transitioner;


    public void Idle(GameObject sender) {
        onIdle.Invoke(sender, EventArgs.Empty);
    }
    public void EnterScence(GameObject sender) {
        onTeleport.Invoke(sender, EventArgs.Empty);
    }


    //we will have a single static GameManager instance
    public static GameManager instance = null;

    //scene information
    private int currentScene = 1;
    private uint sceneCount = 3;

    private int unlockedScenes = 1;

    private float transitionTime = 1.0f;

    //data stored about the player's teleportation
    private bool firstScene = true; //when false the player will override their position with LastPlayerPosition on Awake()
    private Vector3 lastPlayerPosition = new Vector3();


    //these are public methods of the above private variables that allow getting but not setting from outside
    public bool FirstScene { get { return firstScene; } private set { firstScene = value; } }
    public Vector3 LastPlayerPosition {  get { return lastPlayerPosition; } private set { lastPlayerPosition = value; } }
    public int UnlockedScene {  get { return unlockedScenes; } private set { unlockedScenes = value; } }

    private void Awake() {
        //this is a singleton pattern - it ensures that the GameManager only exists once
        //first if there is no game manager, then it must be us
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            //listen for scene loads so we can fade in
            SceneManager.sceneLoaded += OnSceneLoad;
        }
        //if we are not the game manager then it must be someone else
        if (instance != this) {
            Destroy(this.gameObject);
        }
    }


    void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        transitioner.TransitionIn(transitionTime);
    }


    public void Teleport(Vector3 position, int dir) {
        FirstScene = false;
        LastPlayerPosition = position;

        //update the scene we are in to the one we are loading
        currentScene += dir;

        transitioner.TransitionOut(transitionTime);
        StartCoroutine(TeleportCoroutine(currentScene, transitionTime));
    }

    public void UnlockScene(int scene) {
        if (UnlockedScene + 1 == scene) {
            UnlockedScene++;
            onUnlockScene.Invoke(this.gameObject, EventArgs.Empty);
        }
    }


    public bool CanTeleport(int dir) {
        //ensure we're moving to a valid scene
        //we teleport between scenes 1, 2, and 3 (0 is the title scene)
        return (currentScene + dir <= unlockedScenes) && (currentScene + dir >= 1);
    }


    //load the scene at index (vector of scenes loaded in build settings)
    IEnumerator TeleportCoroutine(int idx, float sceneTime) {

        //unity actually loads too fast, so we have to wait here. This may change
        yield return new WaitForSeconds(sceneTime);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(idx);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }



}
