using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthbarUI : MonoBehaviour
{
    [SerializeField]
    private Image _healthFill;
    [SerializeField]
    private TextMeshProUGUI _healthText;
    [SerializeField]
    private Character _character;


    void OnEnable()
    {
        _character.OnHealthChange += OnUpdateHealth;
    }


    void OnDisable()
    {
        _character.OnHealthChange -= OnUpdateHealth;
    }


    void Start()
    {
        OnUpdateHealth();
    }


    void OnUpdateHealth()
    {
        _healthFill.fillAmount = _character.GetHealthPercentage();
        _healthText.text = _character.GetCurHP() + " / " + _character.GetMaxHP();
    }
}
