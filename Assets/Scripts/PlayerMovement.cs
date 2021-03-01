using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour {
    public float runSpeed = 40f;

    Animator animator;
    CharacterController2D controller;
    AudioSource audioSource;

    float horizontalMove = 0f;
    bool isJumping = false;

    public AudioClip[] jumpSounds;

    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if(PlayerManager.Instance.CanMove) {
            horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if(Input.GetButtonDown("Jump")) {
                isJumping = true;
                if(controller.IsGrounded) {
                    audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);
                }
            }
        }
        else {
            horizontalMove = 0f;
        }
    }

    private void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }
}
