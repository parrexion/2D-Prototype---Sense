using UnityEngine;

public class MoveScript : MonoBehaviour {

	public bool active = true;

    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;



    void Update() {
		if (!active)
			movement = new Vector2(0,0);
		else {
			movement = new Vector2(
				speed.x*direction.x,
				speed.y*direction.y);
		}
    }

    void FixedUpdate() {

        if (rigidbodyComponent == null) 
			rigidbodyComponent = GetComponent<Rigidbody2D>();

		rigidbodyComponent.velocity = movement;
    }

    public void setSpeedFromRotation(float rotation) {
        direction = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation));
    }

}