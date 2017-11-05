using UnityEngine;

/// <summary>
/// Base class used by projectiles to define different ways for movement.
/// </summary>
public abstract class MoveScript : MonoBehaviour {

	public bool active = true;

    protected Vector2 speed;
    protected Vector2 direction;
    protected Vector2 movement;
    protected Rigidbody2D rigidbodyComponent;


    void Update() {
        CalculateMovement();
    }

    /// <summary>
    /// Implements how the projectile should move.
    /// </summary>
    protected abstract void CalculateMovement();

    /// <summary>
    /// Updates the movement every frame.
    /// </summary>
    void FixedUpdate() {

        if (rigidbodyComponent == null) 
			rigidbodyComponent = GetComponent<Rigidbody2D>();

		rigidbodyComponent.velocity = movement;
    }

    /// <summary>
    /// Sets the speed and direction for the projectile.
    /// </summary>
    /// <param name="baseSpeed"></param>
    /// <param name="rotation"></param>
    public abstract void setSpeed(Vector2 baseSpeed, float rotation);

}