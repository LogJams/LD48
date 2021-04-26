using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinIt : MonoBehaviour {

    public Transform toSpin;
    public float speed = 5.0f;
    public Vector3 axis = new Vector3(1, 0, 0);


    private float timer = 0;

    public void Update() {

        toSpin.Rotate(axis, speed * Time.deltaTime);

    }


}
