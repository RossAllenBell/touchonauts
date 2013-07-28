using UnityEngine;

public class Level3ToLevel4 : Scene {
	
	private float startTime;
	
	private int narrativeCounter = 0;
	private string[] narrative = new string[]{
		"LEVEL 4\n\nOne For The Road",
		"Holy cow, what a ride! You both barely made it out of there alive, but at least you're in one piece. The bad news is that you need to refuel before the voyage back to Earth.",
		"This level is similar to the previous re-fueling level, but each ship takes a different type of fuel. You can tell which fuel type belongs to which ship by the color.",
	};
	
	public override void Begin() {
		base.Begin();
		Main.SetContinueLevel(4);
	}
	
	public override void OnGUI() {
		LevelTransition.OnGUI(narrativeCounter, narrative, startTime);
	}
	
	public override void Update() {
		if (LevelTransition.Update(ref narrativeCounter, narrative, ref startTime) || Main.IsSkipToNextLevel()) {
			Main.ChangeScenes(new Level4());
		}
	}
	
}
