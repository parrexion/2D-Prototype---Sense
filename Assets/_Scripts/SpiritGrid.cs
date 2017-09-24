using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritGrid : MonoBehaviour {

	public bool active;
	public int[,] grid;
	public int branch;
	public int pos;
	public int balance;
	private int tempBalance;
	public float combo;
	public bool locked;
	public BattleConstants.Direction attackDirection;
	public BattleConstants.Direction lastDirection;

	// Use this for initialization
	void Start () {

		grid = new int[BattleConstants.GRID_BRANCH,BattleConstants.GRID_WIDTH];

		branch = 2;
		pos = 0;
		balance = 0;
		tempBalance = 0;
		combo = 0.6f;
		locked = false;
		attackDirection = BattleConstants.Direction.NEUTRAL;
		lastDirection = BattleConstants.Direction.NEUTRAL;
	}

	private void GenerateGrid(BattleConstants.Direction dir) {

		for (int i = 0; i < BattleConstants.GRID_WIDTH; i++) {
			for (int j = 0; j < BattleConstants.GRID_BRANCH; j++) {
				grid[j,i] = 0;
			}
		}

		NormalGrid(dir,2,2,1,2);
	}

	/*
	 * Generates the spirit grid with the following values
	 * 
	 * Dir					Which direction the grid is travelling in (left / right)
	 * diff_low				lowest number of arrows before the split
	 * diff_high			highest number of arrows before the split
	 * branch_width_low		lowest number of arrows after the split
	 * branch_width_high	highest number of arrows after the split
	 */
	private void NormalGrid(BattleConstants.Direction dir,int diff_low, int diff_high, int branch_width_low, int branch_width_high) {
		int a, b, c, diff;

		pos = 0;
		branch = 2;

		diff = Random.Range(diff_low, diff_high+1);
		a = 1+diff+Random.Range(branch_width_low, branch_width_high);
		b = 1+diff+Random.Range(branch_width_low, branch_width_high+1);
		c = 1+diff+Random.Range(branch_width_low, branch_width_high);

		int arrow = 2;
		if (dir == BattleConstants.Direction.LEFT)
			arrow = 1;

		for (int i = 0; i < BattleConstants.GRID_WIDTH; i++) {

			if (i < diff) {
				grid[2, i] = arrow;
			}
			else if (i == diff) {
				grid[1, i] = 3;
				grid[2, i] = arrow;
				grid[3, i] = 4;
			}
			else {
				if (i == b)
					grid[2, i] = GetEndpoint();
				else if (i < b)
					grid[2, i] = arrow;

				if (i == a)
					grid[1, i] = GetEndpoint();
				else if (i < a)
					grid[1, i] = arrow;

				if (i == c)
					grid[3, i] = GetEndpoint();
				else if (i < c)
					grid[3, i] = arrow;
			}
		}
		grid[2,0] += 4;
	}

	private int GetEndpoint(){
		if (combo < 2.1f)
			return Random.Range(11, 13);
		else if (combo < 3.1f)
			return Random.Range(11, 15);
		else if (combo < 4.1f)
			return Random.Range(11, 17);
		else 
			return Random.Range(13, 17);
	}
		

	public bool MoveGrid(BattleConstants.Direction dir){

		if (dir == BattleConstants.Direction.RIGHT) {

			if (attackDirection == BattleConstants.Direction.NEUTRAL) {
				attackDirection = BattleConstants.Direction.RIGHT;
				GenerateGrid(attackDirection);
			}

			if (attackDirection != BattleConstants.Direction.RIGHT) {
				return false;
			}

			if (grid[branch,pos+1] == 0) {
				return false;
			}
			else if (grid[branch,pos+1] == 2) {
				if (grid[branch-1,pos] == 3)
					cancelBranches(branch);
				else if (grid[branch+1,pos] == 4)
					cancelBranches(branch);

				grid[branch,pos] += 2;
				grid[branch,pos+1] += 4;
				pos++;
				return false;
			}
			else {
				endReached(grid[branch,pos+1]);
				return true;
			}
		}
		else if (dir == BattleConstants.Direction.LEFT) {

			if (attackDirection == BattleConstants.Direction.NEUTRAL) {
				attackDirection = BattleConstants.Direction.LEFT;
				GenerateGrid(attackDirection);
			}

			if (attackDirection != BattleConstants.Direction.LEFT) {
				return false;
			}

			if (grid[branch,pos+1] == 0) {
				return false;
			}
			else if (grid[branch,pos+1] == 1) {
				if (grid[branch-1,pos] == 3)
					cancelBranches(branch);
				else if (grid[branch+1,pos] == 4)
					cancelBranches(branch);

				grid[branch,pos] += 2;
				grid[branch,pos+1] += 4;
				pos++;
				return false;
			}
			else {
				endReached(grid[branch,pos+1]);
				return true;
			}
		}
		else if (dir == BattleConstants.Direction.UP) {
			if (grid[branch-1,pos] == 3) {
				grid[branch,pos] += 2;
				grid[branch-1,pos] += 6;
				grid[branch-1,pos+1] += 4;
				pos++;
				branch--;
				cancelBranches(branch);
				return true;
			}
			else {
				return false;
			}
		}
		else if (dir == BattleConstants.Direction.DOWN) {
			if (grid[branch+1,pos] == 4) {
				grid[branch,pos] += 2;
				grid[branch+1,pos] += 6;
				grid[branch+1,pos+1] += 4;
				pos++;
				branch++;
				cancelBranches(branch);
				return true;
			}
			else {
				return false;
			}
		}

		Debug.LogWarning("Wrong Argument");
		return false;
	}


	public void CancelGrid(){
		attackDirection = BattleConstants.Direction.NEUTRAL;
		branch = 2;
		pos = 0;
	}

	private void endReached(int value) {
		lastDirection = attackDirection;
		combo += 0.5f;
		CancelGrid();

		if (value == 11) {
			balance = Mathf.Min(4,balance+1);
		}
		else if (value == 12) {
			balance = Mathf.Max(-4,balance-1);
		}
		else if (value == 13) {
			balance = Mathf.Min(4,balance+2);
		}
		else if (value == 14) {
			balance = Mathf.Max(-4,balance-2);
		}
		else if (value == 15) {
			balance = Mathf.Min(4,balance+3);
		}
		else if (value == 16) {
			balance = Mathf.Max(-4,balance-3);
		}

		if (Mathf.Abs(balance) == 4) {
			combo = 0.6f;
			StartCoroutine(DestroyedCombo());
		}

	}


	private void cancelBranches(int currentBranch) {
		for (int i = 0; i < BattleConstants.GRID_BRANCH; i++) {
			if (i == currentBranch)
				continue;

			for (int j = 0; j < BattleConstants.GRID_WIDTH; j++) {
				if (grid[i,j] != 0 && grid[i,j] < 5)
					grid[i,j] += 6;
			}
		}
	}

	private IEnumerator DestroyedCombo(){
		tempBalance = 0;
		locked = true;
		for (int i = 0; i < 8; i++) {
			yield return new WaitForSeconds(0.3f);
			int temp = tempBalance;
			tempBalance = balance;
			balance = temp;
		}
		balance = 0;
		locked = false;
	}
}
