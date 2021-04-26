using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupButton : MonoBehaviour {

    public int index = 0;
    public InteractiveObject parent;

    public void OnClick() {
        parent.OnClick(index);
    }

}
