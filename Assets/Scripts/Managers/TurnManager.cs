using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class TurnManager : MonoSingleton<TurnManager>
{
    public event UnityAction<Character> OnBeginTurn;
    public event UnityAction<Character> OnEndTurn;
    public event UnityAction OnEnemyDied;

    [SerializeField]
    private Character _player;

    private List<Character> _characters;

    [SerializeField]
    private float _nextTurnDelay = 1.0f;

    private int _curCharacterIndex = -1;

    public Character currentCharacter;


    public override void Init()
    {
        base.Init();
        _characters = new List<Character>();
        _characters.Add(_player);
    }


    void OnEnable()
    {
        Character.OnDie += OnCharacterDie;
    }


    void OnDisable()
    {
        Character.OnDie -= OnCharacterDie;        
    }


    void Start()
    {
        BeginNextTurn();
    }


    public void BeginNextTurn()
    {
        _curCharacterIndex++;

        if (_curCharacterIndex >= _characters.Count)
            _curCharacterIndex = 0;

        currentCharacter = _characters[_curCharacterIndex];
        OnBeginTurn?.Invoke(currentCharacter);
    }


    public void EndTurn()
    {
        OnEndTurn?.Invoke(currentCharacter);
        Invoke(nameof(BeginNextTurn), _nextTurnDelay);
    }


    void OnCharacterDie(Character character)
    {
        if (character.IsPlayer())
        {
            Debug.Log("You lost!");
        }
        else
        {
            OnEnemyDied?.Invoke();
        }
    }


    public void RegisterNewEnemy(Character enemy)
    {
        if (_characters.Count > 1)
        {
            _characters.RemoveAt(1);
        }
        _characters.Add(enemy);

        // Let the player take the first turn in the round
        _curCharacterIndex = -1;
    }
}
