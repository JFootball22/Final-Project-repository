using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyMover : MonoBehaviour
{
	public float speed;

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * speed;
	}

	void FixedUpdate()
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			speed = speed * 3;
			Debug.Log("Hi");
		}
	}
}

