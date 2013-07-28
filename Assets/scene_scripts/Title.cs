using UnityEngine;

public class Title : Scene {
	
	private readonly Rect TITLE_RECT;
	private readonly GUIStyle TITLE_STYLE;
	
	private readonly Rect CREDITS_RECT;
	private readonly GUIStyle CREDITS_STYLE;
	
	private readonly Rect NEW_GAME_RECT;
	private readonly Rect RESET_RECT;
	private readonly Rect ARCADE_RECT;
	private readonly Rect SOUNDS_RECT;
	
	private readonly GUIStyle BUTTON_STYLE;
	private readonly GUIStyle BUTTON_DISABLED_STYLE;
	
	private readonly Rect RESET_CONFIRM_RECT;
	private readonly Rect YES_RESET_RECT;
	private readonly Rect NO_RESET_RECT;
	private readonly GUIStyle RESET_CONFIRM_STYLE;
	
	private readonly Texture2D buttonTexture;
	
	private bool areYouSureReset = false;
	
	public Title() {
		TITLE_RECT = new Rect(10, 10, Main.NATIVE_WIDTH - 20, Main.NATIVE_HEIGHT / 3);
		TITLE_STYLE = new GUIStyle();
		TITLE_STYLE.fontSize = (int) (200 * Main.GUI_RATIO_WIDTH);
		TITLE_STYLE.normal.textColor = Color.red;
		TITLE_STYLE.alignment = TextAnchor.MiddleCenter;
		
		CREDITS_RECT = new Rect(10, Main.NATIVE_HEIGHT * 0.66f, Main.NATIVE_WIDTH - 20, Main.NATIVE_HEIGHT / 3);
		CREDITS_STYLE = new GUIStyle();
		CREDITS_STYLE.fontSize = (int) (50 * Main.GUI_RATIO_WIDTH);
		CREDITS_STYLE.normal.textColor = Color.white;
		CREDITS_STYLE.alignment = TextAnchor.LowerCenter;
		
		NEW_GAME_RECT = new Rect(Main.NATIVE_WIDTH / 12, (Main.NATIVE_HEIGHT / 15) * 5, Main.NATIVE_WIDTH / 3, Main.NATIVE_HEIGHT / 5);
		RESET_RECT = new Rect(Main.NATIVE_WIDTH / 12, (Main.NATIVE_HEIGHT / 15) * 10, Main.NATIVE_WIDTH / 3, Main.NATIVE_HEIGHT / 5);
		ARCADE_RECT = new Rect((Main.NATIVE_WIDTH / 12) * 7, (Main.NATIVE_HEIGHT / 15) * 5, Main.NATIVE_WIDTH / 3, Main.NATIVE_HEIGHT / 5);
		SOUNDS_RECT = new Rect((Main.NATIVE_WIDTH / 12) * 7, (Main.NATIVE_HEIGHT / 15) * 10, Main.NATIVE_WIDTH / 3, Main.NATIVE_HEIGHT / 5);
		
		buttonTexture = Resources.Load("ui/button") as Texture2D;
		
		BUTTON_STYLE = new GUIStyle();
		BUTTON_STYLE.fontSize = (int) (75 * Main.GUI_RATIO_WIDTH);
		BUTTON_STYLE.normal.textColor = Color.white;
		BUTTON_STYLE.alignment = TextAnchor.MiddleCenter;
		
		BUTTON_DISABLED_STYLE = new GUIStyle(BUTTON_STYLE);
		BUTTON_DISABLED_STYLE.normal.textColor = new Color(0.2f,0.2f,0.2f);
		
		RESET_CONFIRM_RECT = new Rect(10, 10, Main.NATIVE_WIDTH - 20, Main.NATIVE_HEIGHT / 3);
		YES_RESET_RECT = new Rect(Main.NATIVE_WIDTH / 12, (Main.NATIVE_HEIGHT / 15) * 6, Main.NATIVE_WIDTH / 3, Main.NATIVE_HEIGHT / 5);
		NO_RESET_RECT = new Rect((Main.NATIVE_WIDTH / 12) * 7, (Main.NATIVE_HEIGHT / 15) * 6, Main.NATIVE_WIDTH / 3, Main.NATIVE_HEIGHT / 5);
		
		RESET_CONFIRM_STYLE = new GUIStyle();
		RESET_CONFIRM_STYLE.fontSize = (int) (75 * Main.GUI_RATIO_WIDTH);
		RESET_CONFIRM_STYLE.normal.textColor = Color.white;
		RESET_CONFIRM_STYLE.alignment = TextAnchor.MiddleCenter;
	}
	
