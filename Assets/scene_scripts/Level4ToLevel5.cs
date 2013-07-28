using UnityEngine;

public class Level4ToLevel5 : Scene {
	
	private float startTime;
	
	private int narrativeCounter = 0;
	private string[] narrative = new string[]{
		"LEVEL 5\n\nNearly Home",
		"All fueled up and ready to get out of here! Oh seriously, what now... you're getting a distress signal from a nearby asteroid cluster. It looks like there's a third surviving prospecting ship, but its engines are completely disabled. It will take both of your good ships to tow it out to safety.",
		"In this level, a third ship is being towed by a tractor beam between your two good ships. You'll have to be careful to keep that third ship out of harms way. Good luck!",
	};
	
	public override void Begin() {
		base.Begin();
		Main.SetContinueLevel(5);
	}
	
	public override void OnGUI() {
		LevelTransition.OnGUI(narrativeCounter, narrative, startTime);
	}
	
	public override void Update() {
		if (LevelTransition.Update(ref narrativeCounter, narrative, ref startTime) || Main.IsSkipToNextLevel()) {
			Main.ChangeScenes(new Level5());
		}
	}
	
}
