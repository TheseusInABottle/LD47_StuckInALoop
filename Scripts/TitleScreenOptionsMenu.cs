using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class TitleScreenOptionsMenu : MonoBehaviour
{
	public Button options;
	public GameObject optionsPanel;
	public AudioClip[] sfx;
	public AudioSource soundBlaster;
	public bool activeOptions = false;

	public AudioMixer musicMixer;
	public AudioMixer sfxMixer;

    // Start is called before the first frame update
    void Start()
    {
		optionsPanel = GameObject.Find("Options Panel");
		optionsPanel.SetActive(activeOptions);
    }

    // Update is called once per frame
    void Update()
    {
		optionsPanel.SetActive(activeOptions);
    }

	public void OpenOptionsMenu()
	{
		activeOptions = !activeOptions;
	}

	public void TestSFX()
	{
		int x = Random.Range(0, sfx.Length);
		soundBlaster.clip = sfx[x];
		soundBlaster.Play();
	}

	public void SetMusicVolume(float volume)
	{
		musicMixer.SetFloat("musicVolume", volume);
	}

	public void SetSFXVolume(float noise)
	{
		sfxMixer.SetFloat("sfxVolume", noise);
	}
}
