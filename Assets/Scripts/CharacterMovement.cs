using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    CharacterController cc;

    public float speed = 5.0f;

    public Animator anim;

    public float idleTime = 30f; // we are idle if no input for 30s;

    float timer = 0;

    Vector3 lookPos = new Vector3(1, 0, 0);

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


        //look towards mouse when moving
        RaycastHit hitInfo;
        int layer_mask = LayerMask.GetMask("Terain");

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, layer_mask)) {
            lookPos = hitInfo.point;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
        }

        if (movement.sqrMagnitude > 0) {



            movement.Normalize();


            //forward/backward motion
            float forwardSpeed = Vector3.Dot(movement, (lookPos - transform.position).normalized );
            //left right motion
            float strafeSpeed = Vector3.Dot(movement, (lookPos - transform.position).normalized);

            timer = idleTime;
            anim.SetFloat("X Speed", strafeSpeed); // x speed
            anim.SetFloat("Z Speed", forwardSpeed); // y speed
            anim.SetFloat("Total Speed", 1.0f); //  total speed
        }

        else {
            anim.SetFloat("X Speed", 0); // x speed
            anim.SetFloat("Z Speed", 0); // y speed
            anim.SetFloat("Total Speed", 0); //  total speed
        }

        cc.SimpleMove(movement * speed);

        if (timer <= 0) {
            GameManager.instance.Idle(this.gameObject);
            timer = idleTime;
        }


    }
}
