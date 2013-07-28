using UnityEngine;

public class Credits : Scene {
	
	private int messageCount = 0;
	
	public readonly Rect TITLE_RECT;
	public readonly GUIStyle TITLE_STYLE;
	
	public readonly Rect NARRATIVE_RECT;
	public readonly GUIStyle NARRATIVE_STYLE;
	
	public readonly Rect PRESS_RECT;
	public readonly GUIStyle PRESS_STYLE;
	
	public Credits() {
		TITLE_RECT = new Rect(0, 0, Main.NATIVE_WIDTH, Main.NATIVE_HEIGHT);
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
	}
	
	public override void OnGUI() {
		switch (messageCount) {
		case 0: Utils.DrawOutline(NARRATIVE_RECT, "Free of the asteroid belt, your asteroid prospecting ensemble finally made it back to Earth. Congratulations on a safe return home!", NARRATIVE_STYLE, 2, NARRATIVE_STYLE.normal.textColor, Color.black); break;
		case 1: Utils.DrawOutline(TITLE_RECT, "THE END", TITLE_STYLE, 2, TITLE_STYLE.normal.textColor, Color.white); break;
		case 2: Utils.DrawOutline(TITLE_RECT, "Created by:\n\nRoss Bell\nMax Morris-Cohen", TITLE_STYLE, 2, TITLE_STYLE.normal.textColor, Color.white); break;
		}
		
		if (Time.time % 1 <= 0.5f) {
			Utils.DrawOutline(PRESS_RECT, "press to continue", PRESS_STYLE, 2, PRESS_STYLE.normal.textColor, Color.black);
		}
	}
	
	public override void Update() {
		if (Main.Clicked) {
			Sounds.Click();
			messageCount++;
			
			if (messageCount == 3) {
				Main.ChangeScenes(new Title());
			}
		}
	}
	
}
