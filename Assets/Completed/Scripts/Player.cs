using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

	public class Player : MovingObject {

	public int wallDamage = 1;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 20;
	public float restartLevelDelay = 1f;
	public Text foodText;

	private Animator animator;
	private int food;

		// Use this for initialization
	protected override void Start ()
	{
		animator = GetComponent<Animator> ();

		food = GameManager.instance.playerFoodPoints;
	
		foodText.text = "Food:" + food;

		base.Start ();
	}
	
		private void OnDisable()
	{
		GameManager.instance.playerFoodPoints = food;
	}
		
	// Update is called once per frame
	void Update () 
	{
		if (!GameManager.instance.playerTurn)
			return;

        int horizontal = 0;
		int vertical = 0;

		horizontal =(int) Input.GetAxisRaw("Horizontal");
		vertical = (int) Input.GetAxisRaw("Vertical");

		if (horizontal != 0)
		    vertical = 0;


		if (horizontal != 0 || vertical != 0)
		AttemptMove<wall> (horizontal, vertical);
	}

	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		food--;
		foodText.text = "Food" + food;

		base.AttemptMove <T> (xDir, yDir);

		RaycastHit2D hit;

		CheckIfGameOver ();

		GameManager.instance.playerTurn = false;
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == ("Exit"))
		{   
			Invoke ("Restart", restartLevelDelay);
			enabled = false;
				
		}
			else if (other.tag == "Food")
		{
			food += pointsPerFood;
			foodText.text = "+" + pointsPerFood + "Food: " + food;
			other.gameObject.SetActive(false);
		}
			else if(other.tag == "Soda")
		{
			food += pointsPerSoda;
			foodText.text = "+" + pointsPerSoda + " Food: " + food;
			other.gameObject.SetActive(false);
		}
	}

	protected override void OnCantMove <T> (T component)
	{
		wall hitWall = component as wall;
		hitWall.DamageWall (wallDamage);
		animator.SetTrigger ("PlayerChop");
	}

	private void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void LoseFood (int Loss)
	{
		animator.SetTrigger ("PlayerHit");
		food -= Loss;
		foodText.text = "-" + Loss + "Food: " + food;
		CheckIfGameOver();
	}
	private void CheckIfGameOver()
	{
		if (food <= 0)
		GameManager.instance.GameOver ();
	}
}