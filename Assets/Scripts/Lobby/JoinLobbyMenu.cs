using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField]
    private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField]
    private GameObject background = null;
    [SerializeField]
    private InputField ipAddressInputField = null;
    [SerializeField]
    private Button joinButton = null;
    [SerializeField]
    private Button joinScreenButton = null;

    private void OnEnable()
    {
        ipAddressInputField.Select();
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected -= HandleClientDisconnected;
    }

    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;

        if(ipAddress != "")
        {
            networkManager.networkAddress = ipAddress;
            networkManager.StartClient();

            joinButton.interactable = false;
        }
    }

    private void HandleClientConnected()
    {
        joinButton.interactable = false;
        background.SetActive(false);
    }

    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
        background.SetActive(true);
    }
}
