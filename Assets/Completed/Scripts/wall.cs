﻿using UnityEngine;
using System.Collections;

public class wall : MonoBehaviour {

	public Sprite dmgSprite;
	public int hp = 4;


	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () 
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	public void DamageWall (int loss)
	{
		spriteRenderer.sprite = dmgSprite;
		hp -= loss;
		if (hp <= 0)
			gameObject.SetActive (false);
	}
}
