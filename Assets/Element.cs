using UnityEngine;
using System.Collections;

public class Element : MonoBehaviour {

	public bool mine;

	// Different Textures
	public Sprite[] emptyTextures;
	public Sprite mineTexture;

	private int x = 0;
	private int y = 0;

	// Use this for initialization
	void Start () {
		mine = Random.value < 0.15;

		// Register in Grid
		x = (int)transform.position.x;
		y = (int)transform.position.y;
		Grid.elements[x, y] = this;
	}

	void OnMouseUpAsButton() {
		// It's a mine
		if (mine) {
			Grid.uncoverMines();
			// ...
			
			// game over
			print("you lose");
		}
		// It's not a mine
		else {
			Grid.searchForMines(x, y);
			Grid.checkWinCondition();
		}
	}

	public void loadTexture(int number)
	{
		if (mine)
			GetComponent<SpriteRenderer>().sprite = mineTexture;
		else
			GetComponent<SpriteRenderer>().sprite = emptyTextures[number];
	}

	// Is it still covered?
	public bool isCovered() {
		return GetComponent<SpriteRenderer>().sprite.texture.name == "Tile";
	}
}
