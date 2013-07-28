using UnityEngine;

public class StartingArcade : Scene {
	
	private float startTime;
	
	private int narrativeCounter = 0;
	private string[] narrative = new string[]{};
	
	private int ships;
	private int asteroids;
	private bool items;
	
	public StartingArcade(int ships, int asteroids, bool items) {
		this.ships = ships;
		this.asteroids = asteroids;
		this.items = items;
		
		startTime = Time.time;
	}
	
	public override void Begin() {
		base.Begin();
	}
	
	public override void OnGUI() {
		LevelTransition.OnGUI(narrativeCounter, narrative, startTime);
	}
	
	public override void Update() {
		if (LevelTransition.Update(ref narrativeCounter, narrative, ref startTime)) {
			Main.ChangeScenes(new Arcade(ships, asteroids, items));
		}
	}
	
}

