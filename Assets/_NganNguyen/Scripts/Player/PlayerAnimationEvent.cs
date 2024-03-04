using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG15._NganNguyenPlayerAnimationEvent
{
    public class PlayerAnimationEvent : MonoBehaviour
    {
        [SerializeField] private GameObject Light;
        public event Action ObjectInvestigate;
        public void HightObjectInvestigate()
        {
            print("Dung' nan' nan'");
        }
        public void LowObjectInvestigate()
        {
            print("Ngoi` moc' moc'");
        }
        public void PopUp()
        {
            ObjectInvestigate?.Invoke();
        }
        public void LightToogle()
        {
            Light.SetActive(!Light.activeSelf);
        }
        public void LightToogleOn()
        {
            StartCoroutine(LightToogleOn_excute());
        }
        public void LightToogleOff()
        {
            Light.SetActive(false);
        }
        IEnumerator LightToogleOn_excute()
        {
            yield return new WaitForSeconds(0.5f);
            Light.SetActive(true);
        }
    }
}
