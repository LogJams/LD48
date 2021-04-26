using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InteractiveObject : MonoBehaviour
{
    [Header("Interaction Variables")]
    public KeyCode interact = KeyCode.Space;

    public string[] msg;
    public string[] optionsMsgs; 
    GameObject popUpObj;
   
    Text popUpText;

    public bool isPopUpButton;
    int numberOfButtons;
    GameObject[] popUpButtons ; //TODO: We should get it from Canvas Manager
    bool popUp = false;
    

    Camera cam;

    int index = 0;

    // Start is called before the first frame update
    void Start() {
        numberOfButtons = optionsMsgs.Length;
        popUpObj = CanvasManager.instance.popupManager.GetPopup();
        popUpObj.SetActive(false);
        cam = Camera.main; 
        CreateButtons();
    }

    void CreateButtons()
    {
       popUpButtons = CanvasManager.instance.popupManager.GetPopupButtons(numberOfButtons);
        SetButtons(false);
    }
    void SetButtons(bool status)
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            popUpButtons[i].SetActive(status);
            popUpButtons[i].GetComponentInChildren<Text>().text =  optionsMsgs[i];
        }
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

            // If popUpButtons
            if (Input.GetKeyDown(interact) && isPopUpButton)
            {
                SetButtons(true);
            }

        }
        else
        {
            popUpObj.SetActive(false);
            SetButtons(false);

        }
            
    }

    private void OnDestroy() {
        GameObject.Destroy(popUpObj);

    }

}
