using UnityEngine;

public class Sounds {
	
	private static bool enabled;
	public static bool Enabled {get{return enabled;} set{enabled = value; PlayerPrefs.SetInt("SOUND", enabled? 1 : 0);}}
	
	private static AudioClip click;
	private static AudioClip back;
	private static AudioClip asteroidCollision;
	private static AudioClip shipExplosion;
	private static AudioClip item;
	
	private static AudioSource audio;
	
	static Sounds() {
		audio = Camera.main.audio;
		click = Resources.Load("audio/click") as AudioClip;
		back = Resources.Load("audio/back_short") as AudioClip;
		asteroidCollision = Resources.Load("audio/asteroid_collision_short") as AudioClip;
		shipExplosion = Resources.Load("audio/ship_explosion") as AudioClip;
		item = Resources.Load("audio/item_short") as AudioClip;
		
		enabled = PlayerPrefs.GetInt("SOUND", 1) == 1? true : false;
	}
	
	public static void Click() {
		if (enabled) audio.PlayOneShot(click);
	}
	
	public static void Back() {
		if (enabled) audio.PlayOneShot(back);
	}
	
	public static void AsteroidCollision() {
		if (enabled) audio.PlayOneShot(asteroidCollision);
	}
	
	public static void ShipExplosion() {
		if (enabled) audio.PlayOneShot(shipExplosion);
	}
	
	public static void Item() {
		if (enabled) audio.PlayOneShot(item);
	}
	
}
