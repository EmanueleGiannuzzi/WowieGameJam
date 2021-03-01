using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class ChangeTextOnEvent : MonoBehaviour {
    TextMeshPro text;

    void Start() {
        text = GetComponent<TextMeshPro>();
    }

    public void ChangeText(string newText) {
        text.text = newText;
    }
}
