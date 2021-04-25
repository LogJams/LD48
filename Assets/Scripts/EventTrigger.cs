using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour {

    public Announce.EventTypes type;

    public bool oneShot = false;


    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {

            switch (type) {
                default:
                case Announce.EventTypes.idle:
                case Announce.EventTypes.teleport:
                case Announce.EventTypes.unlock:
                    //don't do anything
                    break;
                case Announce.EventTypes.MrJ:
                    GameManager.instance.MrJEvent(this.gameObject);
                    break;
                case Announce.EventTypes.discoverCrystal:
                    GameManager.instance.DiscoverCrystal(this.gameObject);
                    break;


            }

            if (oneShot) {
                Destroy(this);
            }

        }
    }

}
