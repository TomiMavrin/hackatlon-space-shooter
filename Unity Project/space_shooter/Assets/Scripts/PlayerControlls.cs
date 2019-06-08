using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControlls : NetworkBehaviour {

    public Camera playerCamera;
    public GameObject playerSprite;
    private float speed;
    public float maxSpeed;
    public float acceleration;
    public float deAcceleration;

    private void Start() {
        if (isLocalPlayer) {
            playerCamera.gameObject.SetActive(true);
        }
        else {
            playerCamera.gameObject.SetActive(false);
        }
    }

    void Update() {

        if (isLocalPlayer == true) {

            if (Input.GetKey(KeyCode.W)) {
                speed = Mathf.Min(maxSpeed, speed + acceleration * Time.deltaTime);
                //todo finish acceleration 
            }
            if (Input.GetKey(KeyCode.A)) {
                this.transform.Translate(Vector3.left * Time.deltaTime * 3f);
            }
        }

        Vector3 mousePos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 perpendicular = mousePos - transform.position;
        playerSprite.transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
    }

}
