using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour {
    protected bool canInteract = true;

    bool isPlayerClose = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            isPlayerClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            isPlayerClose = false;
        }
    }

    private void Update() {
        if(canInteract && isPlayerClose 
            && Input.GetKeyUp(GameManager.Instance.InteractKey)) {
            OnInteract();
        }
    }

    protected abstract void OnInteract();
}
