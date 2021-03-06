using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyBase> enemies;
    public PlayerBrain player;

    public int EnemiesRemaining
    {
        get
        {
            return enemies.Count;
        }
    }

    // Start is called before the first frame update
    public void Init()
    {
        enemies.Clear();

        this.player = FindObjectOfType<PlayerBrain>();

        enemies.AddRange(gameObject.GetComponent<DungeonManager>().currentDungeonRoom.GetComponentsInChildren<EnemyBase>());

        foreach (EnemyBase enemy in enemies)
        {
            enemy.player = this.player;
            enemy.enemyManager = this;
            enemy.Init();
        }
    }
}
