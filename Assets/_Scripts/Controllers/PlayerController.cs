using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private enum Type { NORMAL,SPIRIT };

	public Camera screenCamera;
	private bool active = true;

	private float startX;
	private float startY;

	private MoveHomingScript moveToPosition;
	private Rigidbody2D rigidbodyComponent;
	private Collider2D coll2D;
    private MouseInformation mouseInfo;
	public WeaponSlot weapon;

	public AnimationScript animScript;
	private AnimationInformation animInfo;
	private int attacking = 0;
	public HurtablePlayerScript hurtScript;
	private int hurting = 0;


	// Use this for initialization
	void Start () {
		if (rigidbodyComponent == null)
			rigidbodyComponent = GetComponent<Rigidbody2D>();
		if (coll2D == null)
			coll2D = GetComponent<Collider2D>();
		if (moveToPosition == null)
			moveToPosition = GetComponent<MoveHomingScript>();

		mouseInfo = new MouseInformation();
		startX = transform.position.x;
		startY = transform.position.y;
		mouseInfo.playerPosition = transform.position;
		mouseInfo.holding = false;
		mouseInfo.holdDuration = -1;
		animInfo = new AnimationInformation();
	}
	
	// Update is called once per frame
	void Update () {

		if (active) {

			coll2D.enabled = (moveToPosition.GetDashPercent() > 0.25f);

			transform.position = new Vector3(
				Mathf.Clamp(transform.position.x,startX-BattleConstants.cameraBorderWidth,startX+BattleConstants.cameraBorderWidth),
				Mathf.Clamp(transform.position.y,startY-BattleConstants.cameraBorderHeight,startY+BattleConstants.cameraBorderHeight),
				0);
			mouseInfo.playerPosition = transform.position;
			Vector3 pos = screenCamera.ScreenToWorldPoint(Input.mousePosition);
			mouseInfo.mousePosition = pos;
			mouseInfo.clicked = false;

			if (moveToPosition.dashing)
				return;

			if (Input.GetMouseButtonDown(0)) {
				mouseInfo.holding = true;
				mouseInfo.holdDuration = 0;
				mouseInfo.position1 = screenCamera.ScreenToWorldPoint(Input.mousePosition);
				//Debug.Log("Screen: "+Input.mousePosition.x+" , "+(1024-Input.mousePosition.y));
			}

			if (mouseInfo.holding) {
				mouseInfo.position2 = screenCamera.ScreenToWorldPoint(Input.mousePosition);
				mouseInfo.holdDuration += Time.deltaTime;
//			Debug.Log("duration:  "+mouseInfo.holdDuration);
			}
        
			if (Input.GetMouseButtonUp(0)) {
				mouseInfo.holding = false;
				mouseInfo.setPosition2(pos.x,pos.y);
				mouseInfo.clicked = true;
			}

			if (weapon.Activate(mouseInfo)) {
				attacking = 20;
			}

			if (Input.GetMouseButtonDown(1) && !mouseInfo.holding) {
				if (Input.GetKey(KeyCode.LeftShift)) {
					moveToPosition.moveToPosition = pos;
					moveToPosition.startDash();
				}
				else if (!moveToPosition.dashing)
					moveToPosition.moveToPosition = pos;
			}

			UpdateAnimation();
		}
    }

	public void SetActive(bool state) {
		active = state;
		moveToPosition.active = state;
		weapon.SetVisible(state);
	}


	public void UpdateAnimation() {

		if (hurtScript.beenHurt) {
			hurtScript.beenHurt = false;
			hurting = 20;
		}

		if (hurting > 0) {
			hurting--;
			animInfo.hurt = true;
		}
		else
			animInfo.hurt = false;

		animInfo.dashing = moveToPosition.dashing;

		if (attacking > 0) {
			animInfo.attacking = true;
			animInfo.blocking = false;
			attacking--;
			if (mouseInfo.position1.x < transform.position.x)
				animInfo.mouseDirection = -1;
			else
				animInfo.mouseDirection = 1;
		}
		else if (mouseInfo.holding && mouseInfo.holdDuration > 0.25f) {
//			Debug.Log("Holding:  "+ mouseInfo.holding + ", Holding duration:  "+mouseInfo.holdDuration);
			animInfo.attacking = false;
			animInfo.blocking = true;
			if (mouseInfo.position1.x < transform.position.x)
				animInfo.mouseDirection = -1;
			else
				animInfo.mouseDirection = 1;
		}
		else {
			animInfo.attacking = false;
			animInfo.blocking = false;
			animInfo.mouseDirection = moveToPosition.moveDirection;
		}
		animScript.UpdateState(animInfo);
	}
}
