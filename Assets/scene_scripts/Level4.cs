using UnityEngine;
using System.Collections.Generic;

public class Level4 : Scene {
	
	private const float FUEL_TO_COMPLETE = 100;
	private const float FUEL_DECAY = 0.25f;
	private const float FUEL_PER_ITEM = 7;
	
	private readonly List<float> fuels;
	
	private readonly GUIStyle statusOutlineStyle;
	
	private readonly Rect statusOutlineRectN1;
	private readonly Rect statusOutlineRectE1;
	private readonly Rect statusOutlineRectS1;
	private readonly Rect statusOutlineRectW1;
	
	private readonly GUIStyle statusStyle1;
	
	private readonly Rect statusOutlineRectN2;
	private readonly Rect statusOutlineRectE2;
	private readonly Rect statusOutlineRectS2;
	private readonly Rect statusOutlineRectW2;
	
	private readonly GUIStyle statusStyle2;
	
	public Level4() {
		statusOutlineStyle = new GUIStyle();
		Texture2D texture = new Texture2D(1,1);
	    texture.SetPixel(0,0,new Color(1f,1f,1f,0.5f));
	    texture.Apply();		
		statusOutlineStyle.normal.background = texture;
		
		statusOutlineRectN1 = new Rect(39, 19, Main.NATIVE_WIDTH - 78, 1);
		statusOutlineRectE1 = new Rect(Main.NATIVE_WIDTH - 39, 19, 1, (int) (30 * Main.GUI_RATIO_HEIGHT) + 1);
		statusOutlineRectS1 = new Rect(39, 19 + (int) (30 * Main.GUI_RATIO_HEIGHT) + 1, Main.NATIVE_WIDTH - 77, 1);
		statusOutlineRectW1 = new Rect(39, 20, 1, (int) (30 * Main.GUI_RATIO_HEIGHT));
		
		statusStyle1 = new GUIStyle();
		texture = new Texture2D(1,1);
	    texture.SetPixel(0,0,new Color(0f,0f,1f,0.5f));
	    texture.Apply();
		statusStyle1.normal.background = texture;
		
		statusOutlineRectN2 = new Rect(39, 39 + (int) (30 * Main.GUI_RATIO_HEIGHT) + 1, Main.NATIVE_WIDTH - 78, 1);
		statusOutlineRectE2 = new Rect(Main.NATIVE_WIDTH - 39, 39 + (int) (30 * Main.GUI_RATIO_HEIGHT) + 1, 1, (int) (30 * Main.GUI_RATIO_HEIGHT) + 1);
		statusOutlineRectS2 = new Rect(39, 39 + (int) (30 * Main.GUI_RATIO_HEIGHT) + 1 + (int) (30 * Main.GUI_RATIO_HEIGHT) + 1, Main.NATIVE_WIDTH - 77, 1);
		statusOutlineRectW2 = new Rect(39, 40 + (int) (30 * Main.GUI_RATIO_HEIGHT) + 1, 1, (int) (30 * Main.GUI_RATIO_HEIGHT));
		
		statusStyle2 = new GUIStyle();
		texture = new Texture2D(1,1);
	    texture.SetPixel(0,0,new Color(1f,0f,0f,0.5f));
	    texture.Apply();
		statusStyle2.normal.background = texture;
		
		fuels = new List<float>();
		fuels.Add(50);
		fuels.Add(50);
	}
	
	public override bool ControlShip {get{return true;}}
	
	public override bool AsteroidCollision {get{return true;}}
	
	public override bool ShipCollision {get{return true;}}
	
	public override int AsteroidCount {get{return 6;}}
	
	public override bool AsteroidItems {get{return true;}}
	
	public override void Begin() {
		base.Begin();
		
		Vector2 offset = new Vector2(Main.BOARD_WIDTH / 6, 0);
		Main.SpawnShip(Main.BOARD_CENTER - offset);
		Main.SpawnShip(Main.BOARD_CENTER + offset);
	}
	
	public override void OnGUI() {
		GUI.Box(statusOutlineRectN1, GUIContent.none, statusOutlineStyle);
		GUI.Box(statusOutlineRectE1, GUIContent.none, statusOutlineStyle);
		GUI.Box(statusOutlineRectS1, GUIContent.none, statusOutlineStyle);
		GUI.Box(statusOutlineRectW1, GUIContent.none, statusOutlineStyle);
		
		GUI.Box(new Rect(40, 20, (Main.NATIVE_WIDTH - 80) * Mathf.Max(0, Mathf.Min(1, fuels[0] / FUEL_TO_COMPLETE)), (int) (30 * Main.GUI_RATIO_HEIGHT)), GUIContent.none, statusStyle1);
		
		GUI.Box(statusOutlineRectN2, GUIContent.none, statusOutlineStyle);
		GUI.Box(statusOutlineRectE2, GUIContent.none, statusOutlineStyle);
		GUI.Box(statusOutlineRectS2, GUIContent.none, statusOutlineStyle);
		GUI.Box(statusOutlineRectW2, GUIContent.none, statusOutlineStyle);
		
		GUI.Box(new Rect(40, 40 + (int) (30 * Main.GUI_RATIO_HEIGHT) + 1, (Main.NATIVE_WIDTH - 80) * Mathf.Max(0, Mathf.Min(1, fuels[1] / FUEL_TO_COMPLETE)), (int) (30 * Main.GUI_RATIO_HEIGHT)), GUIContent.none, statusStyle2);
		
		if (Main.ShipCount() != 2 || fuels[0] <= 0 || fuels[1] <= 0) {
			RestartLevel.OnGUI();
		}
	}
	
	public override void Update() {
		if (Main.ShipCount() != 2 || fuels[0] <= 0 || fuels[1] <= 0) {
			Main.ClearShips();
			if (Main.Clicked) {
				Sounds.Click();
				Main.ChangeScenes(new Level3ToLevel4());
			}
		} else {
			fuels[0] -= FUEL_DECAY * Time.deltaTime;
			fuels[1] -= FUEL_DECAY * Time.deltaTime;
			if ((fuels[0] >= FUEL_TO_COMPLETE && fuels[1] >= FUEL_TO_COMPLETE) || Main.IsSkipToNextLevel()) {
				Main.ChangeScenes(new Level4ToLevel5());
			}
		}
	}
	
	public override void ItemGained(GameObject item) {
		int itemType = item.GetComponent<Item>().itemType;
		if (fuels[itemType] > 0) {
			fuels[itemType] += FUEL_PER_ITEM;
		}
	}
	
}
