using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InteractiveObject : MonoBehaviour
{
    public string[] msg;
    GameObject popUpObj;
    Text popUpText; 
    bool popUp = false;
    Camera cam;

    int index = 0;

    // Start is called before the first frame update
    void Start() {
        popUpObj = CanvasManager.instance.popupManager.GetPopup();
        popUpObj.SetActive(false);

        cam = Camera.main;
    }

    // Update is called once per frame

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            popUp = true;
            index = Random.Range(0, msg.Length);
        }  
    }
    void OnTriggerExit(Collider col)
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
            Vector3 screenPos = cam.WorldToScreenPoint(transform.position + new Vector3(3f, 3f));
            popUpObj.SetActive(true);
            popUpObj.transform.position = screenPos;
            popUpText = popUpObj.GetComponentInChildren<Text>();
            popUpText.text = msg[ index ];


        }
        else
        {
            popUpObj.SetActive(false);
        }
            
    }


    private void OnDestroy() {
        GameObject.Destroy(popUpObj);
    }

}
