using UnityEngine;

public class MoveHomingScript : MonoBehaviour {

	public bool active = true;
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

	public void startDash() {
		if (dashing)
			return;
		dashing = true;
		dashSpeed = speed*1.5f;
		currentDashTime = 0f;

//		Debug.Log("Moveto pos:  "+moveToPosition.x);
//		Debug.Log("Transform pos:  "+transform.position.x);
		if (moveToPosition.x < transform.position.x)
			moveDirection = -1;
		else
			moveDirection = 1;

		Vector2 vvv = new Vector2((moveToPosition.x - transform.position.x),(moveToPosition.y - transform.position.y));

		vvv = vvv.normalized;
		vvv *= 10;
		moveToPosition += vvv;

//		Debug.Log("Moveto pos:  "+moveToPosition);
	}

	void FixedUpdate() {

		if (!active)
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
			Mathf.Clamp(movement.x,BattleConstants.NormalStartX-BattleConstants.NormalBorderWidth,BattleConstants.NormalStartX+BattleConstants.NormalBorderWidth),
			Mathf.Clamp(movement.y,BattleConstants.NormalStartY-BattleConstants.NormalBorderHeight,BattleConstants.NormalStartY+BattleConstants.NormalBorderHeight));
		
		rigidbodyComponent.MovePosition(movement);
	}


	public float GetDashPercent(){
		return currentDashTime/dashTime;
	}
}