using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	
	public const float MAX_SPEED = 8f;
	public const float MAX_ROTATE = 500f;
	
	public static float RADIUS;
	
	private Vector2 desiredLocation;
	
	public int shipNumber;

	void Start () {
		desiredLocation = transform.position;
		//transform.localScale = new Vector3(transform.localScale.x * Main.GUI_RATIO, transform.localScale.y * Main.GUI_RATIO, 0.1f);
		RADIUS = transform.localScale.x * 0.5f;
	}
	
	void Update () {
		if (Main.CurrentScene.ControlShip && Main.CurrentScene.ControlShipCount > shipNumber) {
			desiredLocation = Main.GetTouchLocation(this);
		
			if (desiredLocation != (Vector2) transform.position) {
				Vector2 positionToFace = desiredLocation - (Vector2) transform.position;
				float zRotation = (Mathf.Atan2(positionToFace.y, positionToFace.x) * 180 / Mathf.PI) + 90;
				Quaternion desiredRotation = Quaternion.AngleAxis(zRotation, Vector3.forward);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, MAX_ROTATE * Time.deltaTime);
			}
			
			transform.position = Vector2.MoveTowards(transform.position, desiredLocation, MAX_SPEED * Time.deltaTime);
		}
	}
}
