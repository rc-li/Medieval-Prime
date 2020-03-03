using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	public static PlayerControl instance;
	public float upForce;		
	private bool isDead = false;            
	public bool grounded;
	public bool isDashing = false;
	public LayerMask whatIsGround;
	private Animator anim;					
	private Rigidbody2D rb2d;               
	private Collider2D coll;
	private bool initJump;
	public Vector2 gravityModifier;
	public float dashDuration;
	private float dashSmooth;
	public Vector3 dashRotation;

	// for mobile control
	private float screenWidth;

	private void Awake()
	{
		Physics2D.gravity = gravityModifier;
	}
	void Start()
	{
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>(); 

		//for mobile control
		screenWidth = Screen.width;
		
	}

	void Update()
	{
		// Make sure player stands straight
		//rb2d.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
		//Don't allow control if the bird has died.
		if (isDead == false && GameControl.instance.isPlaying) 
		{
			grounded = Physics2D.IsTouchingLayers(coll, whatIsGround);

			/*if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
			{
				// Jump
				if (grounded)
				{
					//anim.SetTrigger("Flap");
					rb2d.velocity = Vector2.zero;
					rb2d.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
					initJump = true;
				}
				// Double Jump
				else if (initJump)
				{
					//anim.SetTrigger("Flap");
					rb2d.velocity = Vector2.zero;
					rb2d.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
					initJump = false;
				}
			}*/

			if (Input.GetMouseButtonDown(1))
			{
				// Dash down if character is jumping
				if (!grounded)
				{
					rb2d.velocity = Vector2.zero;
					rb2d.AddForce(new Vector2(0, -upForce * 1.5f),ForceMode2D.Impulse);
					initJump = false;
				}
				// Slide if character is on the ground
				else if(grounded && !isDashing)
				{
					StartCoroutine(DashThrough());
					
				}
			}
		}
		IEnumerator DashThrough()
		{
			isDashing = true;
			dashSmooth = Time.deltaTime * dashDuration;
			transform.Rotate(dashRotation * dashSmooth);
			rb2d.AddForce(new Vector2(0, -upForce * 1.5f), ForceMode2D.Impulse);

			//yield on a new YieldInstruction that waits for 5 seconds.
			yield return new WaitForSeconds(2);

			dashSmooth = Time.deltaTime * dashDuration;
			transform.Rotate(new Vector3(0,0,90) * dashSmooth);
			rb2d.AddForce(new Vector2(0, upForce * 1.5f), ForceMode2D.Impulse);
			isDashing = false;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Hazard")
		{
			rb2d.velocity = Vector2.zero;
			isDead = true;
			isDashing = false;
			//anim.SetTrigger("Die");
			GameControl.instance.PlayerDied();
		}
		
	}

	public void Jump(){
		// Jump
		if (grounded)
		{
			//anim.SetTrigger("Flap");
			rb2d.velocity = Vector2.zero;
			rb2d.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
			initJump = true;
		}
		// Double Jump
		else if (initJump)
		{
			//anim.SetTrigger("Flap");
			rb2d.velocity = Vector2.zero;
			rb2d.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
			initJump = false;
		}
	}
}
