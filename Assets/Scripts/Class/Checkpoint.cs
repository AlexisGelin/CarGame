using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    enum Type { Checkpoint, Start }

    [SerializeField] Type type;
    [SerializeField] List<ParticleSystem> particleOnFinishLap;

    bool isPlayerGoesThrough;


    void CheckCheckpoint()
    {
        isPlayerGoesThrough = true;

        RaceManager.Instance.UpdateLastCheckpoint(this);
    }

    public void ResetCheckpoint()
    {
        isPlayerGoesThrough = false;
    }

    public bool GetCheckpointState()
    {
        return isPlayerGoesThrough;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (type == Type.Checkpoint && isPlayerGoesThrough == false)
        {
            foreach (var particle in particleOnFinishLap)
            {
                particle.Play();
            }
        }

        CheckCheckpoint();

        if (type == Type.Start && RaceManager.Instance.CheckForLap())
        {
            foreach (var particle in particleOnFinishLap)
            {
                particle.Play();
            }
        }
    }
}
