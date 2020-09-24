using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterialColor : MonoBehaviour
{
    [SerializeField] Color colorToSet;

    void Start()
    {
        GetComponent<Renderer>().material.color = colorToSet;
    }

}
