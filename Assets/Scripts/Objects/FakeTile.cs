using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FakeTile : SignalReceiver {
    protected SpriteRenderer spriteRenderer;
    bool isActive = false;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        canInteract = false;
        UpdateState(false, false);
    }

    protected void OnTriggerEnter2D(Collider2D collision) {
        if(!isActive && collision.CompareTag("Player") && !collision.gameObject.name.Contains("Ghost")) {
            UpdateHidden(true, true);
        }
    }

    private void UpdateHidden(bool hide, bool propagate) {
        if(hide) {
            isActive = true;
        }

        LeanTween.value(gameObject, 0f, 1f, 0.5f).setOnUpdate((float value) => {
            OnShowHide(hide, value);
        }).setOnComplete(() => {
            if(hide) {
                isActive = false;
            }
        }).setEaseOutCirc();

        if(propagate) {
            foreach(FakeTile brother in this.transform.parent.GetComponentsInChildren<FakeTile>()) {
                if(brother != this) {
                    brother.UpdateHidden(hide, false);
                }
            }

            if(hide) {
                LeanTween.delayedCall(2f, () => {
                    this.UpdateHidden(false, true);
                });
            }
        }
    }

    protected virtual void OnShowHide(bool hide, float value) {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color newColor = spriteRenderer.color;
        newColor.a = hide ? 1f - value : value;
        spriteRenderer.color = newColor;
    }

    public override void OnSignalReceived(bool active) {
        UpdateState(active, true);
    }

    private void UpdateState(bool active, bool propagate) {
        isActive = !active;
        this.GetComponent<Collider2D>().isTrigger = !isActive;
        this.gameObject.layer = isActive ? 0 : 10;
        if(ShoudlHideOnActive()) {
            GetComponent<SpriteRenderer>().maskInteraction = isActive ? SpriteMaskInteraction.None : SpriteMaskInteraction.VisibleOutsideMask;
        }

        if(propagate) {
            foreach(FakeTile brother in this.transform.parent.GetComponentsInChildren<FakeTile>()) {
                if(brother != this) {
                    brother.UpdateState(active, false);
                }
            }
        }
    }

    protected virtual bool ShoudlHideOnActive() {
        return true;
    }

    protected override void OnInteract() {}
}
