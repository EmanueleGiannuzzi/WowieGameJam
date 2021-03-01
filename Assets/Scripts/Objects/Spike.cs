using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class Spike : CallOnContact {
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    bool isAnimating = false;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        OnSignalReceived(isActive);
    }

    public override void OnSignalReceived(bool active) {
        base.OnSignalReceived(active);

        spriteRenderer.color = active ? Color.green : Color.red;

        Vector3 scale = this.transform.localScale;
        Vector3 position = this.transform.position;

        //position.y = active ? 0.5f : 0.125f;
        //scale.y = active ? 1f : 0.25126f;

        this.transform.localScale = scale;
        this.transform.position = position;
    }

    protected override void OnContact() {
        if(!isAnimating) {
            isAnimating = true;

            audioSource.Play();

            float duration = audioSource.clip.length;

            LeanTween.delayedCall(this.gameObject, 0.1f, () => {
                LeanTween.scaleY(this.gameObject, 1f, duration * 0.5f);
                LeanTween.moveLocalY(this.gameObject, 0.5f, duration * 0.5f);

                LeanTween.delayedCall(this.gameObject, 2f, () => {
                    LeanTween.moveLocalY(this.gameObject, 0.125f, 0.35f);
                    LeanTween.scaleY(this.gameObject, 0.25126f, 0.35f)
                        .setOnComplete(() => {
                            isAnimating = false;
                        });
                });
            });

            //PlayerManager.Instance.Die();

            LeanTween.delayedCall(this.gameObject, duration * 0.75f, () => PlayerManager.Instance.Die());
        }
    }
}
