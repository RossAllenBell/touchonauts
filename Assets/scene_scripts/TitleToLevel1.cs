using UnityEngine;

public class TitleToLevel1 : Scene {
	
	private float startTime;
	
	private int narrativeCounter = 0;
	private string[] narrative = new string[]{
		"LEVEL 1\n\nThe Wrong Side Of The Bed",
		"You woke from hyper-sleep to discover that your space prospecting mission took a turn for the worse! You were hit by a cluster of micrometeoroids and now your auto-pilot systems have gone haywire. Even worse, you realize you're in the middle of the main asteroid belt between Mars and Jupiter.",
		"It was tough to get a clear reading, but you think you're only a short distance away from a relatively safe area of space. Use your finger to guide your prospecting ship clear of any asteroids. Keep this up for a short while to advance to the next level. Good luck!",
	};
	
	public override void Begin() {
		base.Begin();
		Main.SetContinueLevel(1);
	}
	
	public override void OnGUI() {
		LevelTransition.OnGUI(narrativeCounter, narrative, startTime);
	}
	
	public override void Update() {
		if (LevelTransition.Update(ref narrativeCounter, narrative, ref startTime) || Main.IsSkipToNextLevel()) {
			Main.ChangeScenes(new Level1());
		}
	}
	
}

