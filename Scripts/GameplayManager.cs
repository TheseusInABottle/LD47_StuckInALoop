using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
	private static int loops;

	private GameObject player;


	public int keyItems = 0;
	public int playerHP = 4;
	public Text timeRemaining;
	public Text hitPoints;
	public Text wandParts;
	public Text deathText;
	public Text winText;

	private float timerStartValue = 0f;
	[SerializeField] private float timeLimit = 60f;
	public float timerValue = 0f;

	private bool winner = false;

    // Start is called before the first frame update
    void Start()
    {
		loops++;
		timerValue = timerStartValue;
		player = GameObject.Find("Player");
		deathText.text = "";
		winText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
		int timeToFinish;
        if (keyItems >= 3)
		{
			winner = true;
		}

		if (timeLimit < timerValue && winner == false)
		{
			Scene currentLoop = SceneManager.GetActiveScene();
			SceneManager.LoadScene(currentLoop.name);
			Debug.Log("Out of time restarting");
		}

		if(playerHP <= 0)
		{
			deathText.text = "You Died!";
			player.SetActive(false);
			StartCoroutine(PlayerDestroyed());
		}

		if (winner == false)
		{
			timerValue += Time.deltaTime;
		}

		timeToFinish = Mathf.RoundToInt(timeLimit - timerValue);

		timeRemaining.text = timeToFinish.ToString();
		hitPoints.text = "HP: " + playerHP;
		wandParts.text = keyItems + "/3";

		if (winner == true)
		{
			winText.text = "You fixed your wand!";
			StartCoroutine(PlayerWins());
		}

    }

	public void PlayerTakeDamage(int damage)
	{

		playerHP -= damage;
	}

	IEnumerator PlayerWins()
	{
		yield return new WaitForSeconds(4);
		SceneManager.LoadScene(2);
		yield return null;
	}

	IEnumerator PlayerDestroyed()
	{
		yield return new WaitForSeconds(3f);
		Scene currentLoop = SceneManager.GetActiveScene();
		SceneManager.LoadScene(currentLoop.name);
		yield return null;
	}
}
