using UnityEngine;

public class Level2ToLevel3 : Scene {
	
	private float startTime;
	
	private int narrativeCounter = 0;
	private string[] narrative = new string[]{
		"LEVEL 3\n\nAn Unexpected Encounter",
		"Fully refueled, you made it out of that last asteroid cluster with hardly a scratch! Strangely, you received a communication signal from near by. It's probably another asteroid prospecting ship! It sounded like they're in a bit of trouble. Help them escape the asteroid cluster they're lost in.",
		"Now things get a little tricky. You'll need a second finger, either yours or a friend's. Each ship will follow one finger, and be sure to keep them from colliding. Good luck!",
	};
	
	public override void Begin() {
		base.Begin();
		Main.SetContinueLevel(3);
	}
	
	public override void OnGUI() {
		LevelTransition.OnGUI(narrativeCounter, narrative, startTime);
	}
	
	public override void Update() {
		if (LevelTransition.Update(ref narrativeCounter, narrative, ref startTime) || Main.IsSkipToNextLevel()) {
			Main.ChangeScenes(new Level3());
		}
	}
	
}
