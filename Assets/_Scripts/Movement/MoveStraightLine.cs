using UnityEngine;

public class MoveStraightLine : MoveScript {


    protected override void CalculateMovement() {
        if (!active)
			movement = new Vector2(0,0);
		else {
			movement = new Vector2(
				speed.x*direction.x,
				speed.y*direction.y);
		}
    }


    public override void setSpeed(Vector2 baseSpeed, float rotation) {
        speed = baseSpeed;
        direction = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation));
    }

}