using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	public float upForce;					//Upward force of the "flap".
	private bool isDead = false;            //Has the player collided with a wall?
	public bool grounded;
	public LayerMask whatIsGround;
	private Animator anim;					//Reference to the Animator component.
	private Rigidbody2D rb2d;               //Holds a reference to the Rigidbody2D component of the bird.
	private Collider2D coll;
	private bool initJump;
	void Start()
	{
		//Get reference to the Animator component attached to this GameObject.
		anim = GetComponent<Animator> ();
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
		
	}

	void Update()
	{
		//Don't allow control if the bird has died.
		if (isDead == false) 
		{
			grounded = Physics2D.IsTouchingLayers(coll, whatIsGround);

			if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
			{
				if (grounded)
				{
					anim.SetTrigger("Flap");
					rb2d.velocity = Vector2.zero;
					rb2d.AddForce(new Vector2(0, upForce));
					initJump = true;
				}
				else if (initJump)
				{
					anim.SetTrigger("Flap");
					rb2d.velocity = Vector2.zero;
					rb2d.AddForce(new Vector2(0, upForce));
					initJump = false;
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Hazard")
		{
			// Zero out the bird's velocity
			rb2d.velocity = Vector2.zero;
			// If the bird collides with something set it to dead...
			isDead = true;
			//...tell the Animator about it...
			//anim.SetTrigger("Die");
			//...and tell the game control about it.
			GameControl.instance.PlayerDied();
		}
		
	}
}
