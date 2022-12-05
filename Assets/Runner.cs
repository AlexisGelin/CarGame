using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public int ActualCheckpoint;
    public Checkpoint LastCheckpoint;


    private void Start()
    {
        LastCheckpoint = RaceManager.Instance._checkpoints[0];

    }

    public void UpdateLastCheckpoint(Checkpoint checkpoint)
    {
        LastCheckpoint = checkpoint;
    }
}
