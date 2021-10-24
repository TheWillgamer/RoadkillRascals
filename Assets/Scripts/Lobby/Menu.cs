using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private NetworkManagerLobby networkManager = null;

    public void HostLobby()
    {
        networkManager.StartHost();

        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
