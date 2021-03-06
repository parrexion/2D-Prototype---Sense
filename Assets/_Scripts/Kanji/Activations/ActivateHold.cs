﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kanji/Activations/Hold")]
public class ActivateHold : KanjiActivation {

	public bool continuous = false;

    public override bool CanActivate(KanjiValues values, MouseInformation info) {
        
		if (info.holdDuration < values.holdMin || !info.holding)
			return false;

		if (!continuous)
			info.holdDuration = 0;

//		float dist = info.GetInternalDistance();
//		if (dist > area) {
//			if (running) {
//				running = false;
//				base.reduceCharge(1.0f);
//				info.holdDuration = 0;
//			}
//			return false;
//		}

		return true;
    }
}