	public override bool AsteroidCollision {get{return true;}}
	
	public override int AsteroidCount {get{return 15;}}
	
	public override void OnGUI() {
		if (!areYouSureReset) {
			GUI.DrawTexture(NEW_GAME_RECT, buttonTexture);
			GUI.DrawTexture(RESET_RECT, buttonTexture);
			GUI.DrawTexture(ARCADE_RECT, buttonTexture);
			GUI.DrawTexture(SOUNDS_RECT, buttonTexture);
			
			Utils.DrawOutline(TITLE_RECT, "TOUCHONAUTS", TITLE_STYLE, 4, TITLE_STYLE.normal.textColor, Color.white);
			
			Utils.DrawOutline(CREDITS_RECT, "Created By: Ross Bell and Max Morris-Cohen", CREDITS_STYLE, 2, CREDITS_STYLE.normal.textColor, Color.black);
			
			Utils.DrawOutline(NEW_GAME_RECT, Main.continueLevel == 1? "NEW GAME" : "CONTINUE", BUTTON_STYLE, 2, BUTTON_STYLE.normal.textColor, Color.black);
			Utils.DrawOutline(RESET_RECT, "RESET", Main.continueLevel > 1? BUTTON_STYLE : BUTTON_DISABLED_STYLE, 2, Main.continueLevel > 1? BUTTON_STYLE.normal.textColor : BUTTON_DISABLED_STYLE.normal.textColor, Color.black);
			Utils.DrawOutline(ARCADE_RECT, "FREESTYLE", BUTTON_STYLE, 2, BUTTON_STYLE.normal.textColor, Color.black);
			Utils.DrawOutline(SOUNDS_RECT, "SOUND: " + (Sounds.Enabled? "ON" : "OFF"), BUTTON_STYLE, 2, BUTTON_STYLE.normal.textColor, Color.black);
		} else {
			Utils.DrawOutline(RESET_CONFIRM_RECT, "Are you sure you want to reset your progress?", RESET_CONFIRM_STYLE, 2, RESET_CONFIRM_STYLE.normal.textColor, Color.black);
			
			GUI.DrawTexture(YES_RESET_RECT, buttonTexture);
			GUI.DrawTexture(NO_RESET_RECT, buttonTexture);
			
			Utils.DrawOutline(YES_RESET_RECT, "YES", BUTTON_STYLE, 2, BUTTON_STYLE.normal.textColor, Color.black);
			Utils.DrawOutline(NO_RESET_RECT, "NO", BUTTON_STYLE, 2, BUTTON_STYLE.normal.textColor, Color.black);
		}
	}
	
	public override void Update() {
		if (!areYouSureReset) {
			if (Main.Clicked && NEW_GAME_RECT.Contains(Main.ClickedLocation)) {
				Sounds.Click();
				Main.ChangeScenes(Main.CreateContinueLevel());
			} else if (Main.Clicked && Main.continueLevel > 1 && RESET_RECT.Contains(Main.ClickedLocation)) {
				Sounds.Click();
				areYouSureReset = true;
				//Main.SetContinueLevel(1);
			} else if (Main.Clicked && ARCADE_RECT.Contains(Main.ClickedLocation)) {
				Sounds.Click();
				Main.ChangeScenes(new ArcadeSetup());
			} else if (Main.Clicked && SOUNDS_RECT.Contains(Main.ClickedLocation)) {
				Sounds.Enabled = !Sounds.Enabled;
				Sounds.Click();
			}
		} else {
			if (Main.Clicked && YES_RESET_RECT.Contains(Main.ClickedLocation)) {
				Sounds.Click();
				areYouSureReset = false;
				Main.SetContinueLevel(1);
			} else if (Main.Clicked && NO_RESET_RECT.Contains(Main.ClickedLocation)) {
				Sounds.Click();
				areYouSureReset = false;
			}
		}			
	}
	
}
