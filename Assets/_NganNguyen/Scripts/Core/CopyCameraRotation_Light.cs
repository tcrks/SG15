using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG15._NganNguyenStateMachine;
namespace SG15._NganNguyenCore
{
    public class CopyCameraRotation_Light : MonoBehaviour
    {
        [field: SerializeField] private Camera _camera;
        [field: SerializeField] private PlayerStateMachine playerStateMachine;
        private bool willLockToCamera;
        private void Start()
        {
            _camera = Camera.main;
        }
        private void Update()
        {
            Quaternion parentQuaternion = transform.parent.rotation;
            if (Input.GetKeyDown(KeyCode.C))
            {
                playerStateMachine.SetWillLockToCameraView(!willLockToCamera);
            }
            willLockToCamera = playerStateMachine.willLockToCamera;
            if (_camera == null || !willLockToCamera)
            {
                if (transform.rotation.eulerAngles.x != 0)
                {
                    transform.rotation = Quaternion.Euler(0f, parentQuaternion.eulerAngles.y, 0f);
                }
                return;
            }
            Quaternion cameraqQuaternion = _camera.transform.rotation;
            transform.rotation = Quaternion.Euler(cameraqQuaternion.eulerAngles.x, parentQuaternion.eulerAngles.y, 0f);
        }
    }
}