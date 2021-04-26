using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomUnlocker : MonoBehaviour {

    // Start is called before the first frame update
    void Start() {
        if (GameManager.instance.HasKey) {
            this.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GameManager.instance.GetKey();
            this.enabled = false;
        }
    }
}
