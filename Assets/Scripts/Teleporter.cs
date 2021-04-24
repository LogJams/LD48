using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour {

    [Header("Teleportation Variables")]   
    public KeyCode teleportForward = KeyCode.X;
    public KeyCode teleportBackward = KeyCode.Z;

    public float holdTime = 3.0f;


    //time tracks how long we have left to wait
    float timer = 1; //default to something > 0
    //dir tracks if we're going forward or backward (+1 or -1)
    int dir = 0;
    //teleporting flag ensure we're only going to teleport once
    bool teleporting = false;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(teleportForward) || Input.GetKeyDown(teleportBackward)) {
            timer = holdTime;

            dir = 0;
            if (Input.GetKeyDown(teleportForward )) { dir++; }
            if (Input.GetKeyDown(teleportBackward)) { dir--; }
        }

        if ( (dir == 1 && Input.GetKey(teleportForward)) || (dir == -1 && Input.GetKey(teleportBackward))) {
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && !teleporting) {
            teleporting = true;
            StartCoroutine(TeleportCoroutine());
        }
    }


    //asynchronous coroutine that is called to load the next scene when the player teleports
    IEnumerator TeleportCoroutine() {

        AsyncOperation asyncLoad;

        if (dir == 1) {
            asyncLoad = SceneManager.LoadSceneAsync(1);
        }
        else{
            asyncLoad = SceneManager.LoadSceneAsync(0);
        }

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }


}
