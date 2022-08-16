using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    [SerializeField]
    private Character _opponent;

    [SerializeField]
    private GameObject[] _enemies;

    private int _curEnemy = -1;


    void OnEnable()
    {
        TurnManager.Instance.OnEnemyDied += GetNextEnemy;
    }


    void OnDisable()
    {
        TurnManager.Instance.OnEnemyDied -= GetNextEnemy;        
    }


    void Start()
    {
        GetNextEnemy();
    }


    void GetNextEnemy()
    {
        _curEnemy++;
        LoadEnemy();
    }


    void LoadEnemy()
    {
        GameObject enemyObj = Instantiate(_enemies[_curEnemy]);
        Character enemy = enemyObj.GetComponent<Character>();
        if (enemy == null)
        {
            Debug.LogError("Instantiated Enemy is missing Character component.");
        }
        else
        {
            // Make the player the opponent of this enemy
            enemy.SetOpponent(_opponent);
            // Make this enemy the opponent of the player
            _opponent.GetComponent<Character>().SetOpponent(enemy);
            TurnManager.Instance.RegisterNewEnemy(enemy);
        }
    }
}
