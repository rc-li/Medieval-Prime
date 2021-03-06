﻿using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{

	public float moveSpeed;
	public float jumpForce;

	private Rigidbody2D myRigidBody;

	public bool grounded;
	public LayerMask whatIsGround;

	private Collider2D myCollider;

	private bool initJump;

	ScoreManager sm;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
		myCollider = GetComponent<Collider2D>();
		sm = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
		grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        //myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
			if (grounded){
				myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
				initJump = true;
			}
			else if (initJump){
				myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
				initJump = false;
			}
		}
    }

	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("coin")){
			Destroy(other.gameObject);
			sm.ChangeScore(1);
		}
	}
}
