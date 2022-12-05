using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        _runners = _runners.OrderByDescending(x => x.ActualCheckpoint).ToList();


        for (int i = 0; i < _runners.Count - 1; i++)
        {
            float distPlayer1 = Vector3.Distance(_runners[i].NextCheckpoint.transform.position, _runners[i].transform.position);
            float distPlayer2 = Vector3.Distance(_runners[i + 1].NextCheckpoint.transform.position, _runners[i + 1].transform.position);

            if (distPlayer1 > distPlayer2)
            {
                var tempRunner = _runners[i + 1];
                _runners[i + 1] = _runners[i];
                _runners[i] = tempRunner;

                i = 0;
            }

        }

        for (int i = 0; i < _runners.Count; i++)
        {
            _runners[i].playerController._UIManager.GameCanvas.SetPlayerRank(i + 1);

        }
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
