using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace Game.Client
{
    public class ClientHandle : MonoBehaviour
    {
        public static void Welcome(Packet _packet)
        {
            string _msg = _packet.ReadString();
            int _myId = _packet.ReadInt();

            Debug.Log($"Message from server: {_msg}");
            Client.instance.myId = _myId;
            ClientSend.WelcomeReceived();

            // Now that we have the client's id, connect UDP
            Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);

            ClientSend.CallForMap();
        }

        public static void SpawnPlayer(Packet _packet)
        {
            int _id = _packet.ReadInt();
            string _username = _packet.ReadString();
            Vector3 _position = _packet.ReadVector3();
            Quaternion _rotation = _packet.ReadQuaternion();

            GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
        }

        public static void PlayerDisconnected(Packet _packet)
        {
            int _id = _packet.ReadInt();

            Destroy(GameManager.players[_id].gameObject);
            GameManager.players.Remove(_id);
        }

        public static void LoadMap(Packet _packet)
        {
            int _index = _packet.ReadInt();

            Core.LoadMap(_index);

            ClientSend.ReadyToSpawn();
        }

        public static void PlayerTransform(Packet _packet)
        {
            int _id = _packet.ReadInt();
            Vector3 _position = _packet.ReadVector3();
            Quaternion _rotation = _packet.ReadQuaternion();

            if (GameManager.players.TryGetValue(_id, out PlayerManager _player))
            {
                _player.transform.position = _position;
                _player.transform.rotation = _rotation;
            }
        }
    }
}