using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace SG15._NganNguyenCore
{
    public class ForceReceiver : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float drag = 0.3f;
        private float verticalVelocity;
        private Vector3 impact;
        private Vector3 dampingVelocity;
        public Vector3 Movement => impact + Vector3.up * verticalVelocity;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            if (TryGetComponent<NavMeshAgent>(out NavMeshAgent component))
                agent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            if (verticalVelocity < 0f && controller.isGrounded)
            {
                verticalVelocity = Physics.gravity.y * Time.deltaTime;
            }
            else
            {
                verticalVelocity += Physics.gravity.y * 2 * Time.deltaTime;
            }
            impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
            if (agent != null)
            {
                if (impact.sqrMagnitude < 0.2f * 0.2f)
                {
                    impact = Vector3.zero;
                    agent.enabled = true;
                }
            }
        }
        public void AddFocre(Vector3 force)
        {
            impact += force;
            if (agent != null)
            {
                agent.enabled = false;
            }
        }
        public void Jump(float jumpForce)
        {
            verticalVelocity += jumpForce;
        }

        internal void Reset()
        {
            verticalVelocity = 0f;
            impact = Vector3.zero;
        }
    }
}