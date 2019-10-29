using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerups;
    [SerializeField] float WaitingTime = 7f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(PowerupSpawnRoutine());
        }

        while (true);
    }

    IEnumerator PowerupSpawnRoutine()
    { 
        int randomPowerup = Random.Range(0, powerups.Length);
		yield return new WaitForSeconds(WaitingTime);
		Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-4.5f, 4.5f), 10, 0), Quaternion.identity); 
    }
}
