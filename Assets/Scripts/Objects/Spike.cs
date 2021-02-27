using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class Spike : DeadlyOnContact {
    SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        OnSignalReceived(isActive);
    }

    public override void OnSignalReceived(bool active) {
        base.OnSignalReceived(active);

        spriteRenderer.color = active ? Color.green : Color.red;

        Vector3 scale = this.transform.localScale;
        Vector3 position = this.transform.position;

        position.y = active ? 0.5f : 0.125f;
        scale.y = active ? 1f : 0.25126f;

        this.transform.localScale = scale;
        this.transform.position = position;
    }
}
