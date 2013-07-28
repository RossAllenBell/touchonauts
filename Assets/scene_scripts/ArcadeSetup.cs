using UnityEngine;

public class ArcadeSetup : Scene {	
	private readonly Rect TITLE_RECT;
	private readonly GUIStyle TITLE_STYLE;
	
	private int shipCount;
	private int asteroidCount;
	private bool items;
	
	private readonly GUIStyle LABEL_STYLE;
	private readonly GUIStyle COUNT_STYLE;
	
	private readonly Rect SHIPS_RECT;
	private readonly Rect SHIP_UP_RECT;
	private readonly Rect SHIP_DOWN_RECT;
	private readonly Rect SHIP_COUNT_RECT;
	
	private readonly Rect ASTEROIDS_RECT;
	private readonly Rect ASTEROID_UP_RECT;
	private readonly Rect ASTEROID_DOWN_RECT;
	private readonly Rect ASTEROID_COUNT_RECT;
	
	private readonly Rect ITEMS_RECT;
	private readonly Rect ITEM_CHECK_RECT;
	
	private readonly Rect GO_RECT;
	
	private readonly Texture2D buttonTexture;
	private readonly Texture2D upTexture;
	private readonly Texture2D downTexture;
	private readonly Texture2D checkedTexture;
	private readonly Texture2D uncheckedTexture;
	
	private readonly GUIStyle BACK_STYLE;
	private readonly Rect BACK_RECT;
	
	public ArcadeSetup() : this(1, 10, true) {}
		
