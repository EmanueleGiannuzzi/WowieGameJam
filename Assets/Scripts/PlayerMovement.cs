using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {
    public float runSpeed = 40f;

    Animator animator;
    CharacterController2D controller;

    float horizontalMove = 0f;
    bool isJumping = false;

    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Jump")) {
            isJumping = true;
        }
    }

    private void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }
}
