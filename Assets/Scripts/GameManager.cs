using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;


namespace Announce {
    public enum EventTypes {
        teleport, idle, unlock, MrJ, discoverCrystal, buildBridge, getKey

    }
}

public class ConversationEventArgs : EventArgs {
    public Announce.EventTypes type;
    public string toSay;
    public float time;
}

public class GameManager : MonoBehaviour {

    //event handlers for messages
    public event EventHandler<EventArgs> onTeleport = (sender, args) => { }; //trigger when player teleports
    public event EventHandler<EventArgs> onIdle = (sender, args) => { }; //trigger after 30 seconds of idling
    public event EventHandler<EventArgs> onUnlockScene = (sender, args) => { }; //trigger after a scene is unlocked
    public event EventHandler<EventArgs> onMrJTrigger = (sender, args) => { }; //trigger after we interact with a Mr J trigger
    public event EventHandler<EventArgs> onDiscoverCrystal = (sender, args) => { }; //trigger after we interact with a Mr J trigger
    public event EventHandler<ConversationEventArgs> onConversation = (sender, args) => { }; //trigger for conversation events
    public event EventHandler<EventArgs> onGetKey = (sender, args) => { }; //trigger for conversation events




    public SceneTransition transitioner;
    public TimerManager timer;

    public float[] sceneTimes = { 30f, 20f };
    float[] actualSceneTimes = { 30f, 20f };

    public void DiscoverCrystal(GameObject sender) {
        onDiscoverCrystal.Invoke(sender, EventArgs.Empty);
    }
    public void Idle(GameObject sender) {
        onIdle.Invoke(sender, EventArgs.Empty);
    }
    public void EnterScence(GameObject sender) {
        onTeleport.Invoke(sender, EventArgs.Empty);
    }
    public void MrJEvent(GameObject sender) {
        onMrJTrigger.Invoke(sender, EventArgs.Empty);
    }

    public void ConversationEvent(GameObject sender, ConversTrriggerSO trigger) {

        if (trigger.eventType == Announce.EventTypes.buildBridge && !bridgeBuilt) {

            ConversationEventArgs args = new ConversationEventArgs();
            args.type = trigger.eventType;
            args.toSay = trigger.TextToSay;
            args.time = trigger.time;

            onConversation.Invoke(sender, args);
            bridgeBuilt = true;
        }
    }

    [Header("Game Progress Variables")]
    [SerializeField]
    private bool bridgeBuilt = false;
    [SerializeField]
    private bool hasKey = false;

    public bool BridgeBuilt { get { return bridgeBuilt; } private set { bridgeBuilt = value; } }
    public bool HasKey { get { return hasKey; } private set { hasKey = value; } }


    //we will have a single static GameManager instance
    public static GameManager instance = null;

    public KeyCode interactKey = KeyCode.Space;

    //scene information
    private int currentScene = 1;

    private int unlockedScenes = 1;

    private float transitionTime = 1.0f;

    //data stored about the player's teleportation
    private bool firstScene = true; //when false the player will override their position with LastPlayerPosition on Awake()
    private Vector3 lastPlayerPosition = new Vector3();

    //scene information
    public int CurrentScene { get { return currentScene; } private set { currentScene = value; } }




    //these are public methods of the above private variables that allow getting but not setting from outside
    public bool FirstScene { get { return firstScene; } private set { firstScene = value; } }
    public Vector3 LastPlayerPosition {  get { return lastPlayerPosition; } private set { lastPlayerPosition = value; } }
    public int UnlockedScene {  get { return unlockedScenes; } private set { unlockedScenes = value; } }

    private void Awake() {
        //this is a singleton pattern - it ensures that the GameManager only exists once
        //first if there is no game manager, then it must be us
        if (instance == null) {
            //set default scene times
            actualSceneTimes[0] = sceneTimes[0];
            actualSceneTimes[1] = sceneTimes[1];

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

        if (currentScene == 1) {
            timer.gameObject.SetActive(false);
        } else {
            timer.gameObject.SetActive(true);
            timer.Initialize(true, sceneTimes[currentScene-2], actualSceneTimes[currentScene - 2]);
        }

    }


    public void GetKey() {
        if (!hasKey) {
            onGetKey.Invoke(this.gameObject, EventArgs.Empty);
        }
        hasKey = true;
    }

    public void OhShit() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Teleporter>().enabled = false;
        player.GetComponent<CharacterMovement>().enabled = false;
        Teleport(new Vector3(-1, 1, -1), 1 - currentScene);
    }

    public void TheEnd() {
        SceneManager.LoadScene(4);
    }

    public void ForceBack() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Teleporter>().enabled = false;
        player.GetComponent<CharacterMovement>().enabled = false;
        Teleport(player.transform.position, -1);
    }

    public void Teleport(Vector3 position, int dir) {
        FirstScene = false;
        LastPlayerPosition = position;

        //update the scene we are in to the one we are loading
        currentScene += dir;

        //reset "actual time" based on where we are going back to
        if (dir == -1) {
            if (currentScene == 2) {
                actualSceneTimes[1] = sceneTimes[1];
            }
            if (currentScene == 1){
                actualSceneTimes[0] = sceneTimes[0];
                actualSceneTimes[1] = sceneTimes[1];
            }
        }
        //save "actual time" based on where we're going to
        if (dir == 1) {
            if (currentScene == 3) {
                actualSceneTimes[0] = timer.TimeRemaining;
            }
        }

        timer.gameObject.SetActive(false);

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