	public ArcadeSetup(int shipCount, int asteroidCount, bool items) {
		TITLE_RECT = new Rect(10, 10, Main.NATIVE_WIDTH - 20, Main.NATIVE_HEIGHT / 3);
		TITLE_STYLE = new GUIStyle();
		TITLE_STYLE.fontSize = (int) (150 * Main.GUI_RATIO_WIDTH);
		TITLE_STYLE.normal.textColor = Color.red;
		TITLE_STYLE.alignment = TextAnchor.MiddleCenter;
		
		LABEL_STYLE = new GUIStyle();
		LABEL_STYLE.fontSize = (int) (100 * Main.GUI_RATIO_WIDTH);
		LABEL_STYLE.normal.textColor = Color.white;
		LABEL_STYLE.alignment = TextAnchor.LowerCenter;
		
		COUNT_STYLE = new GUIStyle();
		COUNT_STYLE.fontSize = (int) (100 * Main.GUI_RATIO_WIDTH);
		COUNT_STYLE.normal.textColor = Color.white;
		COUNT_STYLE.alignment = TextAnchor.MiddleCenter;
		
		this.shipCount = shipCount;
		this.asteroidCount = asteroidCount;
		this.items = items;
		
		float LABEL_WIDTH = Main.NATIVE_WIDTH / 4f;
		float LABEL_HEIGHT = Main.NATIVE_WIDTH / 8f;
		float BUTTON_WIDTH = Main.NATIVE_WIDTH / 12f;
		float PADDING = 5f;
		
		SHIPS_RECT = new Rect((Main.NATIVE_WIDTH / 4f) - (LABEL_WIDTH * Main.GUI_RATIO * 0.5f), (Main.NATIVE_HEIGHT * .80f) - ((BUTTON_WIDTH + LABEL_HEIGHT) * Main.GUI_RATIO) - (PADDING * Main.GUI_RATIO * 2f), LABEL_WIDTH * Main.GUI_RATIO, LABEL_HEIGHT * Main.GUI_RATIO);
		SHIP_UP_RECT = new Rect((Main.NATIVE_WIDTH / 4f) - (BUTTON_WIDTH * Main.GUI_RATIO * 0.5f), (Main.NATIVE_HEIGHT * .80f) - (BUTTON_WIDTH * Main.GUI_RATIO) - (PADDING * Main.GUI_RATIO), BUTTON_WIDTH * Main.GUI_RATIO, BUTTON_WIDTH * Main.GUI_RATIO);
		SHIP_DOWN_RECT = new Rect((Main.NATIVE_WIDTH / 4f) - (BUTTON_WIDTH * Main.GUI_RATIO * 0.5f), (Main.NATIVE_HEIGHT * .80f) + (PADDING * Main.GUI_RATIO), BUTTON_WIDTH * Main.GUI_RATIO, BUTTON_WIDTH * Main.GUI_RATIO);
		SHIP_COUNT_RECT = new Rect((Main.NATIVE_WIDTH / 4f) + (BUTTON_WIDTH * Main.GUI_RATIO * 0.5f), (Main.NATIVE_HEIGHT * .80f) - (BUTTON_WIDTH * Main.GUI_RATIO * 0.5f), BUTTON_WIDTH * Main.GUI_RATIO, BUTTON_WIDTH * Main.GUI_RATIO);
		
		ASTEROIDS_RECT = new Rect((Main.NATIVE_WIDTH / 2f) - (LABEL_WIDTH * Main.GUI_RATIO * 0.5f), (Main.NATIVE_HEIGHT * .80f) - ((BUTTON_WIDTH + LABEL_HEIGHT) * Main.GUI_RATIO) - (PADDING * Main.GUI_RATIO * 2f), LABEL_WIDTH * Main.GUI_RATIO, LABEL_HEIGHT * Main.GUI_RATIO);
		ASTEROID_UP_RECT = new Rect((Main.NATIVE_WIDTH / 2f) - (BUTTON_WIDTH * Main.GUI_RATIO * 0.5f), (Main.NATIVE_HEIGHT * .80f) - (BUTTON_WIDTH * Main.GUI_RATIO) - (PADDING * Main.GUI_RATIO), BUTTON_WIDTH * Main.GUI_RATIO, BUTTON_WIDTH * Main.GUI_RATIO);
		ASTEROID_DOWN_RECT = new Rect((Main.NATIVE_WIDTH / 2f) - (BUTTON_WIDTH * Main.GUI_RATIO * 0.5f), (Main.NATIVE_HEIGHT * .80f) + (PADDING * Main.GUI_RATIO), BUTTON_WIDTH * Main.GUI_RATIO, BUTTON_WIDTH * Main.GUI_RATIO);
		ASTEROID_COUNT_RECT = new Rect((Main.NATIVE_WIDTH / 2f) + (BUTTON_WIDTH * Main.GUI_RATIO * 0.5f), (Main.NATIVE_HEIGHT * .80f) - (BUTTON_WIDTH * Main.GUI_RATIO * 0.5f), BUTTON_WIDTH * Main.GUI_RATIO, BUTTON_WIDTH * Main.GUI_RATIO);
		
		ITEMS_RECT = new Rect((Main.NATIVE_WIDTH * .75f) - (LABEL_WIDTH * Main.GUI_RATIO * 0.5f), (Main.NATIVE_HEIGHT * .80f) - ((BUTTON_WIDTH + LABEL_HEIGHT) * Main.GUI_RATIO) - (PADDING * Main.GUI_RATIO * 2f), LABEL_WIDTH * Main.GUI_RATIO, LABEL_HEIGHT * Main.GUI_RATIO);
		ITEM_CHECK_RECT = new Rect(((Main.NATIVE_WIDTH / 4f) * 3f) - (BUTTON_WIDTH * Main.GUI_RATIO * 0.5f), (Main.NATIVE_HEIGHT * .80f) - (BUTTON_WIDTH * Main.GUI_RATIO * 0.5f), BUTTON_WIDTH * Main.GUI_RATIO, BUTTON_WIDTH * Main.GUI_RATIO);
		
		GO_RECT = new Rect((Main.NATIVE_WIDTH / 2f) - (200 * Main.GUI_RATIO), Main.NATIVE_HEIGHT / 3f, 400 * Main.GUI_RATIO, 150 * Main.GUI_RATIO);
		
		buttonTexture = Resources.Load("ui/button") as Texture2D;
		upTexture = Resources.Load("ui/up") as Texture2D;
		downTexture = Resources.Load("ui/down") as Texture2D;
		checkedTexture = Resources.Load("ui/checked") as Texture2D;
		uncheckedTexture = Resources.Load("ui/unchecked") as Texture2D;
		
		BACK_STYLE = new GUIStyle();
		BACK_STYLE.fontSize = (int) (75 * Main.GUI_RATIO_WIDTH);
		BACK_STYLE.normal.textColor = Color.white;
		BACK_STYLE.alignment = TextAnchor.MiddleCenter;
		
		BACK_RECT = new Rect(10, Main.NATIVE_HEIGHT - ((150 * Main.GUI_RATIO) + 10), 250 * Main.GUI_RATIO, 150 * Main.GUI_RATIO);
	}
	
