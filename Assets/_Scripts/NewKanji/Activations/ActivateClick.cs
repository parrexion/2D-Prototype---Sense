using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kanji/Activations/Click")]
public class ActivateClick : KanjiActivation {

    public override bool CanActivate(KanjiValues values, MouseInformation info) {
        
		if (info.holding || info.holdDuration > values.holdMax)
			return false;

		if (!info.clicked)
			return false;
		
		float dist = info.GetInternalDistance();
		if (dist > values.area) {
			return false;
		}

		return true;
    }
}
