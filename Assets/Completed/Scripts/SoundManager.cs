﻿using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioSource efxSource;
	public AudioSource musicSource
	public static SoundManager instance = null;

	public float lowPitchRange = .95;
	public float highPitchRange = 1.05f;

	// Use this for initialization
	void Start ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}

	public void PlaySingle (AudioClip clip)
	{
		efxSource.clip = clip;
		efxSource.Play ();
	}

	public void RandomizeSfx (params AudioClip [] clips)
	{
		int randomIndex = Random.Ranage
















	// Update is called once per frame
	void Update () {
	
	}
}