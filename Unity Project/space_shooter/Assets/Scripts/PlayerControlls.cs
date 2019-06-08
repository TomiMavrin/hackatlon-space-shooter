using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControlls : NetworkBehaviour {

    private Camera playerCamera;
    public float maxSpeed = 2.0f;
    private Rigidbody2D body;
    public float thrust = 25.0f;

    private void Start() {
        /*
        if (isLocalPlayer) {
            playerCamera.gameObject.SetActive(true);
        }
        else {
            playerCamera.gameObject.SetActive(false);
        }*/
        body = GetComponent<Rigidbody2D>();
        playerCamera = FindObjectOfType<Camera>();
    }

    void Update() {
        if (isLocalPlayer == true) {
            Vector2 newDirection = (playerCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, newDirection);
            playerCamera.transform.position = transform.position + Vector3.back;

            float forward = (Input.GetKey(KeyCode.W)) ? 1.0f : 0.0f;
            float left = ((Input.GetKey(KeyCode.A)) ? 1.0f : 0.0f) - ((Input.GetKey(KeyCode.D)) ? 1.0f : 0.0f);

            if (forward != 0.0f || left != 0.0f) {
                float angle = Mathf.Atan2(left, forward) * Mathf.Rad2Deg;
                newDirection = Quaternion.Euler(0, 0, angle) * newDirection;

                body.AddForce(newDirection * thrust);
            }
            Vector2 velocity = body.velocity;
            float velocityMag = velocity.magnitude;
            if (velocityMag > maxSpeed) {
                body.velocity = velocity.normalized * maxSpeed;
            }
        }
    }

}
