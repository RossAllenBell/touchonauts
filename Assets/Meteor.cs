using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {
	
	public const float MIN_SPEED = 30f;
	public const float MAX_SPEED = 30f;
	
	private Vector2 start;
	private Vector2 destination;
	private float speed;
	private float startTime;
	
	void Start () {
		start = Asteroid.getSpawnPointOutsideScreen();
		transform.position = start;
		destination = Asteroid.getSpawnPointOutsideScreen();
		speed = (Random.value * (MAX_SPEED - MIN_SPEED)) + MIN_SPEED;
		startTime = Time.time;
	}
	
	void Update () {
		transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
		
		if (startTime <= Time.time - 2) {
			Destroy(gameObject);
		}
	}
}
