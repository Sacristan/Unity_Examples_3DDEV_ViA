using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientGameManager : MonoBehaviour
{
    private static ClientGameManager instance;

    [SerializeField]
    private Canvas playerHudCanvas;

    [SerializeField]
    private RectTransform healthBar;

    public static ClientGameManager Instance { get { return instance; } }
    public Canvas PlayerHudCanvas { get { return playerHudCanvas; } }
    public RectTransform HealthBar { get { return healthBar; } }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Debug.LogError("ERROR: ClientGameManager already initialized. Remove redundant ClientGameManagers");
    }
}
