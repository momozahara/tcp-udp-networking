using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Server
{
    public class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();

            Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");

            if (_fromClient != _clientIdCheck)
            {
                Debug.Log($"New Player (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
        }

        public static void CallForMap(int _fromClient, Packet _packet)
        {
            ServerSend.LoadMap(_fromClient, SceneManager.GetActiveScene().buildIndex);
        }

        public static void ReadyToSpawn(int _fromClient, Packet _packet)
        {
            string _username = _packet.ReadString();

            Server.clients[_fromClient].SendIntoGame(_username);
        }
    }
}