	public override bool AsteroidCollision {get{return true;}}
	
	public override int AsteroidCount {get{return asteroidCount;}}
	
	public override void OnGUI() {
		Utils.DrawOutline(TITLE_RECT, "Freestyle Mode Setup", TITLE_STYLE, 2, TITLE_STYLE.normal.textColor, Color.white);
		
		Utils.DrawOutline(SHIPS_RECT, "Ships", LABEL_STYLE, 2, LABEL_STYLE.normal.textColor, Color.black);
		GUI.DrawTexture(SHIP_UP_RECT, upTexture);
		GUI.DrawTexture(SHIP_DOWN_RECT, downTexture);
		Utils.DrawOutline(SHIP_COUNT_RECT, shipCount.ToString(), COUNT_STYLE, 2, COUNT_STYLE.normal.textColor, Color.black);
		
		Utils.DrawOutline(ASTEROIDS_RECT, "Asteroids", LABEL_STYLE, 2, LABEL_STYLE.normal.textColor, Color.black);
		GUI.DrawTexture(ASTEROID_UP_RECT, upTexture);
		GUI.DrawTexture(ASTEROID_DOWN_RECT, downTexture);
		Utils.DrawOutline(ASTEROID_COUNT_RECT, asteroidCount.ToString(), COUNT_STYLE, 2, COUNT_STYLE.normal.textColor, Color.black);
		
		Utils.DrawOutline(ITEMS_RECT, "Fuel", LABEL_STYLE, 2, LABEL_STYLE.normal.textColor, Color.black);
		if (items) {
			GUI.DrawTexture(ITEM_CHECK_RECT, checkedTexture);
		} else {	
			GUI.DrawTexture(ITEM_CHECK_RECT, uncheckedTexture);
		}
		
		GUI.DrawTexture(GO_RECT, buttonTexture);
		Utils.DrawOutline(GO_RECT, "START", COUNT_STYLE, 2, COUNT_STYLE.normal.textColor, Color.black);
		
		GUI.DrawTexture(BACK_RECT, buttonTexture);
		Utils.DrawOutline(BACK_RECT, "BACK", BACK_STYLE, 2, BACK_STYLE.normal.textColor, Color.black);
		
	}
	
	public override void Update() {
		if (Main.Clicked && shipCount < 3 && SHIP_UP_RECT.Contains(Main.ClickedLocation)) {
			Sounds.Click();
			shipCount++;
		} else if (Main.Clicked && shipCount > 1 && SHIP_DOWN_RECT.Contains(Main.ClickedLocation)) {
			Sounds.Click();
			shipCount--;
		} else if (Main.Clicked && asteroidCount < 10 && ASTEROID_UP_RECT.Contains(Main.ClickedLocation)) {
			Sounds.Click();
			asteroidCount++;
		} else if (Main.Clicked && asteroidCount > 2 && ASTEROID_DOWN_RECT.Contains(Main.ClickedLocation)) {
			Sounds.Click();
			asteroidCount--;
		} else if (Main.Clicked && ITEM_CHECK_RECT.Contains(Main.ClickedLocation)) {
			Sounds.Click();
			items = !items;
		} else if (Main.Clicked && GO_RECT.Contains(Main.ClickedLocation)) {
			Sounds.Click();
			Main.ChangeScenes(new StartingArcade(shipCount, asteroidCount, items));
		} else if (Main.Clicked && BACK_RECT.Contains(Main.ClickedLocation)) {
			Sounds.Back();
			Main.ChangeScenes(new Title());
		}
	}
	
}
