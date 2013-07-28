using UnityEngine;

public class Level1ToLevel2 : Scene {
	
	private float startTime;
	
	private int narrativeCounter = 0;
	private string[] narrative = new string[]{
		"LEVEL 2\n\nTime To Recharge",
		"Whew, that was a close one! You're in the clear for now, but you've got other problems. All that asteroid evasion used up precious fuel. The good news is that you know these very asteroids you came here to explore contain precious materials. Maybe you can use them to refuel.",
		"You'll have to go back in, but this time try to collect fuel material left behind when two asteroids collide. Collect enough fuel material to advance to the next level.",
	};
	
	public override void Begin() {
		base.Begin();
		Main.SetContinueLevel(2);
	}
	
	public override void OnGUI() {
		LevelTransition.OnGUI(narrativeCounter, narrative, startTime);
	}
	
	public override void Update() {
		if (LevelTransition.Update(ref narrativeCounter, narrative, ref startTime) || Main.IsSkipToNextLevel()) {
			Main.ChangeScenes(new Level2());
		}
	}
	
}
