using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/AnnouncementScriptableObject", order = 1)]
public class AnnouncementsSO : ScriptableObject {


    public Announce.EventTypes eventType;

    public bool playOnce = false; //play only once or repeat with frequency
    public uint frequency = 1; //how often to trigger this event

    
    [Header("How the announcements are broadcasted")]
    public bool randomizeAnnouncements = false; // if true, go in sequence
    public bool playAll = false; //if true, play until # of announcements is 0

    public string[] announcements;

    public float time = 5f;


    public int Announce() {
        if (randomizeAnnouncements) {
            return Random.Range(0, announcements.Length);
        }


        return 0;
    }


}
