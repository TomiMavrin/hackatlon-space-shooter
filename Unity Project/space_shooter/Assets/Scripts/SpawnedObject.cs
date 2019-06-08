using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnedObject : NetworkBehaviour
{
	public float Speed;
	private Rigidbody2D body;

	public SpawnedObject()
	{
		Speed = 1f;
	}

    // Start is called before the first frame update
    void Start()
    {
		body = GetComponent<Rigidbody2D>();
		body.velocity = Speed * (Quaternion.Euler(0, 0, Random.Range(0, 360f)) * Vector2.right);
    }

    // Update is called once per frame
    void Update()
    {
		
    }
}
