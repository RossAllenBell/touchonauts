using UnityEngine;

public class Arcade : Scene {
	
	public const int SCORE_PER_ITEM = 50;
	public const int SCORE_PER_ASTEROID = 10;
	
	private Rect totalScoreRect;
	private Rect[] shipScoreRects;
	
	public GUIStyle totalStyle;
	public GUIStyle shipScoreStyle;
	public Color[] shipScoreColors;
	
	private int shipCount;
	private int asteroids;
	private bool items;
	
	private int[] shipScores;
	private GameObject[] ships;
	
	public Arcade(int shipCount, int asteroids, bool items) {
		this.shipCount = shipCount;
		this.asteroids = asteroids;
		this.items = items;
		
		totalScoreRect = new Rect(10, 10, Main.NATIVE_WIDTH - 60, 300);
		shipScoreRects = new Rect[3];
		shipScoreRects[0] = new Rect(0, 10, Main.NATIVE_WIDTH / 2f, 300);
		shipScoreRects[1] = new Rect(0, 10, Main.NATIVE_WIDTH, 300);
		shipScoreRects[2] = new Rect(Main.NATIVE_WIDTH / 2f, 10, Main.NATIVE_WIDTH / 2f, 300);
		
		totalStyle = new GUIStyle();
		totalStyle.fontSize = (int) (100 * Main.GUI_RATIO_WIDTH);
		totalStyle.normal.textColor = Color.white;
		totalStyle.alignment = TextAnchor.UpperLeft;
		
		shipScoreStyle = new GUIStyle();
		shipScoreStyle.fontSize = (int) (75 * Main.GUI_RATIO_WIDTH);
		shipScoreStyle.alignment = TextAnchor.UpperCenter;
		
		shipScoreColors = new Color[3];
		shipScoreColors[0] = Color.blue;
		shipScoreColors[1] = Color.red;
		shipScoreColors[2] = Color.yellow;
		
		shipScores = new int[] {0,0,0};
		ships = new GameObject[3];
	}
	
	public override bool ControlShip {get{return true;}}
	
	public override bool ShipCollision {get{return true;}}
	
	public override bool AsteroidCollision {get{return true;}}
	
	public override int AsteroidCount {get{return asteroids;}}
	
	public override bool AsteroidItems {get{return items;}}
	
	public override void Begin() {
		base.Begin();
		
		ships[0] = Main.SpawnShip(Main.BOARD_CENTER);
		if (shipCount >= 2) {
			ships[1] = Main.SpawnShip(Main.BOARD_CENTER - new Vector2(Main.BOARD_WIDTH / 4f, 0f));
		}
		if (shipCount >= 3) {
			ships[2] = Main.SpawnShip(Main.BOARD_CENTER + new Vector2(Main.BOARD_WIDTH / 4f, 0f));
		}
	}
	
	public override void OnGUI() {		
		if (Main.ShipCount() < 1) {
			RestartLevel.OnGUI();
		}
		
		Utils.DrawOutline(totalScoreRect, (shipScores[0] + shipScores[1] + shipScores[2]).ToString(), totalStyle, 3, totalStyle.normal.textColor, Color.black);
		
		if (shipCount > 1) {
			for (int i=0; i<shipCount; i++) {
				Utils.DrawOutline(shipScoreRects[i], shipScores[i].ToString(), shipScoreStyle, 2, shipScoreColors[i], Color.white);
			}
		}
	}
	
	public override void Update() {
		if (Main.ShipCount() < 1) {
			if (Main.Clicked) {
				Sounds.Click();
				Main.ChangeScenes(new ArcadeSetup(shipCount, asteroids, items));
			}
		}
	}
	
	public override void ItemGained(GameObject item) {
		int itemType = item.GetComponent<Item>().itemType;
		if (shipScores[itemType] > 0) {
			shipScores[itemType] += SCORE_PER_ITEM;
		}
	}
	
	public override void AsteroidCollisionEvent(GameObject asteroid1, GameObject asteroid2) {
		for (int i=0; i<shipCount; i++) {
			if (ships[i] != null) {
				shipScores[i] += SCORE_PER_ASTEROID;
			}
		}
	}
	
}


