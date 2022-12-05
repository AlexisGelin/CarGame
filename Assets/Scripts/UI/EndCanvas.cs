using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text _raceTime;

    public void Init()
    {
        _raceTime.text = "Time : " + UIManager.Instance.GameCanvas.timer + "s";
    }
}
