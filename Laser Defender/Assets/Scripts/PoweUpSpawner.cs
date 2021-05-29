using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerups;
    [SerializeField] float MaxWaitingTime = 15f;
    [SerializeField] float MinWaitingTime = 5f;
    int Score;
    [SerializeField] int PowerUpCount = 0;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            CheckProgression();
            yield return StartCoroutine(PowerupSpawnRoutine());
        }
        while (true);
    }

    private void CheckProgression()
    {
        Score = FindObjectOfType<GameSession>().GetScore();
        if (Score > 5000 && Score < 15000)
        {
            PowerUpCount = 1;
        }
        else if (Score > 15000 && Score < 25000)
        {
            PowerUpCount = 2;
        }
        else if (Score > 25000 && Score < 50000)
        {
            PowerUpCount = 3;
        }

        else if (Score > 50000)
        {
            PowerUpCount = 4;
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        int randomPowerup = Random.Range(0, PowerUpCount);
		yield return new WaitForSeconds( Random.Range(MinWaitingTime, MaxWaitingTime));
		var powerup=Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-4.5f, 4.5f), 10, 0), Quaternion.identity);
        powerup.transform.parent = transform;
    }
}
