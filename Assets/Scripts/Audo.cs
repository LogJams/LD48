using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audo : MonoBehaviour {

    public AudioClip firstStage;
    public AudioClip secondStage;
    public AudioClip thirdStage;

    public List<AudioClip> teleports;

    AudioSource src;

    // Start is called before the first frame update
    void Start() {

        src = GetComponent<AudioSource>();

        if (GameManager.instance.CurrentScene == 1) {
            src.clip = firstStage;
        }
        if (GameManager.instance.CurrentScene == 2) {
            src.clip = secondStage;
        }
        if (GameManager.instance.CurrentScene == 3) {
            src.clip = thirdStage;
        }

        src.PlayDelayed(1.0f);


        if (!GameManager.instance.FirstScene) {
            src.volume = 0.8f;
            src.PlayOneShot(teleports[Random.Range(0, teleports.Count)]);
            src.volume = 0.6f;
        }

    }

}
