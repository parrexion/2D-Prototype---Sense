﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritGridController : MonoBehaviour {

	public BoolVariable paused;
	public SpiritGrid grid;
	public BattleController battleController;
	[HideInInspector] public Rigidbody2D rigid;
	public float jumpForce = 400f;
	private float blockTime;
	private float currentBlockTime;
	public Collider2D ground;

	public Transform starProjectile;
	public Transform starEffect;
	public Transform lightningProjectile;
	public Transform lightningEffect;
	public Transform barrier;

	public float AttackTimeLimit = 2.0f;
	private float currentAttackTimeLimit;

	public AnimationScript animScript;
	private AnimationInformation animInfo;
	private int attacking = 0;
	public HurtablePlayerScript hurtScript;
	private int hurting = 0;


	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		animInfo = new AnimationInformation();
		blockTime = 0f;
	}

	void Update () {
		if (paused.value)
			return;
		
		UpdateInput();
		UpdateAnimation();
	}

	void UpdateInput() {
		if (grid.locked)
			return;

		if (attacking > 5)
			return;

		if (blockTime != 0) {
			currentBlockTime += Time.deltaTime;
			if (currentBlockTime >= blockTime) {
				blockTime = 0;
				hurtScript.canBeHurt = true;
			}
			return;
		}

		if (grid.attackDirection != Constants.Direction.NEUTRAL) {
			currentAttackTimeLimit += Time.deltaTime;
			if (currentAttackTimeLimit >= AttackTimeLimit) {
				grid.CancelGrid();
				return;
			}
		}
		else {
			currentAttackTimeLimit = 0;
		}

		int endReached = 0;
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (rigid.IsTouching(ground)) {
				rigid.AddForce(new Vector2(0f,jumpForce));
				grid.CancelGrid();
			}
		}
		else if (Input.GetKeyDown(KeyCode.W)) {
			// if (grid.attackDirection == BattleConstants.Direction.NEUTRAL) {
			// 	if (rigid.IsTouching(ground)) {
			// 		rigid.AddForce(new Vector2(0f,jumpForce));
			// 	}
			// }
			/*else*/ if (grid.MoveGrid(Constants.Direction.UP))
				endReached = 1;
			// else if (rigid.IsTouching(ground)) {
			// 	rigid.AddForce(new Vector2(0f,jumpForce));
			// 	grid.CancelGrid();
			// }
		}
		else if (Input.GetKeyDown(KeyCode.S)) {
			if (grid.attackDirection == Constants.Direction.NEUTRAL)
				endReached = 3;
			else if (grid.MoveGrid(Constants.Direction.DOWN))
				endReached = 1;
			else {
				endReached = 3;
				grid.CancelGrid();
			}
		}
		else if (Input.GetKeyDown(KeyCode.D)) {
			if (grid.attackDirection == Constants.Direction.LEFT) {
				endReached = 3;
				grid.CancelGrid();
			}
			else if (rigid.IsTouching(ground)) {
				if (battleController.enemyController.CheckIfEnemiesAtSide(false)) {
					currentAttackTimeLimit = 0;
					if (grid.MoveGrid(Constants.Direction.RIGHT))
						endReached = 2;
					else
						endReached = 1;
				}
				else if (grid.attackDirection == Constants.Direction.NEUTRAL) {
					//endReached = 3;
				}
				else {
					grid.CancelGrid();
				}
			}
		}
		else if (Input.GetKeyDown(KeyCode.A)) {
			if (grid.attackDirection == Constants.Direction.RIGHT) {
				endReached = 3;
				grid.CancelGrid();
			}
			else if (rigid.IsTouching(ground)) {
				if (battleController.enemyController.CheckIfEnemiesAtSide(true)) {
					currentAttackTimeLimit = 0;
					if (grid.MoveGrid(Constants.Direction.LEFT))
						endReached = 2;
					else
						endReached = 1;
				}
				else if (grid.attackDirection == Constants.Direction.NEUTRAL) {
					//endReached = 3;
				}
				else {
					grid.CancelGrid();
				}
			}
		}

		if (endReached == 1) {
			Attack();
			attacking = 10;
		}
		else if (endReached == 2) {
			EndAttack();
			attacking = 30;
		}
		else if (endReached == 3) {
			Block();
		}

	}

	public void UpdateAnimation() {

		animInfo.blocking = (blockTime != 0);
		animInfo.jumping = !rigid.IsTouching(ground);

		if (hurtScript.beenHurt) {
			hurtScript.beenHurt = false;
			hurting = 20;
		}

		if (hurting > 0 || grid.locked) {
			hurting--;
			animInfo.hurt = true;
		}
		else
			animInfo.hurt = false;

		if (attacking > 0) {
			animInfo.attacking = true;
			attacking--;

			if (grid.attackDirection == Constants.Direction.LEFT)
				animInfo.mouseDirection = -1;
			else if (grid.attackDirection == Constants.Direction.RIGHT)
				animInfo.mouseDirection = 1;
			else {
				animInfo.mouseDirection = 0;
//				Debug.Log("Neutral direction");
			}
			
		}
		else {
			animInfo.attacking = false;
			animInfo.mouseDirection = 0;
		}

//		Vector3 lastPosition = transform.position;
		animScript.UpdateState(animInfo);
//		transform.position = new Vector3(lastPosition.x, lastPosition.y+0.01f,lastPosition.z);
	}


	//Creates a basic attack at the given enemy
	public void Attack(){
		List<DamageValues> dmgs = new List<DamageValues>();
		if (grid.attackDirection == Constants.Direction.LEFT) {
			dmgs = battleController.enemyController.GetRandomEnemies(1,5,true,true);
		}
		else if (grid.attackDirection == Constants.Direction.RIGHT) {
			dmgs = battleController.enemyController.GetRandomEnemies(1,5,true,false);
		}

		foreach (DamageValues dv in dmgs) {
			
			if (dmgs == null)
				continue;

			Transform shotTransform = Instantiate(starProjectile) as Transform;
			shotTransform.GetComponent<Projectile>().damage = dv.GetDamage();
			shotTransform.position = dv.entityHit.position;

			shotTransform = Instantiate(starEffect) as Transform;
			float xpos = Random.Range(-0.35f,0.35f);
			float ypos = Random.Range(-0.35f,0.35f);
			shotTransform.position = new Vector3(dv.entityHit.position.x+xpos,dv.entityHit.position.y+ypos,0);

			shotTransform = Instantiate(starEffect) as Transform;
			xpos = Random.Range(-0.35f,0.35f);
			ypos = Random.Range(-0.35f,0.35f);
			shotTransform.position = new Vector3(dv.entityHit.position.x+xpos,dv.entityHit.position.y+ypos,0);
		}
	}

	//Creates a stronger attack when the end of a branch is reached.
	public void EndAttack(){
		List<DamageValues> dmgs = new List<DamageValues>();
		int combo = (int)(grid.combo);
		if (grid.lastDirection == Constants.Direction.LEFT) {
			dmgs = battleController.enemyController.GetRandomEnemies(combo,15,true,true);
		}
		else if (grid.lastDirection == Constants.Direction.RIGHT) {
			dmgs = battleController.enemyController.GetRandomEnemies(combo,15,true,false);
		}
//		Debug.Log("Combo: "+combo);

		foreach (DamageValues dv in dmgs) {
			Transform enemy = dv.entityHit;

			var shotTransform = Instantiate(lightningProjectile) as Transform;
			shotTransform.GetComponent<Projectile>().damage = dv.GetDamage();
			shotTransform.position = enemy.position;

			shotTransform = Instantiate(lightningEffect) as Transform;
			shotTransform.position = enemy.position;
		}
	}

	//Blocks all attacks for a moment
	public void Block(){
		var barrierTransform = Instantiate(barrier) as Transform;
		barrierTransform.SetParent(transform.parent);
		barrierTransform.localPosition = transform.localPosition;
		ProjectileEffect effect = barrierTransform.GetComponent<ProjectileEffect>();
		blockTime = effect.lifeTime;
		currentBlockTime = 0;
		hurtScript.canBeHurt = false;
	}
}
