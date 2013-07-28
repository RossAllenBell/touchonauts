using UnityEngine;

public class RestartLevel {
	
	public static readonly Rect DOH_RECT;
	public static readonly GUIStyle DOH_STYLE;
	
	public static readonly Rect PRESS_RECT;
	public static readonly GUIStyle PRESS_STYLE;
	
	static RestartLevel() {
		DOH_RECT = new Rect(10, 10, Main.NATIVE_WIDTH - 20, Main.NATIVE_HEIGHT * 0.66f);
		DOH_STYLE = new GUIStyle();
		DOH_STYLE.fontSize = (int) (100 * Main.GUI_RATIO_WIDTH);
		DOH_STYLE.normal.textColor = Color.red;
		DOH_STYLE.alignment = TextAnchor.MiddleCenter;
		
		PRESS_RECT = new Rect(10, Main.NATIVE_HEIGHT * 0.66f, Main.NATIVE_WIDTH - 20, Main.NATIVE_HEIGHT / 3);
		PRESS_STYLE = new GUIStyle();
		PRESS_STYLE.fontSize = (int) (50 * Main.GUI_RATIO_WIDTH);
		PRESS_STYLE.normal.textColor = Color.white;
		PRESS_STYLE.alignment = TextAnchor.LowerCenter;
	}
	
	public static void OnGUI() {
		Utils.DrawOutline(DOH_RECT, "TRY AGAIN", DOH_STYLE, 2, DOH_STYLE.normal.textColor, Color.white);
		if (Time.time % 1 <= 0.5f) {
			Utils.DrawOutline(PRESS_RECT, "press to continue", PRESS_STYLE, 2, PRESS_STYLE.normal.textColor, Color.black);
		}
	}
	
}
