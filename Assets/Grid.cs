using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public static int width = 10;
	public static int height = 7;

	public static Element[,] elements = new Element[width,height];

	// Use this for initialization
	void Start () {
	
	}

	public static void checkWinCondition()
	{
		foreach (Element element in elements) {
			if (!element.mine && element.isCovered())
			{
				return;
			}
		}

		print ("you win");
	}

	public static void uncoverMines()
	{
		foreach (Element element in elements) {
			if (element.mine)
			{
				element.loadTexture(0);
			}
		}
	}

	public static void searchForMines(int x, int y)
	{
		Element element = getElement (x, y);
		if (element == null || (element != null && !element.isCovered()) || checkForMines (x, y) != 0) {
			return;
		}

		// Otherwise we recurse around all areas
		searchForMines (x, y + 1);
		searchForMines (x - 1, y);
		searchForMines (x + 1, y);
		searchForMines (x, y - 1);
	}

	public static int checkForMines(int x, int y)
	{
		int numMines = 0;
		if (mineExistsInCoords (x - 1, y + 1)) { numMines++; }
		if (mineExistsInCoords (x, y + 1)) { numMines++; }
		if (mineExistsInCoords (x + 1, y + 1)) { numMines++; }
		if (mineExistsInCoords (x - 1, y)) { numMines++; }
		if (mineExistsInCoords (x + 1, y)) { numMines++; }
		if (mineExistsInCoords (x - 1, y - 1)) { numMines++; }
		if (mineExistsInCoords (x, y - 1)) { numMines++; }
		if (mineExistsInCoords (x + 1, y - 1)) { numMines++; }

		Element element = getElement (x, y);
		if (element != null) {
			elements [x, y].loadTexture (numMines);
		}

		return numMines;
	}

	public static bool mineExistsInCoords(int x, int y)
	{
		Element element = getElement (x, y);
		if (element != null) {
			return elements [x, y].mine;
		}

		return false;
	}

	public static Element getElement(int x, int y)
	{
		if (x < 0 || x >= width) {
			return null;
		} else if (y < 0 || y >= height) {
			return null;
		}

		return elements [x, y];
	}
}
