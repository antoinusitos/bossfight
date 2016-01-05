using UnityEngine;
using System.Collections;

public class DestructibleBlockV2 : MonoBehaviour {

	public int life;
	// Use this for initialization
	void Start () {
		life=1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(int theDamage)
	{
		life -= theDamage;
		if (life <= 0)
		{
			GameObject parent = GameObject.Find("LD");
			TileType tileT = TileMapGenerator.instance.tileType[0];
			TileMapGenerator.instance.t = (GameObject) Instantiate(tileT.tile, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
			TileMapGenerator.instance.t.transform.parent = parent.transform;
			Destroy (this.gameObject);
		}
	}
}
