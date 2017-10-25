using System;

public static class BattleConstants {

	public enum Direction {NEUTRAL,LEFT,RIGHT,UP,DOWN};

	public static float NormalBorderWidth = 6.0f;
	public static float NormalBorderHeight = 3f;
	public static float NormalStartX = -5.0f;
	public static float NormalStartY = 0f;
	public static float SpiritBorderWidth = 3.5f;
	public static float SpiritBorderHeight = 2.25f;
	public static float SpiritStartX = 10.0f;
	public static float SpiritStartY = 0f;


	public static float cameraBorderWidth = 6.0f;
	public static float cameraBorderHeight = 2.9f;


	//Spirit grid
	public static int GRID_BRANCH = 5;
	public static int GRID_WIDTH = 7;
	public static float SPIRIT_SIZE = 32.0f;
	public static int SPIRIT_IMAGES = 18;
	public static int BALANCE_IMAGES = 9;


	//Enum for all the scenes
	public enum SCENE_INDEXES {
		STARTUP = 0,
		MAINMENU = 1,
		TUTORIAL = 2,
		DIALOGUE = 3,
		BATTLE = 4,
		SCORE = 5,
		INVENTORY = 6,
		BATTLETOWER = 7
	}

}

