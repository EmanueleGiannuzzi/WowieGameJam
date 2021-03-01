using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(Animator))]
public class GhostMovement : MonoBehaviour {
    public float runSpeed = 40f;

    CharacterController2D controller;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update() {
        if(PlayerManager.Instance.CanMove) {
            horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
            verticalMove = Input.GetAxis("Vertical") * runSpeed;
        }
        else {
            horizontalMove = 0f;
            verticalMove = 0f;
        }
    }

    private void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);
    }
}
