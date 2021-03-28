using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Server
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager instance;

        public GameObject playerPrefab;
        public GameObject localPlayerPrefab;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }
        }

        public static void StartServer()
        {
            Server.Start(10, 9999);
        }

        private void OnApplicationQuit()
        {
            Server.Stop();
        }

        public Player InstantiatePlayer()
        {
            return Instantiate(playerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity).GetComponent<Player>();
        }
        public Player InstantiateLocalPlayer()
        {
            return Instantiate(localPlayerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity).GetComponent<Player>();
        }
    }
}