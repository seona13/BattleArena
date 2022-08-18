using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _roundCounter;



    private void OnEnable()
    {
        TurnManager.Instance.OnRoundBegin += UpdateRoundCounter;
    }


    private void OnDisable()
    {
        TurnManager.Instance.OnRoundBegin -= UpdateRoundCounter;
    }


    void UpdateRoundCounter(int value)
    {
        _roundCounter.text = "Round " + value;
    }
}
