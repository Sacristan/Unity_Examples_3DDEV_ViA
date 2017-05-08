using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGuyController : MonoBehaviour
{
    private float speed = 10f;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");

        transform.position += Vector3.right * h * Time.deltaTime * speed;
    }

}
