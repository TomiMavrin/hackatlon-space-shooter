using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;

public class ObjectSpawner : NetworkBehaviour
{
	private float SpawnTimer;
	public float Radius;
	public float MinSpawnTime, MaxSpawnTime;
	public float MinLifespan, MaxLifespan;
	public SpawnedObject[] objectList;

	public ObjectSpawner()
	{
		Radius = 1.0f;
		MinSpawnTime = 20.0f;
		MaxSpawnTime = 30.0f;
		MinLifespan = 10.0f;
		MaxLifespan = 40.0f;
	}

	public void OnDrawGizmosSelected()
	{
		Handles.color = Color.cyan;
		Handles.DrawWireDisc(transform.position, Vector3.back, Radius);
	}

	void Start()
    {
		SpawnTimer = Random.Range(MinSpawnTime, MaxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
		SpawnTimer -= Time.deltaTime;
		if (SpawnTimer <= 0f)
		{
			SpawnTimer = Random.Range(MinSpawnTime, MaxSpawnTime);
			SpawnObject();
		}
    }

	void SpawnObject()
	{
		float distance = Random.Range(0f, Radius);
		Vector2 position = transform.position + Quaternion.Euler(0, 0, Random.Range(0, 360f)) * Vector2.right * distance;
		int objectIdx = Random.Range(0, objectList.Length);
		if (objectList[objectIdx] != null)
		{
			SpawnedObject obj = Instantiate(objectList[objectIdx], position, Quaternion.Euler(0, 0, Random.Range(0, 360f)));
			NetworkServer.Spawn(obj.gameObject);
		}
	}


	[Command]
	void CmdSpawnObject()
	{
		float distance = Random.Range(0f, Radius);
		Vector2 position = transform.position + Quaternion.Euler(0, 0, Random.Range(0, 360f)) * Vector2.right * distance;
		int objectIdx = Random.Range(0, objectList.Length);
		if (objectList[objectIdx] != null)
		{
			SpawnedObject obj = Instantiate(objectList[objectIdx], position, Quaternion.Euler(0, 0, Random.Range(0, 360f)));
			NetworkServer.Spawn(obj.gameObject);
		}
	}
}
