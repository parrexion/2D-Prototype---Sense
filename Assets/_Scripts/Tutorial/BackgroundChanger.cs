using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour {

	public List<Sprite> tutorialBackgrounds;
	public List<Sprite> spiritBackgrounds;
	public List<Sprite> normalBackgrounds;

	public Button escapeBattleButton;

	public WeaponSlot weapons;
	public SpiritGridController gridController;
	public SpiritGrid spiritGrid;

	public SpriteRenderer tutorialSpirit;
	public SpriteRenderer tutorialNormal;

	public SpriteRenderer transformSpirit;
	public SpriteRenderer transformNormal;

	public Camera cameraNormal;
	public Camera cameraSpirit;
}
