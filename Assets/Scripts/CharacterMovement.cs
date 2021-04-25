using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    CharacterController cc;

    public float speed = 5.0f;


    public float idleTime = 30f; // we are idle if no input for 30s;

    float timer = 0;

    private void Awake() {
        cc = GetComponent<CharacterController>();
        timer = idleTime;
    }

    // Start is called before the first frame update
    void Start() {

        if (!GameManager.instance.FirstScene) {
            cc.enabled = false;
            transform.position = GameManager.instance.LastPlayerPosition;
            cc.enabled = true;

            GameManager.instance.EnterScence(this.gameObject);

        }
    }

    // Update is called once per frame
    void Update() {

        timer -= Time.deltaTime;

        Vector3 movement = new Vector3( Input.GetAxisRaw("Horizontal") / 2 + Input.GetAxisRaw("Vertical") / 2, 0, Input.GetAxisRaw("Vertical") / 2 - Input.GetAxisRaw("Horizontal") / 2);

        if (movement.sqrMagnitude > 0) {
            timer = idleTime;
            movement.Normalize();
        }

        cc.SimpleMove(movement * speed);

        if (timer <= 0) {
            GameManager.instance.Idle(this.gameObject);
            timer = idleTime;
        }


    }
}
