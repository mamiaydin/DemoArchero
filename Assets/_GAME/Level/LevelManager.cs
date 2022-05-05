using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : SingletonMonobehavior<LevelManager>
{
    public List<Enemy> enemyList;
    
    void Awake()
    {
        base.Awake();
        enemyList = FindObjectsOfType<Enemy>().ToList();
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
