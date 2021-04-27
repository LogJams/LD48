using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour
{
    [Header("Interaction Variables")]
    public string[] msg;
    public string[] optionsMsgs;
    public string[] responseMsgs;


    public List<ConversTrriggerSO> triggers;

    string messageToUse;

    GameObject popUpObj;
   
    Text popUpText;

    public bool isInteractive;
    int numberOfButtons;
    GameObject[] popUpButtons ; //TODO: We should get it from Canvas Manager

    //activated is used to determine if we're in the zone or not
    bool activated = false;
    bool popUp = false; //popUp is changed when we hit the interact key
    bool showButtons = false; //this is whether we want to show or hide the buttons

    bool doingResponse = false;

    bool waiting = false; //waiting for timer

    Camera cam;

    // Start is called before the first frame update
    void Start() {
        numberOfButtons = optionsMsgs.Length;
        popUpObj = CanvasManager.instance.popupManager.GetPopup();
        popUpObj.SetActive(false);
        cam = Camera.main; 
        CreateButtons();
    }

    void CreateButtons() {
        popUpButtons = CanvasManager.instance.popupManager.GetPopupButtons(popUpObj.transform, numberOfButtons);
        for (int i = 0; i < numberOfButtons; i++) {
            popUpButtons[i].GetComponentInChildren<PopupButton>().index = i;
            popUpButtons[i].GetComponentInChildren<PopupButton>().parent = this;
        }
        SetButtons(false);
        //check if the button is an event that happened, so we should disable it
        //we can send a button message too
        foreach (ConversTrriggerSO trig in triggers) {
            if (GameManager.instance.BridgeBuilt && trig.eventType == Announce.EventTypes.buildBridge) {
                popUpButtons[trig.OptionIndex].GetComponent<Button>().interactable = false;
            }
        }


    }

    void SetButtons(bool status)
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            popUpButtons[i].SetActive(status);
            popUpButtons[i].GetComponentInChildren<Text>().text =  optionsMsgs[i];
        }
    }


    public void OnClick(int buttonIndex) {
        //do something here with the button click
        showButtons = false;
        if (responseMsgs.Length > buttonIndex && !doingResponse) {
            //update the text
            doingResponse = true;
            messageToUse = responseMsgs[buttonIndex];
        } else {
            Reset();
        }

        //we can send a button message too
        foreach (ConversTrriggerSO trig in triggers) {
            if (trig.OptionIndex == buttonIndex) {
                GameManager.instance.ConversationEvent(this.gameObject, trig);
                popUpButtons[buttonIndex].GetComponent<Button>().interactable = false;

                //we will make the person wait before responding by deactivating and reactivating
                waiting = true;
                SetButtons(false);
                popUpObj.SetActive(false);
                IEnumerator waitCoroutine = WaitTime(trig.time - 0.2f);
                StartCoroutine(waitCoroutine);
            }
        }
    }

    IEnumerator WaitTime(float sec) {
        yield return new WaitForSeconds(sec);
        if (activated) {
            SetButtons(true);
            popUpObj.SetActive(true);
        }
        waiting = false;
    }


    private void Reset() {
        //reset the cat to the beginning
        activated = false;
        popUp = false;
        doingResponse = false;
        SetButtons(false);
        popUpObj.SetActive(false);
    }



    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player") {
            activated = true;
            messageToUse = msg[Random.Range(0, msg.Length)];
            showButtons = true;
            //pop up right away if we're not interactive
            popUp = !isInteractive;
            if (isInteractive) {
                CanvasManager.instance.talkIndicator.Add();
            }
        }

    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Reset();
            if (isInteractive) {
                CanvasManager.instance.talkIndicator.Remove();
            }
        }
    }
    void Update() {
        if (!activated || waiting) { return; }

            //check if we haven't popped up yet
            if (!popUp) {
                //play some particle effect to show we can interact with the object
                if (Input.GetKeyDown(GameManager.instance.interactKey) && isInteractive) {
                    popUp = true;
                    showButtons = true;
                }
            }

            //check if we have popped up, if so we display everything
            if (popUp) {
                //set the screen position and activate
                Vector3 screenPos = cam.WorldToScreenPoint(transform.position + new Vector3(0f, 3f, 0f));
                popUpObj.SetActive(true);
                SetButtons(showButtons);
                popUpObj.transform.position = screenPos;
                //set the text
                popUpText = popUpObj.GetComponentInChildren<Text>();
                popUpText.text = messageToUse;
            }

            // If popUpButtons
            //if (Input.GetKeyDown(interact) && isPopUpButton)  {
            //    SetButtons(true);
            //}
            
    }

    private void OnDestroy() {
        GameObject.Destroy(popUpObj);

    }

}
