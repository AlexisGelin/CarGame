using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public PlayerController playerController;
    public int ActualCheckpoint;
    public Checkpoint LastCheckpoint, NextCheckpoint;
    public int position;

    private void Start() => LastCheckpoint = RaceManager.Instance._checkpoints[0];
    
    public void UpdateLastCheckpoint(Checkpoint lastCheckpoint, Checkpoint nextCheckpoint, int indexCheckpoint)
    {
        ActualCheckpoint = indexCheckpoint;
        LastCheckpoint = lastCheckpoint;
        NextCheckpoint = nextCheckpoint;
    }



}
