using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

using System;



public class Announcements : MonoBehaviour {

    GameObject player;

    GameObject popUpObj;
    Text popUpText;


    public List<AnnouncementsSO> announcements;

    string msg;

    bool popUp = false;


    private float timer = 0;


    private int teleportCounter = 0;
    private int idleCounter = 0;
    private int unlockCounter = 0;
    private int mrJCounter = 0;
    private int discoverCrystalCounter = 0;

    // Start is called before the first frame update
    void Start() {
        Initialize();

        GameManager.instance.onTeleport += OnPlayerTeleport;
        GameManager.instance.onIdle += OnPlayerIdle;
        GameManager.instance.onUnlockScene += OnUnlockScene;
        GameManager.instance.onMrJTrigger += OnMrJEvent;
        GameManager.instance.onDiscoverCrystal += OnDiscoverCrystal;

        popUpObj = CanvasManager.instance.popupManager.GetPopup();
        popUpText = popUpObj.GetComponentInChildren<Text>();
        popUpObj.SetActive(false);
    }


    void Initialize() {
        player = GameObject.FindGameObjectsWithTag("Player")[0];

    }

    void OnDiscoverCrystal(System.Object sender, EventArgs e) {
        discoverCrystalCounter++;
        AnnounceEvent(Announce.EventTypes.discoverCrystal, discoverCrystalCounter);
    }

    void OnMrJEvent(System.Object sender, EventArgs e) {
        mrJCounter++;
        AnnounceEvent(Announce.EventTypes.MrJ, mrJCounter);
    }


    void OnUnlockScene(System.Object sender, EventArgs e) {
        AnnounceEvent(Announce.EventTypes.unlock, unlockCounter);
        unlockCounter++;
    }

    void OnPlayerTeleport(System.Object sender, EventArgs e) {
        Initialize(); //only initialize when we enter a new scene to find the player
        teleportCounter++;
        AnnounceEvent(Announce.EventTypes.teleport, teleportCounter);
    }


    void OnPlayerIdle(System.Object sender, EventArgs e) {
        idleCounter++;
        AnnounceEvent(Announce.EventTypes.idle, idleCounter);
    }



    void AnnounceEvent(Announce.EventTypes eventType, int eventCounter) {
        int index = 0;

        for(int i = 0; i < announcements.Count; i++) {
            AnnouncementsSO aso = announcements[i];
            if (aso.eventType == eventType && eventCounter % aso.frequency == 0) {
                popUp = true;
                timer = aso.time;
                msg = aso.announcements[aso.Announce(eventCounter)];
                index = i;

                break;
            }
        }

        if (announcements[index].playOnce) {
            announcements.RemoveAt(index);
        }

    }

    void Update() {

        if (timer <= 0) {
            popUp = false;
            popUpObj.SetActive(false);
        }

        if (popUp) {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3(3f, 3f));
            popUpObj.SetActive(true);
            popUpObj.transform.position = screenPos;
            popUpText.text = msg;
            timer -= Time.deltaTime;

        }   
    }

}
