using UnityEngine;
using System.Collections;

[System.Serializable]
public class Tile 
{
	public int x;
	public int y;
	public int z;
	public int type;
	public int life;

	public void SetTile(int x, int z, int type)
	{
		this.x = x;
		this.z = z;
		this.type = type;
		this.life = 1;
	}

	public int GetTypeAtCoord()
	{
        return type;
	}

	public Vector3 GetPosition(){

		Vector3 position = new Vector3(this.x, 0, this.z);

		return position;
	}
}
