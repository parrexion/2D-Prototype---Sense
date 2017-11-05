using UnityEngine;
using System.Collections;

/// <summary>
/// Class for the controller for the normal player during battles which handles 
/// the input and triggers both moving and attacking using that.
/// </summary>
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
	public HurtablePlayerScript hurtScript;
	private int hurting = 0;

	const int delayPlayerHurt = 20;
	const float delayUntilCharging = 0.25f;
	const float invulFramesForDash = 0.25f;

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

		if (!active)
			return;

		coll2D.enabled = (moveToPosition.GetDashPercent() > invulFramesForDash);

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
		}

		if (mouseInfo.holding) {
			mouseInfo.position2 = screenCamera.ScreenToWorldPoint(Input.mousePosition);
			mouseInfo.holdDuration += Time.deltaTime;
		}
	
		if (Input.GetMouseButtonUp(0)) {
			mouseInfo.holding = false;
			mouseInfo.setPosition2(pos.x,pos.y);
			mouseInfo.clicked = true;
		}

		weapon.Activate(mouseInfo);

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

	/// <summary>
	/// Sets the player controller's active state.
	/// </summary>
	/// <param name="state"></param>
	public void SetActive(bool state) {
		active = state;
		moveToPosition.active = state;
		weapon.SetVisible(state);
	}

	/// <summary>
	/// Updates the current information from the controller's state and sends it to the animator.
	/// </summary>
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

		if (weapon.IsAttacking()) {
			animInfo.attacking = true;
			animInfo.blocking = false;
			if (mouseInfo.position1.x < transform.position.x)
				animInfo.mouseDirection = -1;
			else
				animInfo.mouseDirection = 1;
		}
		else if (mouseInfo.holding && mouseInfo.holdDuration > delayUntilCharging) {
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
