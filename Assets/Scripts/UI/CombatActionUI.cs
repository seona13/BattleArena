using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatActionUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _visualContainer;
    [SerializeField]
    private Button[] _combatActionButtons;



    void OnEnable()
    {
        TurnManager.Instance.OnBeginTurn += OnBeginTurn;
        TurnManager.Instance.OnEndTurn += OnEndTurn;
    }


    void OnDisable()
    {
        TurnManager.Instance.OnBeginTurn -= OnBeginTurn;
        TurnManager.Instance.OnEndTurn -= OnEndTurn;        
    }


    void OnBeginTurn(Character character)
    {
        if (character.IsPlayer() == false)
            return;

        _visualContainer.SetActive(true);

        for (int i = 0; i < _combatActionButtons.Length; i++)
        {
            if (i < character.combatActions.Count)
            {
                CombatAction ca = character.combatActions[i];
                Button btn = _combatActionButtons[i];

                btn.gameObject.SetActive(true);

                btn.GetComponentInChildren<TextMeshProUGUI>().text = ca.displayName;
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => OnClickCombatAction(ca));
            }
            else
            {
                _combatActionButtons[i].gameObject.SetActive(false);
            }
        }
    }


    void OnEndTurn(Character character)
    {
        _visualContainer?.SetActive(false);
    }


    public void OnClickCombatAction(CombatAction action)
    {
        TurnManager.Instance.currentCharacter.CastCombatAction(action);
    }
}
