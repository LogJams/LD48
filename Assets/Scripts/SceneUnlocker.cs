using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUnlocker : MonoBehaviour {

    public int associatedScene;

    // Start is called before the first frame update
    void Start() {
        if (GameManager.instance.UnlockedScene >= associatedScene) {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GameManager.instance.UnlockScene(associatedScene);

            Destroy(this.gameObject);
        }
    }
}
