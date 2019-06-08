using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawner : NetworkBehaviour
{
	private float SpawnTimer;
	public float Radius;
	public float MinSpawnTime, MaxSpawnTime;
	public SpawnedObject[] objectList;

	public float Angle;

	public ObjectSpawner()
	{
		Radius = 1.0f;
		MinSpawnTime = 20.0f;
		MaxSpawnTime = 30.0f;
		Angle = 45.0f;
	}

    /*
	public void OnDrawGizmosSelected()
	{
        UnityEditor.Handles.color = Color.cyan;
        UnityEditor.Handles.DrawWireArc(transform.position, Vector3.back, transform.rotation * Vector3.right, -Angle, Radius);
	}
    */

	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		SpawnTimer -= Time.deltaTime;
		if (SpawnTimer <= 0f)
		{
			SpawnTimer = Random.Range(MinSpawnTime, MaxSpawnTime);
			CmdSpawnObject();
		}
    }

	SpawnedObject SpawnObject()
	{
		
        return null;
	}

	[Command]
	void CmdSpawnObject()
	{
        float distance = Random.Range(0f, Radius);
        Vector2 position = transform.position + transform.rotation * (Quaternion.Euler(0, 0, Random.Range(0, Angle)) * Vector2.right) * distance;
        int objectIdx = Random.Range(0, objectList.Length);
        if (objectList[objectIdx] != null) {
            SpawnedObject obj = Instantiate(objectList[objectIdx], position, Quaternion.Euler(0, 0, Random.Range(0, 360f)));
            NetworkServer.Spawn(obj.gameObject);
            obj.SetMovementDirection(transform.rotation * (Quaternion.Euler(0, 0, Random.Range(0, Angle)) * Vector2.right));
            
        }
	}
}
