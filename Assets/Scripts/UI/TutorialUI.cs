using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour {
    public GameObject panel;
    public TextMeshProUGUI text;

    private void Update() {
        if(Input.GetKeyUp(KeyCode.Escape)) {
            panel.SetActive(!panel.activeSelf);
            if(panel.activeSelf) {
                text.text = "Press ESC to EXIT";
            }
            else {
                text.text = "Press ESC for HELP";
            }
        }
    }
}
