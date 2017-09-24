using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateController : MonoBehaviour {

	public enum WaitStates {MOVE,CHASE,FLEE,RANGE}
	public WaitStates currentWaitState;

	public int enemyid;
	public State currentState;
	public bool aiActive;
	public State remainState;
	public Transform nPlayer;
	public Transform sPlayer;
	public Transform thisTransform;
	public Rigidbody2D rigidBody;
	public Vector2 startPosition;

	[HideInInspector] public float stateTimeElapsed = 0;

	//Most of the stats
	public EnemyValues values;
	//

	//Attacking
	[HideInInspector] public AttackScript attack;
	public bool hasAttacked = false;
	//

	//Waiting
	public bool finishedWaiting = false;
	public float waitTime = 0;
	//

	//Animation
	public AnimationScript animScript;
	public AnimationInformation animInfo;


	/// /////////////////////////////////////////////////////

	void Start(){
		if (values == null) {
			Debug.LogError("No enemy values could be found");
		}

		GameObject go = GameObject.Find("Player Normal");
		if (go != null)
			nPlayer = go.transform;
		go = GameObject.Find("Player Spirit");
		if (go != null)
			sPlayer = go.transform;

		thisTransform = GetComponent<Transform>();
		rigidBody = GetComponent<Rigidbody2D>();
		attack = GetComponent<AttackScript>();

		animInfo = new AnimationInformation();

		waitTime = 0;
		startPosition = thisTransform.position;
		currentWaitState = WaitStates.MOVE;
	}

	// Update is called once per frame
	void Update () {
		if (!aiActive)
			return;

		stateTimeElapsed += Time.deltaTime;
		currentState.UpdateState(this);
		UpdateAnimation();
	}

	public void TransitionToState(State nextState) {
		if (nextState != remainState) {
			currentState = nextState;
			stateTimeElapsed = 0;
			waitTime = 0;
			hasAttacked = false;
			OnExitState();
		}
	}

	protected abstract void OnExitState();

	protected abstract void UpdateAnimation();

	private void OnDrawGizmos() {
		if (currentState != null) {
			Gizmos.color = currentState.sceneGizmoColor;
			Gizmos.DrawWireSphere(transform.position,1.0f);
		}
	}

	public WaitStates GetRandomWaitState(){
		int select = Random.Range(0,values.waitStates.Count);
		return values.waitStates[select];
	}

	public abstract Vector3 GetRandomLocation();

}
