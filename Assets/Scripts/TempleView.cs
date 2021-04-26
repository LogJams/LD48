using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleView : MonoBehaviour {

    public float fadeTime = 1.0f;

    public GameObject toFade;

    List<Material> materials;
    List<Color> originalColors;

    private void Start() {

        materials = new List<Material>(toFade.GetComponent<MeshRenderer>().materials);
        originalColors = new List<Color>();

        foreach (Material mat in toFade.GetComponent<MeshRenderer>().materials) {
            originalColors.Add(mat.color);
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
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


        while (timer >= 0) {
            timer -= Time.deltaTime;
            for (int i = 0; i < materials.Count; i++) {
                if (visible) {
                    materials[i].color = Color.Lerp(originalColors[i], new Color(1, 1, 1, 0), timer / fadeTime);
                } else {
                    materials[i].color = Color.Lerp(new Color(1, 1, 1, 0), originalColors[i], timer / fadeTime);
                }
            }
            yield return null;
        }


        if (visible) {
            for (int i = 0; i < materials.Count; i++) {
                materials[i].color = originalColors[i];
            }
        }

    }

}
