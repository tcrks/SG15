using UnityEngine;
using SG15._NganNguyenStateMachine;
using System.Collections;
namespace SG15._NganNguyenCore
{
    public class CopyCameraRotation_Char : MonoBehaviour
    {
        [field: SerializeField] private Camera mainCamera;
        [field: SerializeField] private PlayerStateMachine playerStateMachine;
        [field: SerializeField] private float rotaionDamping;
        private bool willLockToCamera;
        private bool enableToLock, unlocking;
        private void Start()
        {
            mainCamera = Camera.main;
        }
        private void Update()
        {
            willLockToCamera = playerStateMachine.willLockToCamera;
            if (mainCamera == null || !willLockToCamera)
            {
                enableToLock = false;
                unlocking = false;
                return;
            }
            else
            {
                if (!unlocking)
                {
                    unlocking = true;
                    StartCoroutine(EnableToLock());
                }
            }
            if (enableToLock)
            {
                Quaternion cameraqQuaternion = mainCamera.transform.rotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, cameraqQuaternion.eulerAngles.y, 0f), rotaionDamping * Time.deltaTime);
            }
        }
        private IEnumerator EnableToLock()
        {
            yield return new WaitForSeconds(0.5f);
            enableToLock = true;
        }
    }
}