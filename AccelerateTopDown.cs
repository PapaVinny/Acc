using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateTopDown : MonoBehaviour
{

	public float maxWalkSpeed;
	public float maxRunSpeed;
	public float acceleration;
	public float inputX;
	public float inputY;
	public bool isTryingToRun;
	public bool isRunning;
	private Rigidbody2D rb;

	//Add something to drain stamina while running and maybe increase the acceleration;

	void Start()
	{
		maxWalkSpeed = 5f;
		maxRunSpeed = 15f;
		acceleration = 4f;
		rb = GetComponent<Rigidbody2D>();
	}

   
	void Update()
	{
		inputX = Input.GetAxisRaw("Horizontal");
		inputY = Input.GetAxisRaw("Vertical");

		Accelerate();
		CheckRunning();
		Decelerate();

		if (!isRunning)
		{
			CapSpeed();
		}
		else CapRunSpeed();

	}

	private void CheckRunning() {
		isTryingToRun = Input.GetKey(KeyCode.LeftShift);

		if ( (isTryingToRun && (Mathf.Abs(rb.velocity.x) == maxWalkSpeed || Mathf.Abs(rb.velocity.y) == maxWalkSpeed)) || 
			(isRunning && (Mathf.Abs(rb.velocity.x) > maxWalkSpeed || Mathf.Abs(rb.velocity.y) > maxWalkSpeed))) {
			isRunning = true;
		} else isRunning = false;
	}

	void Accelerate() {
		if (!isRunning) {
			rb.AddForce(new Vector2(inputX * acceleration, inputY * acceleration));
		}else rb.AddForce(new Vector2((inputX * acceleration)/2, (inputY * acceleration))/2);

	}

	void Decelerate()
	{														
		if (Mathf.Abs(rb.velocity.x) > 0 && inputX == 0 || (isRunning && !isTryingToRun && Mathf.Abs(rb.velocity.x) > maxWalkSpeed)) {

			if (Mathf.Abs(rb.velocity.x) < 1f)
			{
				rb.velocity = new Vector2(0, rb.velocity.y);
			}
			else rb.AddForce(new Vector2(acceleration * -(Mathf.Abs(rb.velocity.x) / rb.velocity.x), 0));

			
		}

		if (Mathf.Abs(rb.velocity.y) > 0 && inputY == 0 || (isRunning && !isTryingToRun && Mathf.Abs(rb.velocity.y) > maxWalkSpeed))
		{ 

			if (Mathf.Abs(rb.velocity.y) < 1f)
			{
				rb.velocity = new Vector2(rb.velocity.x,0);
			}else rb.AddForce(new Vector2(0 , acceleration * -(Mathf.Abs(rb.velocity.y) / rb.velocity.y)));
		}
	}

	void CapSpeed()
	{
		if (Mathf.Abs(rb.velocity.x) > maxWalkSpeed) {
			rb.velocity = new Vector2(maxWalkSpeed * (Mathf.Abs(rb.velocity.x)/rb.velocity.x) , rb.velocity.y);
		}

		if (Mathf.Abs(rb.velocity.y) > maxWalkSpeed)
		{
			rb.velocity = new Vector2(rb.velocity.x , maxWalkSpeed * (Mathf.Abs(rb.velocity.y) / rb.velocity.y));
		}
	}

	void CapRunSpeed() {
		if (Mathf.Abs(rb.velocity.x) > maxRunSpeed)
		{
			rb.velocity = new Vector2(maxRunSpeed * (Mathf.Abs(rb.velocity.x) / rb.velocity.x), rb.velocity.y);
		}

		if (Mathf.Abs(rb.velocity.y) > maxRunSpeed)
		{
			rb.velocity = new Vector2(rb.velocity.x, maxRunSpeed * (Mathf.Abs(rb.velocity.y) / rb.velocity.y));
		}
	}
}
