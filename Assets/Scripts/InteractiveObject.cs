using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class InteractiveObject : MonoBehaviour
{
    public string[] msg; 
    public Text popUpObj; 
    bool popUp = false;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        print("the interactive script is running!!");
        cam = Camera.main;
    }

    // Update is called once per frame

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            popUp = true;
        }  
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            popUp = false; 

        }
    }
    void Update()
    {
        if (popUp)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
            popUpObj.text = msg[0]; //for now 
            popUpObj.transform.position = screenPos;
        }
        else
        {
            popUpObj.text = "";
        }
            
    }

}
