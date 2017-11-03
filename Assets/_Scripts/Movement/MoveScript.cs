using UnityEngine;

public abstract class MoveScript : MonoBehaviour {

	public bool active = true;

    protected Vector2 speed;
    protected Vector2 direction;
    protected Vector2 movement;
    protected Rigidbody2D rigidbodyComponent;


    void Update() {
        CalculateMovement();
    }

    protected abstract void CalculateMovement();

    void FixedUpdate() {

        if (rigidbodyComponent == null) 
			rigidbodyComponent = GetComponent<Rigidbody2D>();

		rigidbodyComponent.velocity = movement;
    }

    public abstract void setSpeed(Vector2 baseSpeed, float rotation);

}