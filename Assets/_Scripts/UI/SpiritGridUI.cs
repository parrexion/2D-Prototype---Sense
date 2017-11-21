using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritGridUI : MonoBehaviour {

	public SpiritGrid grid;

	public Texture2D card;
	public Texture2D balanceMeter;
	private Texture2D target;

	public float balance_xpos = 0.5f;
	public float balance_ypos = 0.05f;
	public float balance_width = 0.1f;
	public float balance_height = 0.1f;

	public float grid_xpos = 0.5f;
	public float grid_ypos = 0.05f;
	public float grid_size = 0.1f;
	public float grid_xpos2 = 0.5f;

	// Use this for initialization
	void Start () {
		LoadSprites();
	}

	private void LoadSprites() {
		//cards = Resources.LoadAll<Sprite>("_Sprites/Players/spirit_icons_strip17");
		target = new Texture2D(1,1);
		target.SetPixel(0,0,Color.red);
		target.Apply();
	}


	void OnGUI() {

		if (grid.paused.value)
			return;

		Rect r = new Rect();
		Rect cut = new Rect();
		float gridSize = Screen.height*grid_size;

		//Draws the spirit grid and current position
		if (grid.attackDirection != BattleConstants.Direction.NEUTRAL) {
			
			//Draws the current position on the branch
			if (grid.attackDirection == BattleConstants.Direction.LEFT)
				r.Set(Screen.width*grid_xpos2-(grid.pos*gridSize*1.2f),Screen.height*grid_ypos+(grid.branch)*gridSize*1.2f,gridSize,gridSize);
			else if (grid.attackDirection == BattleConstants.Direction.RIGHT)
				r.Set(Screen.width*grid_xpos+(grid.pos*gridSize*1.2f),Screen.height*grid_ypos+grid.branch*gridSize*1.2f,gridSize,gridSize);
			GUI.DrawTexture(r,target);

			//Draws the grid
			for (int i = 0; i < BattleConstants.GRID_WIDTH; i++) {
				for (int j = 0; j < BattleConstants.GRID_BRANCH; j++) {

					if (grid.grid[j,i] == 0)
						continue;

					if (grid.attackDirection == BattleConstants.Direction.LEFT) {
						r.Set(Screen.width*grid_xpos2-(i*gridSize*1.2f),Screen.height*grid_ypos+(j)*gridSize*1.2f,gridSize,gridSize);
					}
					else if (grid.attackDirection == BattleConstants.Direction.RIGHT) {
						r.Set(Screen.width*grid_xpos+(i*gridSize*1.2f),Screen.height*grid_ypos+j*gridSize*1.2f,gridSize,gridSize);
					}
					cut.Set(grid.grid[j,i]/(float)BattleConstants.SPIRIT_IMAGES,0,1.0f/BattleConstants.SPIRIT_IMAGES,1);
					GUI.DrawTextureWithTexCoords(r,card,cut);
				}
			}
		}

		//Draws the current spiritual balance.
		r.Set(Screen.width*balance_xpos-Screen.width*balance_width*0.5f,Screen.height*balance_ypos,Screen.width*balance_width,Screen.height*balance_height);
		cut.Set((4.0f+grid.balance)/(float)BattleConstants.BALANCE_IMAGES, 0, 1.0f/BattleConstants.BALANCE_IMAGES, 1);
		GUI.DrawTextureWithTexCoords(r,balanceMeter,cut);
	}


}
