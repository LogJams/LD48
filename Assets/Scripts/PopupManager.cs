using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
//using System.Numerics;
using UnityEngine;

public class PopupManager : MonoBehaviour {

    public GameObject popupPanel;
    public GameObject popUpButtons;


    public GameObject GetPopup() {
        return Instantiate(popupPanel, this.transform);
    }
    public GameObject[] GetPopupButtons(Transform parent, int number)
    {
        GameObject[] popUpButtonsArray = new GameObject[number];
        for(int i=0; i<number; i++)
        {
            popUpButtonsArray[i] = Instantiate(popUpButtons, this.transform.position + Vector3.down*30*i, new Quaternion(), parent);
        }
        
        return popUpButtonsArray;
    }

}
