using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancedMaterial : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1f);
    }

}
