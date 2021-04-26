using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumDoor : MonoBehaviour {

    public float time = 0.4f;
    public float angle = -165f;

    bool open = false;

    InteractiveObject io;

    private void Awake() {
        io = GetComponent<InteractiveObject>();
    }

    private void Update() {
        if (GameManager.instance.HasKey && io.enabled) {
            io.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (GameManager.instance.HasKey && !open) {
            open = true;

            IEnumerator coroutine = OpenDoor(time);
            StartCoroutine(coroutine);
        }
    }



    IEnumerator OpenDoor(float time) {

        float totalTime = time;

        float openSpeed = angle / time; //avg opening speed

        while (time >= 0) {
            time -= Time.deltaTime;

            transform.Rotate(Vector3.up, openSpeed * Time.deltaTime);

            yield return null;
        }

    }

}
