using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance;

    [Header("Player")]
    public GameObject Player;
    public GameObject Ghost;
    public GameObject RespawnPoint;

    [Header("PostProcess")]
    public PostProcessVolume ghostVolume;

    private float spriteMaskScale = 125f;
    private SpriteMask spriteMask;

    public bool IsAlive { private set; get; } = true;
    public bool CanMove { private set; get; } = true;

    private void Start() {
        Player.SetActive(true);
        Ghost.SetActive(false);
        RespawnPoint.SetActive(false);
        //ghostVolume.profile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
        //ghostVolume.profile.TryGetSettings<ColorGrading>(out colorGrading);

        spriteMask = Ghost.GetComponentInChildren<SpriteMask>();
        //spriteMaskScale = spriteMask.gameObject.transform.localScale.x;

        IsAlive = false;
        Respawn();
    }

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else if(Instance != this) {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void Die() {
        if(IsAlive) {
            IsAlive = false;
            CanMove = false;

            Player.GetComponent<Collider2D>().enabled = false;
            Player.GetComponent<Rigidbody2D>().simulated = false;

            Vector3 ghostPos = Player.transform.position;
            ghostPos.y -= 0.1f;
            ghostPos.x -= 0.11f;
            Ghost.transform.position = ghostPos;
            Ghost.SetActive(true);
            //Ghost.GetComponent<Rigidbody2D>().velocity = Player.GetComponent<Rigidbody2D>().velocity;

            RespawnPoint.SetActive(true);

            LeanTween.value(gameObject, 0f, 1f, 1f).setOnUpdate((float value) =>
            {
                SpriteRenderer ghostRenderer = Ghost.GetComponent<SpriteRenderer>();
                Color newGhostColor = ghostRenderer.color;
                newGhostColor.a = value;
                ghostRenderer.color = newGhostColor;

                SpriteRenderer respawnPointRenderer = RespawnPoint.GetComponent<SpriteRenderer>();
                Color newRespawnColor = respawnPointRenderer.color;
                newRespawnColor.a = value;
                respawnPointRenderer.color = newRespawnColor;

                SpriteRenderer playerRenderer = Player.GetComponent<SpriteRenderer>();
                Color newPlayerColor = playerRenderer.color;
                newPlayerColor.a = 1f - value;
                playerRenderer.color = newPlayerColor;

                spriteMask.transform.localScale = Vector3.one * spriteMaskScale * value; 

                ghostVolume.weight = value;
            }).setOnComplete(() => {
                CanMove = true;
                Player.GetComponent<Collider2D>().enabled = true;
                Player.GetComponent<Rigidbody2D>().simulated = true;
                Player.GetComponent<SpriteRenderer>().color = Color.white;
                Player.SetActive(false);
            }).setEaseOutCirc();
        }
    }

    public void Respawn() {
        if(!IsAlive) {
            GameManager.ResetSwitches();

            IsAlive = true;
            CanMove = false;

            Ghost.GetComponent<Collider2D>().enabled = false;
            Ghost.GetComponent<Rigidbody2D>().simulated = false;

            //Vector3 playerPos = Ghost.transform.position;
            //playerPos.y += 0.1f;
            //playerPos.x += 0.11f;
            Player.transform.position = RespawnPoint.transform.position;
            Player.SetActive(true);

            RespawnPoint.SetActive(false);

            LeanTween.value(gameObject, 0f, 1f, 1f).setOnUpdate((float value) => {
                SpriteRenderer ghostRenderer = Ghost.GetComponent<SpriteRenderer>();
                Color newGhostColor = ghostRenderer.color;
                newGhostColor.a = 1f - value;
                ghostRenderer.color = newGhostColor;

                //SpriteRenderer respawnPointRenderer = RespawnPoint.GetComponent<SpriteRenderer>();
                //Color newRespawnColor = respawnPointRenderer.color;
                //newRespawnColor.a = 1f - value;
                //respawnPointRenderer.color = newRespawnColor;

                SpriteRenderer playerRenderer = Player.GetComponent<SpriteRenderer>();
                Color newPlayerColor = playerRenderer.color;
                newPlayerColor.a = value;
                playerRenderer.color = newPlayerColor;

                spriteMask.transform.localScale = Vector3.one * spriteMaskScale * (1f - value);

                ghostVolume.weight = 1f - value;
            }).setOnComplete(() => {
                CanMove = true;
                Ghost.GetComponent<Collider2D>().enabled = true;
                Ghost.GetComponent<Rigidbody2D>().simulated = true;
                Ghost.GetComponent<SpriteRenderer>().color = Color.white;
                Ghost.SetActive(false);
            }).setEaseOutCirc();
        }
    }
}
