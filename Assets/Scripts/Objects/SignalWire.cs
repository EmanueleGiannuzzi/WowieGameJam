using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SignalWire : SignalReceiver {
    public SpriteRenderer spriteRenderer;

    public bool isActive = false;
    public WireType wireType = WireType.DOUBLE;

    public Sprite doubleSprite;
    public Sprite tripleSprite;
    public Sprite quadrupleSprite;
    public Sprite cornerSprite;

    public enum WireType {
        DOUBLE,
        TRIPLE,
        QUADRUPLE,
        CORNER
    }

    private void Awake() {
        canInteract = false;
    }

    public override void OnSignalReceived(bool active) {
        UpdateState(active, true);
    }

    private void UpdateState(bool active, bool propagate) {
        isActive = active;
        spriteRenderer.color = isActive ? Color.green : Color.red;

        if(propagate) {
            foreach(SignalWire brother in this.transform.parent.GetComponentsInChildren<SignalWire>()) {
                if(brother != this) {
                    brother.UpdateState(active, false);
                }
            }
        }
    }

    protected override void OnInteract() {}

    private void OnValidate() {
        Sprite sprite = null;
        switch(wireType) {
            case WireType.DOUBLE:
                sprite = doubleSprite;
                break;
            case WireType.TRIPLE:
                sprite = tripleSprite;
                break;
            case WireType.QUADRUPLE:
                sprite = quadrupleSprite;
                break;
            case WireType.CORNER:
                sprite = cornerSprite;
                break;
        }

        spriteRenderer.sprite = sprite;
        spriteRenderer.color = isActive ? Color.green : Color.red;

        //this.transform.Rotate(Vector3.forward, rotation);
    }
}
