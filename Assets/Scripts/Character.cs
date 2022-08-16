using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public event UnityAction OnHealthChange;
    public static event UnityAction<Character> OnDie;

    [SerializeField]
    private string _characterName;
    [SerializeField]
    private int _curHP;
    [SerializeField]
    private int _maxHP;

    [SerializeField]
    private bool _isPlayer;

    public List<CombatAction> combatActions;

    [SerializeField]
    private Character _opponent;

    private Vector3 _startPos;


    void Start()
    {
        _startPos = transform.position;
    }


    public bool IsPlayer()
    {
        return _isPlayer;
    }


    public int GetCurHP()
    {
        return _curHP;
    }


    public int GetMaxHP()
    {
        return _maxHP;
    }


    public void SetOpponent(Character opponent)
    {
        _opponent = opponent;
    }


    public void TakeDamage(int damageToTake)
    {
        _curHP -= damageToTake;

        OnHealthChange?.Invoke();

        if (_curHP <= 0)
            Die();
    }


    void Die()
    {
        OnDie?.Invoke(this);
        Destroy(gameObject);
    }


    public void Heal(int healAmount)
    {
        _curHP += healAmount;

        if (_curHP > _maxHP)
            _curHP = _maxHP;

        OnHealthChange?.Invoke();
    }


    public float GetHealthPercentage()
    {
        return (float)_curHP / (float)_maxHP;
    }


    public void CastCombatAction(CombatAction action)
    {
        if (action.actionType == CombatAction.Type.Attack)
        {
            if (action.projectilePrefab == null) // Melee attack
            {
                StartCoroutine(AttackOpponent(action));
            }
            else // Projectile attack
            {
                GameObject proj = Instantiate(action.projectilePrefab, transform.position, Quaternion.identity);
                proj.GetComponent<Projectile>().Initialize(_opponent, TurnManager.Instance.EndTurn);
            }
        }
        else if (action.actionType == CombatAction.Type.Heal)
        {
            Heal(action.healAmount);
            TurnManager.Instance.EndTurn();
        }
        else
        {
            Debug.LogError("Unrecognised Action Type");
        }
    }


    IEnumerator AttackOpponent(CombatAction combatAction)
    {
        while (transform.position != _opponent.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _opponent.transform.position, 50 * Time.deltaTime);
            yield return null;
        }

        _opponent.TakeDamage(combatAction.Damage);

        while (transform.position != _startPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPos, 20 * Time.deltaTime);
            yield return null;
        }

        TurnManager.Instance.EndTurn();
    }
}
