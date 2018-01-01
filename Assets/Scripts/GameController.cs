using UnityEngine;

public class GameController : MonoBehaviour
{
    // Script public variables.
    public SpawnProperties[] spawnProperties;
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < spawnProperties.Length; i++)
        {
            if (Time.time > spawnProperties[i].waitSeconds && !spawnProperties[i].HasSpawned())
            {
                StartCoroutine(spawnProperties[i].enemy.GetComponent<EnemyController>().Spawn(spawnProperties[i].xPosition, spawnProperties[i].yPosition));
                StartCoroutine(spawnProperties[i].enemy.GetComponent<EnemyController>().ShotBack());
                spawnProperties[i].MarkAsSpawned();
            }
        }
	}
}
