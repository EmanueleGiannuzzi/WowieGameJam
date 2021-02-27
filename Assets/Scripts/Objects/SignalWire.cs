using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SignalWire : SignalReceiver {
    SpriteRenderer spriteRenderer;

    public bool isActive = false;
    public WireType wireType = WireType.DOUBLE;
    public int rotation = 0; 

    public Sprite[] doubleSprite = new Sprite[2];
    public Sprite[] tripleSprite = new Sprite[2];
    public Sprite[] quadrupleSprite = new Sprite[2];
    public Sprite[] cornerSprite = new Sprite[2];

    public enum WireType {
        DOUBLE,
        TRIPLE,
        QUADRUPLE,
        CORNER
    }

    private void Awake() {
        canInteract = false;
    }

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnSignalReceived(bool active) {
        
    }

    protected override void OnInteract() {}

    private void OnValidate() {
        Sprite[] spriteArray = null;
        switch(wireType) {
            case WireType.DOUBLE:
                spriteArray = doubleSprite;
                break;
            case WireType.TRIPLE:
                spriteArray = tripleSprite;
                break;
            case WireType.QUADRUPLE:
                spriteArray = quadrupleSprite;
                break;
            case WireType.CORNER:
                spriteArray = cornerSprite;
                break;
        }

        spriteRenderer.sprite = spriteArray[isActive ? 1 : 0];

        this.transform.Rotate(Vector3.forward, rotation);
    }
}
