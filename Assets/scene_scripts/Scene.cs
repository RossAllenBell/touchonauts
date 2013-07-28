using UnityEngine;

public abstract class Scene {
	
	public virtual bool ControlShip {get{return false;}}
	
	public virtual int ControlShipCount {get{return 3;}}
	
	public virtual bool ShipCollision {get{return false;}}
	
	public virtual bool AsteroidCollision {get{return false;}}
	
	public virtual bool AsteroidItems {get{return false;}}
	
	public virtual int AsteroidCount {get{return 0;}}
	
	public virtual void Begin() {
		Main.ClearShips();
		Main.ClearAsteroids();
	}
	
	public virtual void Update() {}
	
	public virtual void OnGUI() {}
	
	public virtual void End() {}
	
	public virtual void ItemGained(GameObject item) {}
	
	public virtual void AsteroidCollisionEvent(GameObject asteroid1, GameObject asteroid2) {}
	
}
