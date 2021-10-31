using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    [SerializeField] private GameObject _charTransform;
    private CinemachineVirtualCamera _closestCam;
    private CinemachineVirtualCamera _oldClosestCam;
    [SerializeField] private CinemachineVirtualCamera _clockCamera;

    private bool _lookingAtClock = false;

    
    //private List<Vector3> _realCamPositions = {};

    
    // Start is called before the first frame update
    void Start()
    {
        _closestCam = cameras[0];
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestCam();
    }

    private void FindClosestCam()
    {
        foreach (var cam in cameras)
        {
            float oldCamDistance = Vector3.Distance(_closestCam.transform.position, _charTransform.transform.position);
            float currCamDistance = Vector3.Distance(cam.transform.position, _charTransform.transform.position);
            if (currCamDistance < oldCamDistance)
            {
                _closestCam.Priority = 0;
                _closestCam = cam;
                Debug.Log("Closer to camera :" + cam.gameObject.name);
            }
            else
            {
                cam.Priority = 0;
            }
            
        }

        _closestCam.Priority = 20;
    }

    private void ZoomToClock()
    {
        _clockCamera.Priority = 40;
    }
}

