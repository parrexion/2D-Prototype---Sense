using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterHealthGUI : MonoBehaviour {
    
	private enum Side {TOP,BOTTOM};

	private bool active = true;
	private PlayerStats playerStats;

	public float bar_xpos = 0.04f;
	public float bar_ypos = 0.2f;
	public float bar_width = 0.1f;
	public float bar_height = 0.6f;
	public float bar_borderx = 0.05f;
	public float bar_bordery = 0.05f;

	public Text winText;

	[HideInInspector] public Rect healthRect;
	[HideInInspector] public Rect emptyRect;
	[HideInInspector] public Texture2D healthTexture;
	[HideInInspector] public Texture2D emptyTexture;
	
	// Use this for initialization
	void Start () {
		playerStats = PlayerStats.instance;

		healthRect = new Rect(Screen.width * bar_xpos, Screen.height * bar_ypos, Screen.width * bar_width, Screen.height * bar_height);
		emptyRect = new Rect(Screen.width*(bar_xpos-bar_borderx),Screen.height*(bar_ypos-bar_bordery),Screen.width*(bar_width+2*bar_borderx),Screen.height*(bar_height+2*bar_bordery));

        healthTexture = new Texture2D(1,1);
        healthTexture.SetPixel(0,0,Color.green);
        healthTexture.Apply();
        emptyTexture = new Texture2D(1,1);
        emptyTexture.SetPixel(0,0,Color.black);
        emptyTexture.Apply();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHealth();
	}

    void OnGUI() {
		if (!active)
			return;

        GUI.DrawTexture(emptyRect,emptyTexture);
        GUI.DrawTexture(healthRect,healthTexture);
    }

	public void SetActive(bool state) {
		active = state;
	}
	public void SetInvulnerable(bool state) {
		if (!playerStats)
			playerStats = PlayerStats.instance;
		playerStats.SetInvincible(state);
	}

    void UpdateHealth() {
		float ratioTop = (float)playerStats.spiritDamageTaken / (float)playerStats.maxHealth.GetValue();
		float ratioBottom = 1 - ((float)playerStats.normalDamageTaken / (float)playerStats.maxHealth.GetValue());
        
		healthRect.yMin = Screen.height * (ratioTop * bar_height + bar_ypos);
		healthRect.yMax = Mathf.Max(Screen.height * (ratioBottom * bar_height + bar_ypos), healthRect.yMin);
    }

}
