using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] public int networkPort = 4455;
    [SerializeField] public string networkAddress = "localhost";

    [SerializeField] Text ipAddress;

    NetworkManager networkM;
        
    public void Start() {
        networkM = GetComponent<NetworkManager>();
        networkM.networkPort = networkPort;
        networkM.networkAddress = networkAddress;

    }

    public void OnCustomizationButton() {
        SceneManager.LoadScene("ShipCustomMenu");
    }


    public void OnHostButton() {
        networkM.StartHost();
    }

    public void OnJoinGameButton() {
        if (ipAddress.text == "") {
            networkM.networkAddress = "localhost";
        }
        else {
            networkM.networkAddress = ipAddress.text;
        }


        Debug.Log(ipAddress.text);
        Debug.Log(networkM.networkAddress);
        networkM.StartClient();
    }
}
