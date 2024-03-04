using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace SG15._NganNguyenCore
{
    public class LoopControl : MonoBehaviour
    {
        [field: SerializeField] private GameObject Player;
        [field: SerializeField] private GameObject Ter1;
        [field: SerializeField] private GameObject Ter2;
        [field: SerializeField] private GameObject CameraDriven;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                CameraDriven.SetActive(false);
                Player.GetComponent<CharacterController>().enabled = false;
                Vector3 newPos = Vector3.zero;
                if (Player.transform.parent == Ter2.transform)
                {
                    Player.transform.parent = Ter1.transform;
                    newPos = Ter1.transform.position - Ter2.transform.position;
                }
                else
                {
                    Player.transform.parent = Ter2.transform;
                    newPos = Ter2.transform.position - Ter1.transform.position;
                }
                Player.transform.Translate(newPos, Space.World);
                Player.GetComponent<CharacterController>().enabled = true;
                CameraDriven.SetActive(true);
            }
        }
    }
}
