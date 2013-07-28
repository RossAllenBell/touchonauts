using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	
	public const float LIFESPAN = 3.0f;
	public const float SPEED = 0.25f;
	public const float ROTATION = 50f;
	public const float START_BLINK_DURATION = 0.5f;
	public const float BLINK_DECAY = 0.835f;
	
	public static float RADIUS;
	
	private Vector2 direction;
	private int rotationDirection;
	private float dieAt;
	private bool startedBlink;
	private float blinkDuration;
	
	public int itemType;
	
	void Start () {
		float radians = Random.value * 2 * Mathf.PI;
		float x = Mathf.Sin(radians);
		float y = Mathf.Cos(radians);
		direction = new Vector2(x,y);
		rotationDirection = Random.value < 0.5f? 1 : -1;
		RADIUS = transform.localScale.x * 0.5f;	
		dieAt = Time.time + LIFESPAN;
		blinkDuration = START_BLINK_DURATION;
	}
	
	void Update () {
		transform.Rotate(Vector3.forward, ROTATION * Time.deltaTime * rotationDirection);
		transform.position = Vector2.MoveTowards(transform.position, (Vector2) transform.position + direction, SPEED * Time.deltaTime);
		
		if (dieAt <= Time.time) {
			Destroy(gameObject);
		} else if (!startedBlink) {
			startedBlink = true;
			StartCoroutine(Blink());
		}
	}
	
	IEnumerator Blink() {
		while (true) {
			if (dieAt <= Time.time) {
				return true;
			}
			yield return new WaitForSeconds(blinkDuration);
			renderer.enabled = !renderer.enabled;
			blinkDuration *= BLINK_DECAY;
		}		
	}
}
