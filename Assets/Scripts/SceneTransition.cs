using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour {

    public Image blackScreen;

    private void Awake() {
        blackScreen.enabled = true;
    }

    public void TransitionIn(float time) {
        blackScreen.CrossFadeAlpha(1, 0, false);
        blackScreen.enabled = true;
        blackScreen.CrossFadeAlpha(0, time, false);
        StartCoroutine("Wait", time);
    }

    public void TransitionOut(float time) {
        blackScreen.enabled = true;
        blackScreen.CrossFadeAlpha(1, time, false);
    }


    IEnumerable Wait(float time) {
        yield return new WaitForSeconds(time);
        blackScreen.enabled = false;
    }

}
