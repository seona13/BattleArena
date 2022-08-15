using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _healChanceCurve;
    [SerializeField]
    private Character _character;



    void OnEnable()
    {
        TurnManager.Instance.OnBeginTurn += OnBeginTurn;
    }


    void OnDisable()
    {
        TurnManager.Instance.OnBeginTurn -= OnBeginTurn;        
    }


    void OnBeginTurn(Character c)
    {
        if (_character == c)
        {
            DetermineCombatAction();
        }
    }


    void DetermineCombatAction()
    {
        float healthPercentage = _character.GetHealthPercentage();
        bool wantToHeal = Random.value < _healChanceCurve.Evaluate(healthPercentage);

        CombatAction ca = null;

        if (wantToHeal && HasCombatActionOfType(CombatAction.Type.Heal))
        {
            ca = GetCombatActionOfType(CombatAction.Type.Heal);
        }
        else if (HasCombatActionOfType(CombatAction.Type.Attack))
        {
            ca = GetCombatActionOfType(CombatAction.Type.Attack);
        }

        if (ca != null)
        {
            _character.CastCombatAction(ca);
        }
        else
        {
            Debug.LogWarning("Enemy could not find a valid action to cast");
            TurnManager.Instance.EndTurn();
        }
    }


    bool HasCombatActionOfType(CombatAction.Type type)
    {
        return _character.combatActions.Exists(x => x.actionType == type);
    }


    CombatAction GetCombatActionOfType(CombatAction.Type type)
    {
        List<CombatAction> availableActions = _character.combatActions.FindAll(x => x.actionType == type);
        return availableActions[Random.Range(0, availableActions.Count)];
    }
}
