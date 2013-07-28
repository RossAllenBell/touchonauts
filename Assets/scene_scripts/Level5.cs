using UnityEngine;

public class Level5 : Scene {
	
	private const int seconds = 30;
	
	private readonly Rect statusOutlineRectN;
	private readonly Rect statusOutlineRectE;
	private readonly Rect statusOutlineRectS;
	private readonly Rect statusOutlineRectW;
	private readonly GUIStyle statusOutlineStyle;
	
	private readonly GUIStyle statusStyle;
	
	private float startTime;
	private float deadTime;
	
	public Level5() {
		statusOutlineRectN = new Rect(39, 19, Main.NATIVE_WIDTH - 78, 1);
		statusOutlineRectE = new Rect(Main.NATIVE_WIDTH - 39, 19, 1, (int) (30 * Main.GUI_RATIO_HEIGHT) + 1);
		statusOutlineRectS = new Rect(39, 19 + (int) (30 * Main.GUI_RATIO_HEIGHT) + 1, Main.NATIVE_WIDTH - 77, 1);
		statusOutlineRectW = new Rect(39, 20, 1, (int) (30 * Main.GUI_RATIO_HEIGHT));
		statusOutlineStyle = new GUIStyle();
		Texture2D texture = new Texture2D(1,1);
	    texture.SetPixel(0,0,new Color(1f,1f,1f,0.5f));
	    texture.Apply();		
		statusOutlineStyle.normal.background = texture;
		
		statusStyle = new GUIStyle();
		texture = new Texture2D(1,1);
	    texture.SetPixel(0,0,new Color(1f,0f,0f,0.5f));
	    texture.Apply();
		statusStyle.normal.background = texture;
	}
	
	public override bool ControlShip {get{return true;}}
	
	public override bool AsteroidCollision {get{return true;}}
	
	public override bool ShipCollision {get{return true;}}
	
	public override int AsteroidCount {get{return 6;}}
	
	public override int ControlShipCount {get{return 2;}}
	
	public override void Begin() {
		base.Begin();
		
		Vector2 offset = new Vector2(Main.BOARD_WIDTH / 6, 0);
		Main.SpawnShip(Main.BOARD_CENTER - offset);
		Main.SpawnShip(Main.BOARD_CENTER + offset);
		Main.SpawnShip(Main.BOARD_CENTER);
		startTime = Time.time;
		deadTime = Time.time;
	}
	
	public override void OnGUI() {
		GUI.Box(statusOutlineRectN, GUIContent.none, statusOutlineStyle);
		GUI.Box(statusOutlineRectE, GUIContent.none, statusOutlineStyle);
		GUI.Box(statusOutlineRectS, GUIContent.none, statusOutlineStyle);
		GUI.Box(statusOutlineRectW, GUIContent.none, statusOutlineStyle);
		
		GUI.Box(new Rect(40, 20, (Main.NATIVE_WIDTH - 80) * Mathf.Min(1, (deadTime - startTime) / seconds), (int) (30 * Main.GUI_RATIO_HEIGHT)), GUIContent.none, statusStyle);
		
		if (Main.ShipCount() != 3) {
			RestartLevel.OnGUI();
		}
	}
	
	public override void Update() {
		if (Main.ShipCount() == 3) {
			deadTime = Time.time;
			
			Vector2 midPoint = Main.ships[0].transform.position + Main.ships[1].transform.position;
			midPoint.Scale(new Vector2(0.5f, 0.5f));
			Main.ships[2].transform.position = midPoint;
			
			Vector2 vectorBetween = Main.ships[1].transform.position - Main.ships[0].transform.position;
			float angle = (vectorBetween.y == 0? (vectorBetween.x > 0? 0f : Mathf.PI) : Mathf.Atan2(vectorBetween.y, vectorBetween.x)) * -180 / Mathf.PI;
			Main.ships[2].transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
			
			if (Time.time - startTime >= seconds || Main.IsSkipToNextLevel()) {
				Main.ChangeScenes(new Credits());
			}
		} else {
			if (Main.Clicked) {
				Sounds.Click();
				Main.ChangeScenes(new Level4ToLevel5());
			}
		}
	}
	
}
