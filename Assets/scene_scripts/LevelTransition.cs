using UnityEngine;

public class LevelTransition {
	
	public static readonly int COUNTDOWN = 3;
	
	public static readonly Rect TITLE_RECT;
	public static readonly GUIStyle TITLE_STYLE;
	
	public static readonly Rect NARRATIVE_RECT;
	public static readonly GUIStyle NARRATIVE_STYLE;
	
	public static readonly Rect PRESS_RECT;
	public static readonly GUIStyle PRESS_STYLE;
	
	public static readonly Rect COUNTER_RECT;
	public static readonly GUIStyle COUNTER_STYLE;
	
	public static readonly Texture2D buttonTexture;
	public static readonly Rect BACK_RECT;
	public static readonly GUIStyle BACK_STYLE;
	
	static LevelTransition() {
		TITLE_RECT = new Rect(10, 10, Main.NATIVE_WIDTH - 20, Main.NATIVE_HEIGHT * 0.66f);
		TITLE_STYLE = new GUIStyle();
		TITLE_STYLE.fontSize = (int) (100 * Main.GUI_RATIO_WIDTH);
		TITLE_STYLE.normal.textColor = Color.red;
		TITLE_STYLE.alignment = TextAnchor.MiddleCenter;
		
		NARRATIVE_RECT = new Rect(30, 30, Main.NATIVE_WIDTH - 60, Main.NATIVE_HEIGHT * 0.66f);
		NARRATIVE_STYLE = new GUIStyle();
		NARRATIVE_STYLE.fontSize = (int) (75 * Main.GUI_RATIO_WIDTH);
		NARRATIVE_STYLE.normal.textColor = Color.white;
		NARRATIVE_STYLE.alignment = TextAnchor.UpperLeft;
		NARRATIVE_STYLE.wordWrap = true;
		
		PRESS_RECT = new Rect(10, Main.NATIVE_HEIGHT * 0.66f, Main.NATIVE_WIDTH - 20, Main.NATIVE_HEIGHT / 3);
		PRESS_STYLE = new GUIStyle();
		PRESS_STYLE.fontSize = (int) (50 * Main.GUI_RATIO_WIDTH);
		PRESS_STYLE.normal.textColor = Color.white;
		PRESS_STYLE.alignment = TextAnchor.LowerCenter;
		
		COUNTER_RECT = new Rect(10, 10, Main.NATIVE_WIDTH - 20, Main.NATIVE_HEIGHT - 20);
		COUNTER_STYLE = new GUIStyle();
		COUNTER_STYLE.fontSize = (int) (200 * Main.GUI_RATIO_WIDTH);
		COUNTER_STYLE.normal.textColor = Color.red;
		COUNTER_STYLE.alignment = TextAnchor.MiddleCenter;
		
		BACK_STYLE = new GUIStyle();
		BACK_STYLE.fontSize = (int) (75 * Main.GUI_RATIO_WIDTH);
		BACK_STYLE.normal.textColor = Color.white;
		BACK_STYLE.alignment = TextAnchor.MiddleCenter;
		
		buttonTexture = Resources.Load("ui/button") as Texture2D;
		BACK_RECT = new Rect(10, Main.NATIVE_HEIGHT - ((150 * Main.GUI_RATIO) + 10), 250 * Main.GUI_RATIO, 150 * Main.GUI_RATIO);
	}
	
	public static void OnGUI(int narrativeCounter, string[] narrative, float startTime) {
		if (narrativeCounter < narrative.Length) {
			if (narrativeCounter == 0) {
				Utils.DrawOutline(TITLE_RECT, narrative[narrativeCounter], TITLE_STYLE, 2, TITLE_STYLE.normal.textColor, Color.white);
			} else {
				Utils.DrawOutline(NARRATIVE_RECT, narrative[narrativeCounter], NARRATIVE_STYLE, 2, NARRATIVE_STYLE.normal.textColor, Color.black);
			}
			
			if (Time.time % 1 <= 0.5f) {
				Utils.DrawOutline(PRESS_RECT, "press to continue", PRESS_STYLE, 2, PRESS_STYLE.normal.textColor, Color.black);
			}
			
			GUI.DrawTexture(BACK_RECT, buttonTexture);
			Utils.DrawOutline(BACK_RECT, "BACK", BACK_STYLE, 2, BACK_STYLE.normal.textColor, Color.black);
		} else {
			float elapsedTime = Time.time - startTime;
			if (elapsedTime > 0 && elapsedTime % 1 <= 0.5f) {
				Utils.DrawOutline(COUNTER_RECT, Mathf.CeilToInt(COUNTDOWN - elapsedTime).ToString(), COUNTER_STYLE, 4, COUNTER_STYLE.normal.textColor, Color.white);
			}
		}
	}
	
	public static bool Update(ref int narrativeCounter, string[] narrative, ref float startTime) {
		if (Main.Clicked && BACK_RECT.Contains(Main.ClickedLocation)) {
			Sounds.Back();
			Main.ChangeScenes(new Title());
			return false;
		}
		
		if (Main.Clicked) {
			if (narrativeCounter < narrative.Length) {
				Sounds.Click();
				narrativeCounter++;
				if (narrativeCounter == narrative.Length) {
					startTime = Time.time;
				}
			} else {
				//Sounds.Click();
				//startTime -= 1 - ((Time.time - startTime) % 1);
			}
		}
		
		if (narrativeCounter == narrative.Length) {
			float elapsedTime = Time.time - startTime;
			if (elapsedTime >= LevelTransition.COUNTDOWN) {
				return true;
			}
		}
		
		return false;
	}
	
}
