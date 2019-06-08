using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControlls : NetworkBehaviour {

    private Camera playerCamera;
    private Rigidbody2D body;
    public Laser laser;
    public GameObject laserSpawn;
    public Vector2 spawnPosition;
    private MainMenuUI gameManager;

    public GameObject playerRedFlag;
    public GameObject playerBlueFlag;

    private GameObject redFlag;
    private GameObject blueFlag;

    static int Team = 0;

    public float thrust = 25.0f;
    public float maxSpeed = 2.0f;
    public float laserCooldown = 0.5f;
    private float laserTimer = 0.0f;
    public int team;
    public int health = 2;

    private void Start() {
        body = GetComponent<Rigidbody2D>();
        playerCamera = FindObjectOfType<Camera>();
        gameManager = FindObjectOfType<MainMenuUI>();
        team = PlayerControlls.Team++;
        spawnPosition = transform.position;      
    }

    private void OnEnable() {
        health = 2;
    }

    void Update() {
        if (isLocalPlayer == true) {
            if (laserTimer > 0.0f)
                laserTimer -= Time.deltaTime;
            Vector2 newDirection = (playerCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, newDirection);
            playerCamera.transform.position = transform.position + Vector3.back;

            float forward = (Input.GetKey(KeyCode.W)) ? 1.0f : 0.0f;
            float left = ((Input.GetKey(KeyCode.A)) ? 1.0f : 0.0f) - ((Input.GetKey(KeyCode.D)) ? 1.0f : 0.0f);

            if (forward != 0.0f || left != 0.0f) {
                float angle = Mathf.Atan2(left, forward) * Mathf.Rad2Deg;
                body.AddForce((Quaternion.Euler(0, 0, angle) * newDirection) * thrust);
            }
            Vector2 velocity = body.velocity;
            float velocityMag = velocity.magnitude;
            if (velocityMag > maxSpeed) {
                body.velocity = velocity.normalized * maxSpeed;
            }
            if (Input.GetMouseButtonDown(0) && laserTimer <= 0.0f) {
                laserTimer = laserCooldown;
                CmdShoot(newDirection);
            }
        }
    }

    //public void LaserHit(PlayerControlls enemy) {
    //    health--;
    //    if (health <= 0) {
    //        gameManager.RespawnPlayer(this);
    //        gameObject.SetActive(false);
    //    }
    //}

    void OnCollisionEnter2D(Collision2D collision) {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Debris") && collision.relativeVelocity.magnitude > 0) {
            gameManager.RespawnPlayer(this);
            gameObject.SetActive(false);
            if (team % 2 == 0) {
                redFlag.SetActive(true);
                playerRedFlag.SetActive(false);
            }
            else {
                blueFlag.SetActive(true);
                playerBlueFlag.SetActive(false);
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(team + " " + collision.gameObject.tag);

        if (team % 2 == 0 && collision.gameObject.tag == "FlagRed") {
            playerRedFlag.gameObject.SetActive(true);
            collision.gameObject.SetActive(false);
            redFlag = collision.gameObject;
        }
        else if (team % 2 == 1 && collision.gameObject.tag == "FlagBlue") {
            playerBlueFlag.gameObject.SetActive(true);
            collision.gameObject.SetActive(false);
            blueFlag = collision.gameObject;
        }

        if(playerRedFlag.activeSelf && collision.gameObject.tag == "FlagBlue") {
            FindObjectOfType<WinnerScript>().ShowWinner();
            Debug.Log("You won!");
        }
        if (playerBlueFlag.activeSelf && collision.gameObject.tag == "FlagRed") {
            Debug.Log("You won!");
            FindObjectOfType<WinnerScript>().ShowWinner();
        }

        else if(collision.gameObject.tag == "Laser"){
            gameManager.RespawnPlayer(this);
            gameObject.SetActive(false);
            if (team % 2 == 0) {
                redFlag.SetActive(true);
                playerRedFlag.SetActive(false);
            }
            else {
                blueFlag.SetActive(true);
                playerBlueFlag.SetActive(false);
            }
        }
    }

    [Command]
    void CmdShoot(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        Laser l = Instantiate<Laser>(laser, laserSpawn.transform.position, Quaternion.Euler(0, 0, angle));
        l.Init(gameObject, direction);
        NetworkServer.Spawn(l.gameObject);
    }
}
