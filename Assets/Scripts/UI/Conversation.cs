using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation : MonoBehaviour {

    private int counter = 0;

    public Image image;
    public Text text;

    private void Awake() {
        image.enabled = false;
        text.enabled = false;
    }

    public void Add() {
        if (counter == 0) {
            image.enabled = true;
            text.enabled = true;
        }
        counter++;
    }

    public void Remove() {
        counter--;
        if (counter == 0) {
            image.enabled = false;
            text.enabled = false;
        }
    }

}
