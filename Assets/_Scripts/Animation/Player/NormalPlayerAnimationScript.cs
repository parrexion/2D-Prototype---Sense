using System.Collections;
using UnityEngine;

/// <summary>
/// Implementation of the animation script used as the default animation for the characters
/// in the game.
/// </summary>
public class NormalPlayerAnimationScript : AnimationScript {

	enum State { IDLE, WALK_LEFT, WALK_RIGHT, ATTACK }


	public override void UpdateState(AnimationInformation info) {

		if (info.hurt) {
			animator.Play(kHurtAnim);
		}
		else if (info.blocking) {
			animator.Play(kBlockAnim);
			if (info.mouseDirection != 0)
				Face(info.mouseDirection);
		}
		else if (info.dashing) {
			animator.Play(kDashAnim);
			if (info.mouseDirection != 0)
				Face(info.mouseDirection);
		}
		else if (info.jumping) {
			if (info.mouseDirection == -1)
				animator.Play(kWalkLAnim);
			else
				animator.Play(kWalkRAnim);
		}
		else if (info.chasing) {
			animator.Play(kChaseAnim);
			if (info.mouseDirection != 0)
				Face(info.mouseDirection);
		}
		else if (info.attacking) {
			animator.Play(kAttackAnim);
			if (info.mouseDirection != 0)
				Face(info.mouseDirection);
		}
		else if (info.mouseDirection == -1) {
			animator.Play(kWalkLAnim);
			Face(info.mouseDirection);
		}
		else if (info.mouseDirection == 1) {
			animator.Play(kWalkRAnim);
			Face(info.mouseDirection);
		}
		else {
			animator.Play(kIdleAnim);
		}

	}

}
