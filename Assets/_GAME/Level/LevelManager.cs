using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class EnemySpawnData
{
    public Enemy enemy;
    public int count;
    public float spawnDelay;
    public List<Transform> spawnPoints;
}

public class LevelManager : SingletonMonobehavior<LevelManager>
{
    
    [HideInInspector]
    public List<Enemy> enemyList = new();

    public List<EnemySpawnData> _levels;

    IEnumerator EnemySpawnerGroup(EnemySpawnData data)
    {
        for (int i = 0; i < data.count; i++)
        {
            var spawnPointIndex = i % data.spawnPoints.Count;
            var enemyGo = Instantiate(data.enemy, data.spawnPoints[spawnPointIndex].position,Quaternion.identity);
            enemyList.Add(enemyGo);
            
            yield return new WaitForSeconds(data.spawnDelay);
        }
        
    }

    void SpawnAllEnemies()
    {
        foreach (var level in _levels)
        {
            StartCoroutine(EnemySpawnerGroup(level));
        }
    }

    void Awake()
    {
        base.Awake();

        SpawnAllEnemies();
        Debug.Log($"Enemy list count {enemyList.Count}");
    }
    

    public void RemoveEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
        Debug.Log($"remaining enemy count {enemyList.Count}");
    }
    
    public void AddEnemy(Enemy enemy)
    {
        enemyList.Add(enemy);
    }
    
    public void AddEnemyList(List<Enemy> enemy)
    {
        enemyList.AddRange(enemy);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
