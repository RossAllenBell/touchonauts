using UnityEngine;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	
	private readonly Rect FIX_TEXT_RECT = new Rect(0, 0, 100, 100);
	
	public static bool DEBUG = false;
	private static int DEBUG_SEQUENCE_INDEX = 0;
	
	private Rect DEBUG_RECT_L;
	private Rect DEBUG_RECT_R;
	private GUIStyle DEBUG_STYLE;
	
	private static Rect KONAMI_DEBUG_UP_RECT;
	private static Rect KONAMI_DEBUG_DOWN_RECT;
	private static Rect KONAMI_DEBUG_LEFT_RECT;
	private static Rect KONAMI_DEBUG_RIGHT_RECT;
	private static int KONAMI_DEBUG_SEQUENCE_INDEX = 0;
	
	private const int NORMAL_WIDTH = 1920;
	private const int NORMAL_HEIGHT = 1080;	
	private const int ASTEROID_MAT_COUNT = 10;
	private const float SECONDS_PER_ASTEROID_SPAWN = 0.5f;
	
	public static float GUI_RATIO;
	public static float GUI_RATIO_WIDTH;
	public static float GUI_RATIO_HEIGHT;
	public static int NATIVE_WIDTH;
	public static int NATIVE_HEIGHT;	
	public static float BOARD_WIDTH;
	public static float BOARD_HEIGHT;	
	public static float BOARD_RADIUS;
	public static Vector2 BOARD_CENTER;
	
	private static List<Vector2> touchInputs;
	private static Dictionary<int, Vector2> touchInputMappings;
	
	public static List<GameObject> ships;
	private static List<GameObject> asteroids;
	private static List<GameObject> items;
	private static List<GameObject> meteors;
	
	private static float lastAsteroidSpawnTime;
	
	private static float lastMeteorSpawnTime;
	private static float nextMeteorIn;
	
	private static Scene currentScene;
	public static Scene CurrentScene {get{return currentScene;}}
	
	private static bool click;
	private static Vector2 clickLocation;
	private static bool touching;
	public static bool Clicked {get{return click;}}
	public static Vector2 ClickedLocation {get{return clickLocation;}}
	public static bool Touching {get{return touching;}}
	
	public static int continueLevel = 1;

	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		
		NATIVE_WIDTH = Screen.width;
		NATIVE_HEIGHT = Screen.height;
		
		GUI_RATIO_WIDTH = (float) NATIVE_WIDTH / (float) NORMAL_WIDTH;
		GUI_RATIO_HEIGHT = (float) NATIVE_WIDTH / (float) NORMAL_WIDTH;
		GUI_RATIO = Mathf.Min(GUI_RATIO_WIDTH, GUI_RATIO_HEIGHT);
		
		Camera camera = Camera.main;
		
		//just makes sure the game view width is always 10 units wide
		float newOrthoSize = ((10f * NATIVE_HEIGHT) / NATIVE_WIDTH) / 2f;
		camera.orthographicSize = newOrthoSize;
		
		BOARD_HEIGHT = 2f * camera.orthographicSize;
		BOARD_WIDTH = BOARD_HEIGHT * camera.aspect;
		
		Vector3 newCameraLocation = camera.transform.position;
		newCameraLocation.y = newOrthoSize;
		newCameraLocation.x = BOARD_WIDTH / 2f;
		camera.transform.position = newCameraLocation;
		
		touchInputs = new List<Vector2>();
		touchInputMappings = new Dictionary<int, Vector2>();
		
		ships = new List<GameObject>();
		
		asteroids = new List<GameObject>();
		lastAsteroidSpawnTime = 0;
		
		items = new List<GameObject>();
		
		meteors = new List<GameObject>();
		lastMeteorSpawnTime = 0;
		nextMeteorIn = 0;
		
		BOARD_RADIUS = (Mathf.Sqrt(Mathf.Pow(BOARD_WIDTH, 2) + Mathf.Pow(BOARD_HEIGHT, 2))) / 2f;
		BOARD_CENTER = new Vector2(BOARD_WIDTH / 2f, BOARD_HEIGHT / 2f);
		
		click = false;
		touching = false;
		
		DEBUG_RECT_L = new Rect(0, 0, Main.NATIVE_HEIGHT, 100);
		DEBUG_RECT_R = new Rect(Main.NATIVE_WIDTH - Main.NATIVE_HEIGHT, 0, Main.NATIVE_HEIGHT, 100);
		DEBUG_STYLE = new GUIStyle();
		DEBUG_STYLE.fontSize = (int) (50 * Main.GUI_RATIO_WIDTH);
		DEBUG_STYLE.normal.textColor = Color.red;
		DEBUG_STYLE.alignment = TextAnchor.UpperCenter;
		
		KONAMI_DEBUG_UP_RECT = new Rect((Main.NATIVE_WIDTH / 2f) - (Main.NATIVE_WIDTH / 20f), 0f, Main.NATIVE_WIDTH / 10f, Main.NATIVE_HEIGHT / 10f);
		KONAMI_DEBUG_DOWN_RECT = new Rect((Main.NATIVE_WIDTH / 2f) - (Main.NATIVE_WIDTH / 20f), Main.NATIVE_HEIGHT - (Main.NATIVE_HEIGHT / 10f), Main.NATIVE_WIDTH / 10f, Main.NATIVE_HEIGHT / 10f);
		KONAMI_DEBUG_LEFT_RECT = new Rect(0, (Main.NATIVE_HEIGHT / 2f) - (Main.NATIVE_HEIGHT / 20f), Main.NATIVE_WIDTH / 10f, Main.NATIVE_HEIGHT / 10f);
		KONAMI_DEBUG_RIGHT_RECT = new Rect(Main.NATIVE_WIDTH - (Main.NATIVE_WIDTH / 10f), (Main.NATIVE_HEIGHT / 2f) - (Main.NATIVE_HEIGHT / 20f), Main.NATIVE_WIDTH / 10f, Main.NATIVE_HEIGHT / 10f);
		
		continueLevel = PlayerPrefs.GetInt("CONTINUE_LEVEL", continueLevel);
		
		ChangeScenes(new Title());
	}
	
	void Update () {
		if (DEBUG_SEQUENCE_INDEX == 0 && Input.GetKeyUp(KeyCode.C)) {
			DEBUG_SEQUENCE_INDEX++;
		} else if (DEBUG_SEQUENCE_INDEX == 1 && Input.GetKeyUp(KeyCode.A)) {
			DEBUG_SEQUENCE_INDEX++;
		} else if (DEBUG_SEQUENCE_INDEX == 2 && Input.GetKeyUp(KeyCode.B)) {
			DEBUG_SEQUENCE_INDEX++;
		} else if (DEBUG_SEQUENCE_INDEX == 3 && Input.GetKeyUp(KeyCode.B)) {
			DEBUG_SEQUENCE_INDEX++;
		} else if (DEBUG_SEQUENCE_INDEX == 4 && Input.GetKeyUp(KeyCode.A)) {
			DEBUG_SEQUENCE_INDEX++;
		} else if (DEBUG_SEQUENCE_INDEX == 5 && Input.GetKeyUp(KeyCode.G)) {
			DEBUG_SEQUENCE_INDEX++;
		} else if (DEBUG_SEQUENCE_INDEX == 6 && Input.GetKeyUp(KeyCode.E)) {
			DEBUG_SEQUENCE_INDEX++;
			DEBUG = true;
		}
		
		if (Input.GetKeyUp(KeyCode.Escape)) {
			if (currentScene is Title) {
				Sounds.Back();
				Application.Quit();
			} else {
				Sounds.Back();
				ChangeScenes(new Title());
			}
		}
		
		if (Input.touchCount > 0 | Input.GetMouseButton(0)) {
			Vector2 tempLocation = Input.touchCount > 0? (Vector2) Input.GetTouch(0).position : (Vector2) Input.mousePosition;
			clickLocation = new Vector2(tempLocation.x, NATIVE_HEIGHT - tempLocation.y);
			click = !touching;
			touching = true;
		} else {
			click = false;
			touching = false;
		}
		
		if (KONAMI_DEBUG_SEQUENCE_INDEX == 0 && click && KONAMI_DEBUG_UP_RECT.Contains(clickLocation)) {
			KONAMI_DEBUG_SEQUENCE_INDEX++;
		} else if (KONAMI_DEBUG_SEQUENCE_INDEX == 1 && click && KONAMI_DEBUG_UP_RECT.Contains(clickLocation)) {
			KONAMI_DEBUG_SEQUENCE_INDEX++;
		} else if (KONAMI_DEBUG_SEQUENCE_INDEX == 2 && click && KONAMI_DEBUG_DOWN_RECT.Contains(clickLocation)) {
			KONAMI_DEBUG_SEQUENCE_INDEX++;
		} else if (KONAMI_DEBUG_SEQUENCE_INDEX == 3 && click && KONAMI_DEBUG_DOWN_RECT.Contains(clickLocation)) {
			KONAMI_DEBUG_SEQUENCE_INDEX++;
		} else if (KONAMI_DEBUG_SEQUENCE_INDEX == 4 && click && KONAMI_DEBUG_LEFT_RECT.Contains(clickLocation)) {
			KONAMI_DEBUG_SEQUENCE_INDEX++;
		} else if (KONAMI_DEBUG_SEQUENCE_INDEX == 5 && click && KONAMI_DEBUG_RIGHT_RECT.Contains(clickLocation)) {
			KONAMI_DEBUG_SEQUENCE_INDEX++;
		} else if (KONAMI_DEBUG_SEQUENCE_INDEX == 6 && click && KONAMI_DEBUG_LEFT_RECT.Contains(clickLocation)) {
			KONAMI_DEBUG_SEQUENCE_INDEX++;
		} else if (KONAMI_DEBUG_SEQUENCE_INDEX == 7 && click && KONAMI_DEBUG_RIGHT_RECT.Contains(clickLocation)) {
			KONAMI_DEBUG_SEQUENCE_INDEX++;
			DEBUG = true;
		} else if(click && KONAMI_DEBUG_SEQUENCE_INDEX > 0) {
			KONAMI_DEBUG_SEQUENCE_INDEX = 0;
		}
		
		touchInputs.Clear();
		if (Input.touchCount > 0) {
			for (int i=0; i<Input.touchCount; i++) {
				touchInputs.Add(InputLocationToBoardLocation(Input.GetTouch(i).position));
			}
		} else if(Input.GetMouseButton(0)) {
			touchInputs.Add(InputLocationToBoardLocation(Input.mousePosition));
		}
		RemapShipInput();
		
		currentScene.Update();
		
		// so this looks like an inefficient mess of loops, but it's actually
		// not that bad and sure beats letting a physics engine check for
		// collisions
		for (int i = asteroids.Count - 1; i >= 0; i--) {
			if (asteroids[i] == null) {
				asteroids.RemoveAt(i);
				continue;
			}
			
			if (currentScene.ShipCollision) {
				for (int j=ships.Count - 1; j>=0; j--) {
					if (asteroids[i] != null && ships[j] != null && ((Vector2) (asteroids[i].transform.position - ships[j].transform.position)).magnitude <= Ship.RADIUS + Asteroid.RADIUS) {
						Sounds.ShipExplosion();
						GameObject shipExplosion = (GameObject) Instantiate(Resources.Load("ship_explosion"));
						shipExplosion.transform.position = ships[j].transform.position;
						Destroy(ships[j]);
						ships.RemoveAt(j);
					}
				}
			}
			
			if (currentScene.AsteroidCollision) {
				for (int k=i-1; k>=0; k--) {
					if (asteroids[i] != null && asteroids[k] != null && ((Vector2) (asteroids[i].transform.position - asteroids[k].transform.position)).magnitude <= Asteroid.RADIUS * 2.0f) {
						Sounds.AsteroidCollision();
						currentScene.AsteroidCollisionEvent(asteroids[i], asteroids[k]);
						GameObject asteroidExplosion = (GameObject) Instantiate(Resources.Load("asteroid_explosion"));
						Vector2 contactLocation = Vector2.Scale((Vector2) (asteroids[i].transform.position + asteroids[k].transform.position), new Vector2(0.5f, 0.5f));
						asteroidExplosion.transform.position = contactLocation;
						if (currentScene.AsteroidItems && ships.Count > 0) {
							GameObject item = (GameObject) Instantiate(Resources.Load("item_prefab"));
							int itemType = ships[(int) (Random.value * ships.Count)].GetComponent<Ship>().shipNumber;
							item.renderer.material = (Material) Resources.Load("item_" + itemType, typeof(Material));
							item.GetComponent<Item>().itemType = itemType;
							item.transform.position = contactLocation;
							items.Add(item);
						}
						Destroy(asteroids[i]);
						Destroy(asteroids[k]);
					}
				}
			}
		}
		
		for (int i = ships.Count - 1; i >= 0; i--) {			
			if (ships[i] == null) {
				ships.RemoveAt(i);
				continue;
			}
			
			if (currentScene.ShipCollision) {
				for (int k=i-1; k>=0; k--) {
					if (ships[i] != null && ships[k] != null && ((Vector2) (ships[i].transform.position - ships[k].transform.position)).magnitude <= Ship.RADIUS * 2.0f) {
						Sounds.ShipExplosion();
						GameObject shipExplosion = (GameObject) Instantiate(Resources.Load("ship_explosion"));
						shipExplosion.transform.position = ships[i].transform.position;
						shipExplosion = (GameObject) Instantiate(Resources.Load("ship_explosion"));
						shipExplosion.transform.position = ships[k].transform.position;
						Destroy(ships[i]);
						ships.RemoveAt(i);
						Destroy(ships[k]);
						break;
					}
				}
			}
		}
		
		for (int i = items.Count - 1; i >= 0; i--) {
			if (items[i] == null) {
				items.RemoveAt(i);
				continue;
			}
			
			if (currentScene.ShipCollision) {
				for (int j=0; j<ships.Count; j++) {
					if (items[i] != null && ships[j] != null && ((Vector2) (items[i].transform.position - ships[j].transform.position)).magnitude <= Ship.RADIUS + Item.RADIUS && items[i].GetComponent<Item>().itemType == ships[j].GetComponent<Ship>().shipNumber) {
						Sounds.Item();
						GameObject itemExplosion = (GameObject) Instantiate(Resources.Load("item_explosion"));
						itemExplosion.transform.position = items[i].transform.position;
						currentScene.ItemGained(items[i]);
						Destroy(items[i]);
						break;
					}
				}
			}
		}
		
		for (int i = meteors.Count - 1; i >= 0; i--) {			
			if (meteors[i] == null) {
				meteors.RemoveAt(i);
			}
		}
		
		if (asteroids.Count < currentScene.AsteroidCount && lastAsteroidSpawnTime <= Time.time - SECONDS_PER_ASTEROID_SPAWN) {
			GameObject asteroid = (GameObject) Instantiate(Resources.Load("asteroid_prefab"));
			asteroid.renderer.material = (Material) Resources.Load("asteroid_mats/asteroid_0" + (int) (Random.value * ASTEROID_MAT_COUNT), typeof(Material));
			asteroids.Add(asteroid);
			lastAsteroidSpawnTime = Time.time;
		}
		
		if (lastMeteorSpawnTime + nextMeteorIn <= Time.time) {
			GameObject meteor = (GameObject) Instantiate(Resources.Load("meteor_prefab"));
			meteors.Add(meteor);
			lastMeteorSpawnTime = Time.time;
			nextMeteorIn = 5 * Random.value;
		}
		
	}
	
	void OnGUI() {
		
		if (DEBUG) {
			Matrix4x4 savedMatrix = GUI.matrix;
			GUIUtility.RotateAroundPivot(270, new Vector2(Main.NATIVE_HEIGHT / 2, Main.NATIVE_HEIGHT / 2));
			Utils.DrawOutline(DEBUG_RECT_L, "DEBUG", DEBUG_STYLE, 1, DEBUG_STYLE.normal.textColor, Color.white);
			GUI.matrix = savedMatrix;
			GUIUtility.RotateAroundPivot(90, new Vector2(Main.NATIVE_WIDTH - (Main.NATIVE_HEIGHT / 2), Main.NATIVE_HEIGHT / 2));
			Utils.DrawOutline(DEBUG_RECT_R, "DEBUG", DEBUG_STYLE, 1, DEBUG_STYLE.normal.textColor, Color.white);
			GUI.matrix = savedMatrix;
		}
		
		GUI.Label(FIX_TEXT_RECT, ".");
		
		currentScene.OnGUI();
	}
	
	public static Vector2 GetTouchLocation(Ship ship) {
		return touchInputMappings[ship.gameObject.GetInstanceID()];
	}
	
	public static void ClearShips() {
		for (int i = ships.Count - 1; i >= 0; i--) {
			if (ships[i] != null) {
				Destroy(ships[i]);
			}
			ships.RemoveAt(i);
		}
		
		touchInputMappings.Clear();
	}
	
	public static GameObject SpawnShip(Vector2 location) {
		GameObject ship = (GameObject) Instantiate(Resources.Load("ship_" + ships.Count + "_prefab"));
		ship.GetComponent<Ship>().shipNumber = ships.Count;
		ships.Add(ship);
		ship.transform.position = location;
		
		touchInputMappings.Add(ship.GetInstanceID(), location);
		
		return ship;
	}
	
	public static void ClearAsteroids() {
		for (int i = asteroids.Count - 1; i >= 0; i--) {
			if (asteroids[i] != null) {
				GameObject asteroidExplosion = (GameObject) Instantiate(Resources.Load("asteroid_explosion"));
				asteroidExplosion.transform.position = asteroids[i].transform.position;
				Destroy(asteroids[i]);
			}
			asteroids.RemoveAt(i);
		}
	}
	
	public static void ClearItems() {
		for (int i = items.Count - 1; i >= 0; i--) {
			if (items[i] != null) {
				GameObject itemExplosion = (GameObject) Instantiate(Resources.Load("item_explosion"));
				itemExplosion.transform.position = items[i].transform.position;
				Destroy(items[i]);
			}
			items.RemoveAt(i);
		}
	}
	
	public static void ChangeScenes(Scene scene) {
		currentScene = scene;
		currentScene.Begin();
	}
	
	public static int ShipCount() {
		return ships.Count;
	}
	
	public static Vector2 InputLocationToBoardLocation(Vector2 inputLocation) {
		return new Vector2(BOARD_WIDTH * (inputLocation.x / NATIVE_WIDTH), BOARD_HEIGHT * (inputLocation.y / NATIVE_HEIGHT));
	}
	
	private static void RemapShipInput() {
		List<GameObject> tempShips = new List<GameObject>(ships).FindAll(i => i != null && i.GetComponent<Ship>().shipNumber < currentScene.ControlShipCount);
		List<Vector2> tempInputs = new List<Vector2>(touchInputs);
		while (tempShips.Count > 0 && tempInputs.Count > 0) {
			GameObject closestShip = null;
			Vector2 closestInput = Vector2.zero;
			float closestDistance = float.MaxValue;
			foreach (GameObject ship in tempShips) {
				if (ship != null) {
					foreach (Vector2 input in tempInputs) {
						float thisDistance = Vector2.Distance(ship.transform.position, input);
						if (thisDistance < closestDistance) {
							closestShip = ship;
							closestInput = input;
							closestDistance = thisDistance;
						}
					}
				}
			}
			if (closestShip != null) {
				touchInputMappings[closestShip.GetInstanceID()] = closestInput;
				tempInputs.Remove(closestInput);
			}
			tempShips.Remove(closestShip);
		}
	}
	
	public static Scene CreateContinueLevel() {
		switch (continueLevel) {
		case 1: return new TitleToLevel1();
		case 2: return new Level1ToLevel2();
		case 3: return new Level2ToLevel3();
		case 4: return new Level3ToLevel4();
		case 5: return new Level4ToLevel5();
		default: return new TitleToLevel1();
		}
	}
	
	public static void SetContinueLevel(int levelNumber) {
		continueLevel = levelNumber;
		PlayerPrefs.SetInt("CONTINUE_LEVEL", continueLevel);
	}
	
	public static bool IsSkipToNextLevel() {
		return Main.DEBUG && (Input.GetKeyUp(KeyCode.RightArrow) || (click && KONAMI_DEBUG_RIGHT_RECT.Contains(clickLocation)));
	}
	
}
