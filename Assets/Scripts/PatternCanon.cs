using UnityEngine;
using System.Collections;

public class PatternCanon : MonoBehaviour {

	public Transform bulletPrefab;

	enum ShootType {Type1, Type2}
	ShootType shootType;



	void Start () {
		//transform.position = TileMapGenerator.instance.tileMapCorridor[TileMapGenerator.instance.CorridorLarger-3].GetPosition();

		shootType = ShootType.Type1;

		StartCoroutine(CannonShoot(6.0f));
	}


	IEnumerator CannonShoot(float time)
	{
		while(GameManager.instance.gameState == GameManager.GameState.Tuto)
		{
			StartCoroutine(Shoot(shootType));
			yield return new WaitForSeconds(time);
		}

	}


	IEnumerator Shoot(ShootType type)
	{
		if(shootType == ShootType.Type1)
		{
            Transform bullet1 = Instantiate(bulletPrefab, transform.GetChild(1).transform.position, Quaternion.identity)as Transform;
            bullet1.GetComponent<Bullet>().direction = -transform.forward;
			yield return new WaitForSeconds(2.0f);
            Transform bullet2 = Instantiate(bulletPrefab, transform.GetChild(0).transform.position, Quaternion.identity) as Transform;
            bullet2.GetComponent<Bullet>().direction = -transform.forward;
            Transform bullet3 = Instantiate(bulletPrefab, transform.GetChild(2).transform.position, Quaternion.identity) as Transform;
            bullet3.GetComponent<Bullet>().direction = -transform.forward;

            shootType = ShootType.Type2;
		}
		else if(shootType == ShootType.Type2)
		{
            Transform bullet1 = Instantiate(bulletPrefab, transform.GetChild(0).transform.position, Quaternion.identity)as Transform;
            bullet1.GetComponent<Bullet>().direction = -transform.forward;
            yield return new WaitForSeconds(1.0f);;
            Transform bullet2 = Instantiate(bulletPrefab, transform.GetChild(1).transform.position, Quaternion.identity) as Transform;
            bullet2.GetComponent<Bullet>().direction = -transform.forward;
            yield return new WaitForSeconds(2.5f);
            Transform bullet3 = Instantiate(bulletPrefab, transform.GetChild(2).transform.position, Quaternion.identity) as Transform;
            bullet3.GetComponent<Bullet>().direction = -transform.forward;

            shootType = ShootType.Type1;
		}
	}
}
