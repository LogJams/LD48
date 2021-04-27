using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {


    public float bias = 2.5f;

    Transform player;
    Vector3 boom = new Vector3(-26.1f, 35.8f, -24f);

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = player.position + boom;
    }

    // Update is called once per frame
    void LateUpdate() {

//        transform.position = Vector3.Lerp(transform.position, player.position + boom, Time.deltaTime * bias);
        transform.position = player.position + boom;


        
    }
}
