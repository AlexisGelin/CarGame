using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text _lapText, _speedText, _playerPos, _countdownText, _timerText;

    public float timer;
    public bool _isRacing;

    public void Init()
    {
        UpdateLapText();

        timer = 0;
    }

    private void Update()
    {
        if (_isRacing)
        {
            timer += Time.deltaTime;

            _timerText.text = timer.ToString("F2") + "s";
        }
    }

    public void UpdateLapText()
    {
        _lapText.text = "Lap count : " + RaceManager.Instance.NumberOfLap;
    }

    public void UpdateSpeedText(int speed)
    {
        _speedText.text = speed + " m/s";
    }

    public IEnumerator DoStartCountdown()
    {
        _countdownText.text = "3";
        yield return new WaitForSeconds(1f);
        _countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        _countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        _countdownText.text = "GO";
        yield return new WaitForSeconds(1f);
        _countdownText.gameObject.SetActive(false);
    }
}
