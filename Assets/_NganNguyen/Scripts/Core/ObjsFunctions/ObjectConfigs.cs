using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG15.ObjectFunction
{

    public class ObjectConfigs : MonoBehaviour, IObjsFunction
    {
        [SerializeField] private ObjectTypes objectTypes;
        [SerializeField] private ObjectsIntereactionType intereactionType;
        public ObjectsIntereactionType GetObjectsIntereactionTypes()
        {
            return intereactionType;
        }

        public void PobUpFunction()
        {
            if (objectTypes == ObjectTypes.IntereactabelObjects)
            {
                print("This is IntereactObjects");
                return;
            }
            if (objectTypes == ObjectTypes.PickableObject)
            {
                print("This is Pickable Object or Breakable Object(One Touch)");
                StartCoroutine(DestroyObject());
                return;
            }
        }
        private IEnumerator DestroyObject()
        {
            transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            GameObject.Destroy(gameObject);
        }
    }
}
