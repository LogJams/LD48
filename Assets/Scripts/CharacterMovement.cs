using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    CharacterController cc;

    public float speed = 5.0f;


    private void Awake() {
        cc = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start() {
        if (!GameManager.instance.FirstScene) {
            cc.enabled = false;
            transform.position = GameManager.instance.LastPlayerPosition;
            cc.enabled = true;
        }
    }

    // Update is called once per frame
    void Update() {

        Vector3 movement = new Vector3( Input.GetAxisRaw("Horizontal") / 2 + Input.GetAxisRaw("Vertical") / 2, 0, Input.GetAxisRaw("Vertical") / 2 - Input.GetAxisRaw("Horizontal") / 2);

        if (movement.sqrMagnitude > 0) {
            movement.Normalize();
        }

        cc.SimpleMove(movement * speed);





    }
}
