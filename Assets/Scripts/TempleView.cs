using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleView : MonoBehaviour {

    public float fadeTime = 1.0f;

    public GameObject toFade;

    Material material;
    Color originalColor;

    private void Start() {
        material = toFade.GetComponent<MeshRenderer>().materials[0];
        originalColor = material.color;
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Detected player entering");

            IEnumerator coroutine = FadeTransparency(false);
            StartCoroutine(coroutine);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            IEnumerator coroutine = FadeTransparency(true);
            StartCoroutine(coroutine);
        }
    }


    private IEnumerator FadeTransparency(bool visible) {

        float timer = fadeTime;
        toFade.SetActive(true);


        while (timer >= 0) {
            timer -= Time.deltaTime;
            if (visible) {
                material.color = Color.Lerp(originalColor, new Color(1, 1, 1, 0), timer / fadeTime);
            } else {
                material.color = Color.Lerp(new Color(1, 1, 1, 0), originalColor, timer / fadeTime);
            }
            yield return null;
        }


        if (visible) {
            material.color = originalColor;
        } else {
            toFade.SetActive(false);
        }

    }

}
