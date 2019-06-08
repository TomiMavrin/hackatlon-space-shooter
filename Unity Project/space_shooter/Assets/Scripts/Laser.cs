using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Laser : NetworkBehaviour
{
    public float Speed = 10.0f;
    private PlayerControlls owner;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 20.0f);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(GameObject owner, Vector2 dir) {
        GetComponent<Rigidbody2D>().velocity = dir * Speed;
        this.owner = owner.GetComponent<PlayerControlls>();
    }

}
