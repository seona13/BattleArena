using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoSingleton<TurnManager>
{
    public event UnityAction<Character> OnBeginTurn;
    public event UnityAction<Character> OnEndTurn;

    [SerializeField]
    private Character[] _characters;

    [SerializeField]
    private float _nextTurnDelay = 1.0f;

    private int _curCharacterIndex = -1;

    public Character currentCharacter;

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

        if (_curCharacterIndex >= _characters.Length)
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
            Debug.Log("You win!");
        }
    }
}
