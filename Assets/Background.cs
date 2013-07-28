using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
	
	private bool setupAfterMain = false;
	private int rotationDirection = 1;
	
	void Start () {
		rotationDirection = Random.value < 0.5f? 1 : -1;
	}
	
	void Update () {
		if (!setupAfterMain) {
			Vector3 startPostion = new Vector3(Main.BOARD_WIDTH / 2, Main.BOARD_HEIGHT / 2, 2f);
			transform.position = startPostion;
			setupAfterMain = true;
		}
		
		transform.Rotate(Vector3.up, 0.125f * Time.deltaTime * rotationDirection);
		//transform.position = Vector2.MoveTowards(transform.position, new Vector2(0f,0f), 0.02f * Time.deltaTime);
	}
}
