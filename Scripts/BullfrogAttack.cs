using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullfrogAttack : MonoBehaviour
{
	private GameplayManager gameplay;
	private PlayerMovement expel;

	public float attackTime = 5;
	private float timeBetweenAttack;
	public float attackRange;
	public LayerMask playerLayer;
	private int damage = 1;

	private Animator anim;

	public AudioSource atkSound;


    // Start is called before the first frame update
    void Start()
    {
		gameplay = GameObject.Find("GameplayController").GetComponent<GameplayManager>();
		expel = GameObject.Find("Player").GetComponent<PlayerMovement>();
		timeBetweenAttack = attackTime;

		anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {


		if (timeBetweenAttack <= 0)
		{
			Collider[] playerDetection = Physics.OverlapSphere(gameObject.transform.position, attackRange, playerLayer);
			if (playerDetection.Length >= 1)
			{
				StartCoroutine(shockwave());
			}

			timeBetweenAttack = attackTime;
		}
		else
		{
			timeBetweenAttack -= Time.deltaTime;
		}
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		//Gizmos.DrawWireSphere(transform.position, attackRange);
	}

	IEnumerator shockwave()
	{
		anim.Play("shockwave");
		atkSound.Play();
		yield return new WaitForSeconds(1.5f);
		Collider[] playerStillHere = Physics.OverlapSphere(gameObject.transform.position, attackRange, playerLayer);
		if(playerStillHere.Length >= 1)
		{
			gameplay.PlayerTakeDamage(damage);
			expel.expel = true;
		}

		yield return null;
	}
}
