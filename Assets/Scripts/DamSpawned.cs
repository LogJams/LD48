using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamSpawned : MonoBehaviour {

    [Header("Disappear when the dam is built.")]
    public bool inverse = false;

    // Start is called before the first frame update
    void Start() {
        if ( (GameManager.instance.BridgeBuilt && inverse) || (!GameManager.instance.BridgeBuilt && !inverse)) {
            Destroy(this.gameObject);
        }   
    }
}
