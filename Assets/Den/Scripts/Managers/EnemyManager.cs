using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    private List<Enemy> enemies;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        this.enemies = findAllEnemies();
    }

    private void Update()
    {
        
    }

    public void RespawnAllEnemy()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
            enemy.Spawn();
        }
    }

    private List<Enemy> findAllEnemies()
    {
        IEnumerable<Enemy> enemies = FindObjectsOfType<Enemy>();//I guess this traverse all the mono behaviour script classes to find those with IDataPersistence
        return new List<Enemy>(enemies);
    }
}
