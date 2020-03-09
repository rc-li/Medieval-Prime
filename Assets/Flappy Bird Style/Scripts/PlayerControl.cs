using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public static PlayerControl instance;
	public float upForce;
	private bool isDead = false;
	public bool grounded = true;
	public bool isDashing = false;
	public LayerMask whatIsGround;
	private Animator anim;
	private Rigidbody2D rb2d;
	private Collider2D coll;
	private bool initJump = false;
	public Vector2 gravityModifier;
	public float dashDuration;
	public Vector3 dashRotation;
	private SpriteRenderer renderer;
	public int lifeCounter = 1;
	public int bufferTime = 3;

	private void Awake()
	{
		Physics2D.gravity = gravityModifier;
	}
	void Start()
	{
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
		renderer = GetComponent<SpriteRenderer>();
		lifeCounter = PlayerPrefs.GetInt("TotalLife", 1);

	}

	void Update()
	{
		// Make sure player stands straight
		//rb2d.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
		//Don't allow control if the bird has died.
		if (isDead == false && GameControl.instance.isPlaying)
		{
			grounded = Physics2D.IsTouchingLayers(coll, whatIsGround);
			//PC CONTROLS
			//Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)
			// MOBILE CONTROLS
			// touch.position.x > Screen.width/2 && touch.phase == TouchPhase.Ended || Input.GetKeyDown(KeyCode.Space)
			// var touch = Input.GetTouch(0);
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && !isDashing)
			{
				// Jump
				if (grounded)
				{
					//anim.SetTrigger("Flap");
					rb2d.velocity = Vector2.zero;
					rb2d.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
					renderer.color = new Color(0f, 255f, 0f, 1f);
					initJump = true;
				}
				// Double Jump
				else if(initJump)
				{
					//anim.SetTrigger("Flap");
					rb2d.velocity = Vector2.zero;
					rb2d.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
					renderer.color = new Color(0.5f, 0.5f, 0.5f, 1f);
					initJump = false;
				}
			}
			// PC CONTROLS
			// Input.GetMouseButtonDown(1)
			// MOBILE CONTROLS
			// touch.position.x < Screen.width / 2
			else if (Input.GetMouseButtonDown(1))
			{
				Dash();
			}


		}

	}


	void Dash()
	{
		// Dash down if character is jumping
		if (!grounded)
		{
			rb2d.velocity = Vector2.zero;
			rb2d.AddForce(new Vector2(0, -upForce * 1.5f), ForceMode2D.Impulse);
			initJump = false;
		}
		// Slide if character is on the ground
		else if (grounded && !isDashing)
		{
			StartCoroutine(DashThrough());

		}
	}
	IEnumerator DashThrough()
	{
		Debug.Log("Started Dash");
		isDashing = true;
		renderer.color = new Color(153f,0f, 0f, 1f);
		//yield on a new YieldInstruction that waits for 5 seconds.
		yield return new WaitForSeconds(dashDuration);

		Debug.Log("Waited Dash");
		renderer.color = Color.white;
		isDashing = false;
	}

	void OnTriggerEnter2D(Collider2D col)
	{	
		Debug.Log("Trigger");

		if((col.gameObject.tag == "lowHazard" && grounded == true) || 
		(col.gameObject.tag == "highHazard" && initJump == true && grounded == false) ||
		(col.gameObject.tag == "dashHazard" && isDashing == false))
		{
			GameControl.instance.updateLifeCounter();

			if(lifeCounter == 0)
			{
				rb2d.velocity = Vector2.zero;
				GameControl.instance.PlayerDied();
				isDashing = false;
				isDead = true;
				//anim.SetTrigger("Die");
			} else 
			{
				
			}
		}
		
		// if (col.gameObject.tag == "lowHazard" && grounded == true)
		// {
		// 	Debug.Log("Trigger low");
		// 	rb2d.velocity = Vector2.zero;
		// 	isDead = true;
		// 	isDashing = false;
		// 	//anim.SetTrigger("Die"); 
			
		// 	GameControl.instance.PlayerDied();
		// }

		// if (col.gameObject.tag == "highHazard" && initJump == true && grounded == false)
		// {
		// 	Debug.Log("Trigger high");
		// 	rb2d.velocity = Vector2.zero;
		// 	isDead = true;
		// 	isDashing = false;
		// 	//anim.SetTrigger("Die");
		// 	GameControl.instance.PlayerDied();
		// }

		// if (col.gameObject.tag == "dashHazard" && isDashing == false)
		// {
		// 	Debug.Log("Trigger dash");
		// 	rb2d.velocity = Vector2.zero;
		// 	isDead = true;
		// 	isDashing = false;
		// 	//anim.SetTrigger("Die");
		// 	GameControl.instance.PlayerDied();
		// }



	}
}
