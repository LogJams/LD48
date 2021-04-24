using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("the interactive script is running!!");
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            print("Some thing entered");
        }
        
    }
}
