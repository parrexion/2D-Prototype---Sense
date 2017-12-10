using UnityEngine;

/// <summary>
/// Class which moves the object towards a position or to follow an object and 
/// stay within the boundaries. Also allows for dashing
/// </summary>
public class MoveHomingScript : MonoBehaviour {

	public BoolVariable paused;
	public Vector2 speed = new Vector2(5, 5);
	public Vector2 moveToPosition = new Vector2(0, 0);
	public Transform objectToFollow;
	public int moveDirection = 0;

	public Vector2 movement;
	private Rigidbody2D rigidbodyComponent;

	public bool dashing = false;
	public float dashTime = 0.75f;
	private float currentDashTime;
	private Vector2 dashSpeed;


	void Start() {
		if (rigidbodyComponent == null) 
			rigidbodyComponent = GetComponent<Rigidbody2D>();
		moveToPosition = new Vector2(transform.position.x,transform.position.y);
		currentDashTime = dashTime;
	}

	/// <summary>
	/// Lets the player dash if not currently dashing.
	/// </summary>
	public void startDash() {
		if (dashing)
			return;
		dashing = true;
		dashSpeed = speed*1.5f;
		currentDashTime = 0f;

		if (moveToPosition.x < transform.position.x)
			moveDirection = -1;
		else
			moveDirection = 1;

		Vector2 vvv = new Vector2((moveToPosition.x - transform.position.x),(moveToPosition.y - transform.position.y));

		vvv = vvv.normalized;
		vvv *= 10;
		moveToPosition += vvv;
	}

	/// <summary>
	/// Moves the transform towards the position or the object to follow.
	/// </summary>
	void FixedUpdate() {
		if (paused.value)
			return;

		if (objectToFollow == null) {
			if (dashing) {
				movement = Vector2.MoveTowards(transform.position,moveToPosition,dashSpeed.x*Time.fixedDeltaTime);
				dashSpeed -= new Vector2(0.3f,0.3f);
				currentDashTime += Time.deltaTime;
				dashing = (currentDashTime < dashTime);
				if (!dashing)
					moveToPosition = transform.position;
			}
			else
				movement = Vector2.MoveTowards(transform.position,moveToPosition,speed.x*Time.fixedDeltaTime);
			if (Vector2.Distance(transform.position,moveToPosition) > 0.35) {
				if (moveToPosition.x < transform.position.x)
					moveDirection = -1;
				else
					moveDirection = 1;
			}
			else {
				moveDirection = 0;
			}
		}
		else {
			movement = Vector2.MoveTowards (transform.position, objectToFollow.position, speed.x * Time.fixedDeltaTime);
			if (Vector2.Distance(transform.position,objectToFollow.position) > 0.25) {
				if (objectToFollow.position.x < transform.position.x)
					moveDirection = -1;
				else
					moveDirection = 1;
			}
			else {
				moveDirection = 0;
			}
		}
		movement.Set(
			Mathf.Clamp(movement.x,Constants.NormalStartX-Constants.NormalBorderWidth,Constants.NormalStartX+Constants.NormalBorderWidth),
			Mathf.Clamp(movement.y,Constants.NormalStartY-Constants.NormalBorderHeight,Constants.NormalStartY+Constants.NormalBorderHeight));
		
		rigidbodyComponent.MovePosition(movement);
	}

	/// <summary>
	/// Calculates how much is left of the dash in percent.
	/// </summary>
	/// <returns></returns>
	public float GetDashPercent(){
		return currentDashTime/dashTime;
	}
}