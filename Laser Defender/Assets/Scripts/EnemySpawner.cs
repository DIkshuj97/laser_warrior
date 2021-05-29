using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;    
    int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] int waveCount=4;
    int score;
    IEnumerator Start()
    {
        
        do
        {
            CheckProgression();
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private void CheckProgression()
    {
        score = FindObjectOfType<GameSession>().GetScore();
        if (score > 10000 && score < 17000)
        {
            waveCount = 6;
        }

        else if (score > 17000 && score < 25000)
        {
            waveCount = 8;
        }

        else if (score > 25000)
        {
            waveCount = 9;
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int waveindex=startingWave; waveindex<waveCount;waveindex++ )
        {
            var currentWave = waveConfigs[waveindex];
            yield return StartCoroutine(SpwanAllEnemiesInWave(currentWave));
        }
    }

   private  IEnumerator SpwanAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            var newEnemy= Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            newEnemy.transform.parent = transform;
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
