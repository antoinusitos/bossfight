using UnityEngine;
using System.Collections;

public class DestructibleBlock : MonoBehaviour {

   private AudioSource crateExplosionSound;

	public int life;
    public GameObject explosionCrate;
	// Use this for initialization
	void Start () {
		life=1;

       crateExplosionSound = SoundManager.instance.crateExplosion.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(int theDamage)
	{
		life -= theDamage;
		if (life <= 0)
		{
           
            Instantiate(explosionCrate, transform.position, Quaternion.identity);
			GameObject parent = GameObject.Find("LD");
			TileType tileT = TileMapGenerator.instance.tileType[0];
			TileMapGenerator.instance.t = (GameObject) Instantiate(tileT.tile, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
			TileMapGenerator.instance.t.transform.parent = parent.transform;
            crateExplosionSound.PlayDelayed(0.3f);
			Destroy (this.gameObject);
		}
	}
}
