using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {

    public float dampTime = 0.15f;
    public float borderCheckRadius;
    private Vector3 velocity = Vector3.zero;
    public Transform[] targets;

    Camera thisCamera;

    private void Start() {
        thisCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        Transform target = null;
        foreach(Transform possibleTarget in targets) {
            if(possibleTarget != null && possibleTarget.gameObject.activeSelf) {
                target = possibleTarget;
                break;
            }
        }

        if(target) {
            Vector3 point = thisCamera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - thisCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;

            //RaycastHit2D hit = Physics2D.Raycast(destination, delta, borderCheckRadius, LayerMask.GetMask("Border"));

            //Debug.DrawRay(destination, delta, Color.cyan);

            //if(hit.collider == null) {
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            //}
        }

    }
}