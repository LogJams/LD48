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


    private uint teleportCounter = 0;
    private uint idleCounter = 0;


    // Start is called before the first frame update
    void Start() {
        Initialize();

        GameManager.instance.onTeleport += OnPlayerTeleport;
        GameManager.instance.onIdle += OnPlayerIdle;


        popUpObj = CanvasManager.instance.popupManager.GetPopup();
        popUpText = popUpObj.GetComponentInChildren<Text>();
        popUpObj.SetActive(false);
    }


    void Initialize() {
        player = GameObject.FindGameObjectsWithTag("Player")[0];

    }


    void OnPlayerTeleport(System.Object sender, EventArgs e) {
        Initialize(); //only initialize when we enter a new scene to find the player
        AnnounceEvent(Announce.EventTypes.teleport);
    }


    void OnPlayerIdle(System.Object sender, EventArgs e) {
        AnnounceEvent(Announce.EventTypes.idle);
    }



    void AnnounceEvent(Announce.EventTypes eventType) {

        teleportCounter++;

        int index = 0;

        for(int i = 0; i < announcements.Count; i++) {
            AnnouncementsSO aso = announcements[i];
            if (aso.eventType == eventType && teleportCounter % aso.frequency == 0) {
                popUp = true;
                timer = aso.time;
                msg = aso.announcements[aso.Announce()];
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
