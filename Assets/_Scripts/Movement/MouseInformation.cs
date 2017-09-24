using UnityEngine;
using System.Collections;

public class MouseInformation {
    
	public Vector2 mousePosition;
	public Vector2 position1;
	public Vector2 position2;

    public float distX;
    public float distY;

	public float rotationInternal;
	public float rotationPlayer;

	public float holdDuration;
	public bool holding;
	public bool clicked;

    public Vector2 playerPosition;
    

    public void setPosition2(float x, float y) {
        position2.Set(x, y);

        distX = position2.x - position1.x;
        distY = position2.y - position1.y;

		rotationPlayer = Mathf.Atan2(
            position2.y - playerPosition.y,
            position2.x - playerPosition.x);

		rotationInternal = Mathf.Atan2(
			position2.y - position1.y,
			position2.x - position1.x);
        
//      Debug.Log("Playerpos:  " + playerPosition.x + "," + playerPosition.y);
//      Debug.Log("Mousepos:  " + position2.x + "," + position2.y);
//
//		Debug.Log("Rotation: " + rotationInternal);
	}

	public float GetPlayerPos1Distance(){
		return Vector2.Distance (playerPosition, position1);
	}
    
	public float GetPlayerPos2Distance(){
		return Vector2.Distance (playerPosition, position2);
	}

	public float GetInternalDistance(){
		return Vector2.Distance (position1, position2);
	}

}
