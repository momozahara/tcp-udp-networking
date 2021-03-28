using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Core : MonoBehaviour
{
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
        Game.Server.NetworkManager.StartServer();
        Core.LoadMap(Sceneindex);
    }

    public void JoinGame()
    {
        gameObject.AddComponent<Game.Client.Client>();
        gameObject.AddComponent<Game.Client.ClientHandle>();
        gameObject.AddComponent<Game.Client.ClientSend>();
        gameObject.AddComponent<Game.Client.GameManager>();
        Game.Client.Client.ConnectToServer();
    }

    public static void LoadMap(int Sceneindex)
    {
        SceneManager.LoadSceneAsync(Sceneindex, LoadSceneMode.Single);
    }
}