using UnityEngine;

public class MoveHomingNoLimit : MonoBehaviour {

	public bool active = true;
	public Vector2 speed = new Vector2(5, 5);
	public Vector2 moveToPosition = new Vector2(0, 0);
	public Transform objectToFollow;

	public Vector2 movement;
	private Rigidbody2D rigidbodyComponent;


	void Start() {
		if (rigidbodyComponent == null) 
			rigidbodyComponent = GetComponent<Rigidbody2D>();
		moveToPosition = new Vector2(transform.position.x,transform.position.y);
	}

	void FixedUpdate() {

		if (!active)
			return;

		if (objectToFollow == null) {
			movement = Vector2.MoveTowards(transform.position,moveToPosition,speed.x*Time.fixedDeltaTime);
		}
		else {
			movement = Vector2.MoveTowards (transform.position, objectToFollow.position, speed.x * Time.fixedDeltaTime);
		}

		
		rigidbodyComponent.MovePosition(movement);
	}

}