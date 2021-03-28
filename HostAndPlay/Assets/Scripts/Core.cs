using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Core : MonoBehaviour
{
    // Maybe rework this too
    public GameObject Client_LocalPlayer;
    public GameObject Client_Player;

    public GameObject Server_LocalPlayer;
    public GameObject Server_Player;
    //

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 300;

        gameObject.AddComponent<ThreadManager>();
    }

    public void HostGame(int Sceneindex)
    {
        gameObject.AddComponent<Game.Server.NetworkManager>();
        Game.Server.NetworkManager.instance.playerPrefab = Server_Player;
        Game.Server.NetworkManager.instance.localPlayerPrefab = Server_LocalPlayer;
        Game.Server.NetworkManager.StartServer();

        LoadMap(Sceneindex);

        // I'll fix this part later
        Game.Server.Player _player = Game.Server.NetworkManager.instance.InstantiateLocalPlayer();
        Game.Server.Server.clients[1].player = _player;
        _player.Initialize(1, "James");
        DontDestroyOnLoad(_player);
        //
    }

    public void JoinGame()
    {
        gameObject.AddComponent<Game.Client.Client>();
        gameObject.AddComponent<Game.Client.ClientHandle>();
        gameObject.AddComponent<Game.Client.ClientSend>();
        gameObject.AddComponent<Game.Client.GameManager>();

        Game.Client.GameManager.instance.localPlayerPrefab = Client_LocalPlayer;
        Game.Client.GameManager.instance.playerPrefab = Client_Player;

        Game.Client.Client.ConnectToServer();
    }

    public static void LoadMap(int Sceneindex)
    {
        // I'll add AsyncOperation later
        SceneManager.LoadSceneAsync(Sceneindex, LoadSceneMode.Single);
    }
}