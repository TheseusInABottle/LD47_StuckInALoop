using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private GameplayManager gameplay;

	private Rigidbody ballRB;
	private float movX, movZ;

	public float speed;

	public bool expel;

	public AudioSource mushroomSound;
	public AudioSource pickupSound;

    // Start is called before the first frame update
    void Start()
    {
		gameplay = GameObject.Find("GameplayController").GetComponent<GameplayManager>();
		ballRB = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update()
	{
		movX = Input.GetAxisRaw("Horizontal");
		movZ = Input.GetAxisRaw("Vertical");
	}

	// FixedUpdate is called once per physics Update
	void FixedUpdate()
    {
		if (gameplay.playerHP > 0)
		{
			Vector3 moveDirection = new Vector3(movX, 0, movZ);

			ballRB.AddForce(moveDirection * speed);
		} else
		{
			gameObject.SetActive(false);
		}

		if (expel == true)
		{
			ballRB.AddForce((ballRB.velocity) * 3, ForceMode.Impulse);
			expel = false;
		}

    }

	private void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
			case "KeyItem": gameplay.keyItems++; Destroy(other.gameObject); pickupSound.Play();
				break;
			case "Health": gameplay.playerHP++; Destroy(other.gameObject); pickupSound.Play();
				break;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Bumper"))
		{
			ballRB.AddForce((ballRB.velocity * -1) * 3, ForceMode.Impulse);
			mushroomSound.Play();
		}
	}
}
