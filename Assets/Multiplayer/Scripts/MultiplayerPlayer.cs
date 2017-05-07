using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(sendInterval = 1, channel = 0)]
public class MultiplayerPlayer : NetworkBehaviour
{
    private float movementSpeed = 20f;
    private float rotationSpeed = 150f;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletSpawnTransform;

    [SerializeField]
    private Camera playerCamera;

    [SyncVar]
    private Color playerColor;

    private void Update()
    {
        if (!isLocalPlayer) return;

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space)) CmdFire();
    }

    [Command]
    private void CmdFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, bulletSpawnTransform.rotation) as GameObject;

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = bullet.transform.forward * 100f;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    }

    public override void OnStartLocalPlayer()
    {
        if (isLocalPlayer) playerCamera.gameObject.SetActive(true);
        playerColor = new Color(Random.value, Random.value, Random.value);
        GetComponent<Renderer>().material.color = playerColor;
    }
}
