using UnityEngine;

public class Utils {
	
	/*
	private static Texture2D BG_TEXTURE;
	
	static Utils() {
		BG_TEXTURE = new Texture2D(1,1);
	    BG_TEXTURE.SetPixel(0,0,new Color(1f,0f,0f,0f));
	    BG_TEXTURE.Apply();
	}
	*/
	
	public static void DrawOutline(Rect position, string text, GUIStyle style) {
		DrawOutline(position, text, style, 2);
	}
	
	public static void DrawOutline(Rect position, string text, GUIStyle style, int offset) {
		DrawOutline(position, text, style, offset, style.normal.textColor);
	}
		
	public static void DrawOutline(Rect position, string text, GUIStyle style, int offset, Color color) {
		DrawOutline(position, text, style, offset, color, InvertColor(color));
	}
	
	public static void DrawOutline(Rect position, string text, GUIStyle style, int offset, Color color, Color outColor){
		//GUI.DrawTexture(position, BG_TEXTURE);
		
		GUIStyle backupStyle = style;
	    style.normal.textColor = outColor;
	    position.x -= offset;
	    GUI.Label(position, text, style);
	    position.x += offset * 2;
	    GUI.Label(position, text, style);
	    position.x -= offset;
	    position.y -= offset;
	    GUI.Label(position, text, style);
	    position.y += offset * 2;
	    GUI.Label(position, text, style);
	    position.y -= offset;
	    style.normal.textColor = color;
	    GUI.Label(position, text, style);
	    style = backupStyle;
	}
	
	public static Color InvertColor (Color color) {
    	return new Color (1.0f-color.r, 1.0f-color.g, 1.0f-color.b);
	}
	
}

