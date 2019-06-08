using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnedObject : NetworkBehaviour
{
	public float Speed;
	public float MaxTravelDistance;
	private Vector2 LastPosition;
    private float DistanceTravelled;

	public SpawnedObject()
	{
		Speed = 1f;
		MaxTravelDistance = 100.0f;
	}

    // Start is called before the first frame update
    void Start()
    {
		LastPosition = transform.position;
        DistanceTravelled = 0.0f;
    }

	public void SetMovementDirection(Vector2 direction)
	{
		GetComponent<Rigidbody2D>().velocity = Speed * direction;
	}

    // Update is called once per frame
    void Update()
    {
        Vector2 curPosition = (Vector2)transform.position;
		float curTravelDistance = (curPosition - LastPosition).magnitude;
        DistanceTravelled += curTravelDistance;
		if (DistanceTravelled > MaxTravelDistance)
			Destroy(this.gameObject);
        LastPosition = curPosition;
    }
}
