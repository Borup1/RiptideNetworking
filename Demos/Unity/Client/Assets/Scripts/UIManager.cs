﻿using RiptideNetworking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _singleton;
    public static UIManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(UIManager)} instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }

    [SerializeField] private InputField usernameField;
    [SerializeField] private GameObject connectScreen;

    private void Awake()
    {
        Singleton = this;
    }

    public void ConnectClicked()
    {
        usernameField.interactable = false;
        connectScreen.SetActive(false);

        NetworkManager.Singleton.Connect();
    }

    #region Messages
    public void SendName()
    {
        Message message = new Message((ushort)ClientToServerId.playerName);
        message.Add(usernameField.text);
        NetworkManager.Singleton.Client.SendReliable(message);
    }
    #endregion
}
