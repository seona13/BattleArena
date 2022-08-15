using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combat Action", menuName = "New Combat Action")]
public class CombatAction : ScriptableObject
{
    public enum Type
    {
        Attack,
        Heal
    }

    public string displayName;
    public Type actionType;

    [Header("Damage")]
    public int Damage;
    public GameObject projectilePrefab;

    [Header("Heal")]
    public int healAmount;
}
