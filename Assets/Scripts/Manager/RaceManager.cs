using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoSingleton<RaceManager>
{
    public List<Checkpoint> _checkpoints;
    [SerializeField] List<Runner> _runners;

    [SerializeField] int _numberOfLap;

    public int NumberOfLap { get => _numberOfLap; }


    public void Init()
    {

        foreach (var checkpoint in _checkpoints)
        {
            checkpoint.ResetCheckpoint();
        }
    }

    private void Update()
    {
        _runners.Sort((p1, p2) => p1.ActualCheckpoint.CompareTo(p2.ActualCheckpoint));

    }






    public bool CheckForLap()
    {
        foreach (var checkpoint in _checkpoints)
        {
            if (checkpoint.GetCheckpointState() == false)
            {
                return false;
            }
        }

        _numberOfLap++;

        UIManager.Instance.GameCanvas.UpdateLapText();

        foreach (var checkpoint in _checkpoints)
        {
            checkpoint.ResetCheckpoint();
        }

        if (NumberOfLap >= 3)
        {
            GameManager.Instance.EndGame();
        }

        return true;
    }

    public void RespawnAtLastCheckPoint(PlayerController player)
    {

        player.transform.position = player.Runner.LastCheckpoint.transform.position + new Vector3(0, 2, 0);
        player.transform.rotation = player.Runner.LastCheckpoint.transform.rotation;
        player.Rb.velocity = Vector3.zero;
        player.Rb.angularVelocity = Vector3.zero;

    }
}
