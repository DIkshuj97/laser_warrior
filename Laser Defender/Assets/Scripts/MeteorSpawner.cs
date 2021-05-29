using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Meteors;
    [SerializeField] float WaitingTime = 10f;
    int Score;
    [SerializeField] int MeteorCount = 0;

    IEnumerator Start()
    {
        do
        {
            CheckProgression();
            yield return StartCoroutine(MeteorSpawnRoutine());
        }

        while (true);
    }

    private void CheckProgression()
    {
        Score = FindObjectOfType<GameSession>().GetScore();
        if (Score > 5000 && Score < 15000)
        {
            MeteorCount = 1;
        }
        else if (Score > 15000 && Score < 25000)
        {
            MeteorCount = 2;
        }
        else if (Score > 25000 && Score < 40000)
        {
            MeteorCount = 3;
        }

        else if (Score > 40000 && Score < 55000)
        {
            MeteorCount = 4;
        }

        else if (Score > 55000)
        {
            MeteorCount = 5;
        }
    }

    IEnumerator MeteorSpawnRoutine()
    {
        int randomMeteor = Random.Range(0, MeteorCount);
        yield return new WaitForSeconds(Random.Range(0, 5f));
        var meteor= Instantiate(Meteors[randomMeteor], new Vector3(Random.Range(-4.5f, 4.5f), 10, 0), Quaternion.identity);
        meteor.transform.parent = transform;
    }
}
