using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
        Debug.Log("Server Added Player: "+ playerControllerId);

        GameObject player = null;
        PlayerController playerController = null;

        playerController = conn.playerControllers[playerControllerId];
        player = playerController.gameObject;

        if (player != null)
        {
            MultiplayerPlayer multiplayerPlayer = player.GetComponent<MultiplayerPlayer>();
            multiplayerPlayer.GenerateColor();
        }
    }
}
