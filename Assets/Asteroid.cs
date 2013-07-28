using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	
	public const float MIN_SPEED = 1f;
	public const float MAX_SPEED = 2f;
	public const float MIN_ROTATION = 0f;
	public const float MAX_ROTATION = 150f;
	
	public const float VARIATION_FROM_CENTER = Mathf.PI * 0.75f;
	
	public static float RADIUS;
	
	private Vector2 start;
	private Vector2 destination;
	private float speed;
	private float rotation;
	private int rotationDirection;
	
	void Start () {
		start = getSpawnPointOutsideScreen();
		transform.position = start;
		destination = getOpposingPoint(start, VARIATION_FROM_CENTER);
		speed = (Random.value * (MAX_SPEED - MIN_SPEED)) + MIN_SPEED;
		rotation = (Random.value * (MAX_ROTATION - MIN_ROTATION)) + MIN_ROTATION;
		rotationDirection = Random.value < 0.5f? 1 : -1;
		//transform.localScale = new Vector3(transform.localScale.x * Main.GUI_RATIO, transform.localScale.y * Main.GUI_RATIO, 0.1f);
		RADIUS = transform.localScale.x * 0.5f;
	}
	
	void Update () {
		transform.Rotate(Vector3.forward, rotation * Time.deltaTime * rotationDirection);
		transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
		
		if ((Vector2) transform.position == destination) {
			Destroy(gameObject);
		}
	}
	
	public static Vector2 getSpawnPointOutsideScreen() {
		float radians = Random.value * 2 * Mathf.PI;
		float x = Mathf.Sin(radians);
		float y = Mathf.Cos(radians);
		Vector2 vector = new Vector2(x,y);
		vector.Normalize();
		float distanceOut = Main.BOARD_RADIUS + RADIUS;
		vector.Scale(new Vector2(distanceOut,distanceOut));
		vector = Main.BOARD_CENTER + vector;
		return vector;
	}
	
	public static Vector2 getOpposingPoint(Vector2 point, float withinRadians) {
		Vector2 centerMinusStart = Main.BOARD_CENTER - point;
		float radians = (Vector2.Angle(Vector2.up, centerMinusStart) / 180) * Mathf.PI;
		if (centerMinusStart.x < 0) {
			radians = (2f * Mathf.PI) - radians;
		}
		radians += (withinRadians * Random.value) - (withinRadians / 2);
		Vector2 vector = new Vector2(Mathf.Sin(radians), Mathf.Cos(radians));
		vector.Normalize();
		float distanceOut = Main.BOARD_RADIUS + RADIUS;
		vector.Scale(new Vector2(distanceOut,distanceOut));
		return Main.BOARD_CENTER + vector;
	}
}
