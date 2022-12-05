using BaseTemplate.Behaviours;
using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using DG.Tweening;

public class CameraSwitch : MonoSingleton<CameraSwitch>
{
    [SerializeField] private string cameraNameToPlayFirst = "MenuCamera";

    [SerializeField] List<CameraItem> cameraItem;

    Dictionary<string, CameraItem> allCinemachine = new Dictionary<string, CameraItem>();

    CinemachineVirtualCamera actualCamera;

    public void Init()
    {
        InitDictionnary();

        actualCamera = allCinemachine[cameraNameToPlayFirst].Camera;

        DOVirtual.Float(actualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.Value, 360, 20, (f) => actualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.Value = f).SetEase(Ease.Linear).SetLoops(-1);
    }

    private void InitDictionnary()
    {
        foreach (CameraItem camItem in cameraItem)
        {
            allCinemachine.Add(camItem.CameraName, camItem);
        }
    }

    public void switchCamera(string nameOfCamera)
    {
        if (allCinemachine.ContainsKey(nameOfCamera))
        {
            if (actualCamera == allCinemachine[cameraNameToPlayFirst].Camera) DOTween.KillAll();
            
            actualCamera.Priority = 0;

            actualCamera = allCinemachine[nameOfCamera].Camera;

            actualCamera.Priority = 1;
        }
        else
        {
            Debug.LogWarning("Camera introuvable");
        }
    }
}
