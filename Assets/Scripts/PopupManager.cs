using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour {

    public GameObject popupPanel;


    public GameObject GetPopup() {
        return Instantiate(popupPanel, this.transform);
    }

}
