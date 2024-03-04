using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLightControl : MonoBehaviour
{
    [SerializeField] private GameObject Light;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            Light.SetActive(!Light.activeSelf);
        }
    }
}
