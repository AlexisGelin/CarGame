using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoSingleton<RaceManager>
{
    [SerializeField] List<Checkpoint> _checkpoints;
    [SerializeField] List<Runner> _runners;

    [SerializeField] int _numberOfLap;

    public int NumberOfLap { get => _numberOfLap; }

    public Checkpoint LastCheckpoint;

    public void Init()
    {
        LastCheckpoint = _checkpoints[0];

        foreach (var checkpoint in _checkpoints)
        {
            checkpoint.ResetCheckpoint();
        }
    }

    private void Update()
    {
        _runners.Sort((p1,p2) => p1.ActualCheckpoint.CompareTo(p2.ActualCheckpoint));

    }



    public void UpdateLastCheckpoint(Checkpoint checkpoint)
    {
        LastCheckpoint = checkpoint;
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

    public void RespawnAtLastCheckPoint()
    {
        PlayerController.Instance.transform.position = LastCheckpoint.transform.position + new Vector3(0, 2, 0);
        PlayerController.Instance.transform.rotation = LastCheckpoint.transform.rotation;
        PlayerController.Instance.Rb.velocity = Vector3.zero;
        PlayerController.Instance.Rb.angularVelocity = Vector3.zero;
    }
}
