using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace SG15._NganNguyenInput
{
    public class InputsReader : MonoBehaviour, Controls.IPlayerActions
    {

        public Vector2 MovementValue { get; private set; }
        public bool IsRunning { get; private set; } = true;
        public bool isEquipedLight { get; private set; } = false;
        public event Action JumpEvent;
        //public event Action TargetEvent;
        public event Action EquipLightEvent;
        //public event Action CancelTargetEvent;
        public event Action InterReaction;

        private Controls controls;
        private bool targertEnable = true;
        private bool buttonYEnable = true;
        private bool isTargeting;

        private void Start()
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);
            controls.Player.Enable();
        }
        public void OnInterReaction(InputAction.CallbackContext context)
        {
            if (!context.performed) { return; }
            InterReaction?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed) { return; }
            JumpEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnTarget(InputAction.CallbackContext context)
        {
            /* if (!context.performed) return;
             if (!targertEnable) return;
             if (isTargeting)
             {
                 isTargeting = false;
                 targertEnable = false;
                 TargetEvent?.Invoke();
                 StartCoroutine(TargertEnable());
             }
             else
             {
                 isTargeting = true;
                 targertEnable = false;
                 TargetEvent?.Invoke();
                 StartCoroutine(TargertEnable());
             }*/
        }
        private IEnumerator TargertEnable()
        {
            yield return new WaitForSeconds(0.2f);
            if (!targertEnable)
                targertEnable = true;
        }
        public void SetIsTargeting(bool _isTargeting)
        {
            isTargeting = _isTargeting;
        }
        public void OnLook(InputAction.CallbackContext context)
        {

        }

        public void OnRun(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                IsRunning = true;
                return;
            }
            IsRunning = false;
        }

        public void OnUseItem(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!buttonYEnable) return;
            if (isEquipedLight)
            {
                isEquipedLight = false;
                buttonYEnable = false;
                EquipLightEvent?.Invoke();
                StartCoroutine(ButtonYEnable());
            }
            else
            {
                isEquipedLight = true;
                buttonYEnable = false;
                EquipLightEvent?.Invoke();
                StartCoroutine(ButtonYEnable());
            }
        }
        private IEnumerator ButtonYEnable()
        {
            yield return new WaitForSeconds(0.2f);
            if (!buttonYEnable)
                buttonYEnable = true;
        }
    }
}