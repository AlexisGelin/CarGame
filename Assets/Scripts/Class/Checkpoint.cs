using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    enum Type { Checkpoint, Start }

    [SerializeField] Type type;
    [SerializeField] List<ParticleSystem> particleOnFinishLap;

    bool isPlayerGoesThrough;


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
        if (other.gameObject.layer == 10) other.gameObject.GetComponent<Runner>().UpdateLastCheckpoint(this);

        if (type == Type.Start && RaceManager.Instance.CheckForLap())
        {
            foreach (var particle in particleOnFinishLap)
            {
                particle.Play();
            }
        }
    }
}
