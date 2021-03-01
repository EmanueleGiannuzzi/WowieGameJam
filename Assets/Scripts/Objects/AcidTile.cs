using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidTile : FakeTile {

    protected override void OnShowHide(bool hide, float value) {
        Color newColor = spriteRenderer.color;
        newColor.a = hide ? 1f - value : value;
        spriteRenderer.color = newColor;
    }

    private new void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision);

        PlayerManager.Instance.Die();
    }

    protected override bool ShoudlHideOnActive() {
        return false;
    }
}
