using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Server
{
    public class LocalPlayerController : MonoBehaviour
    {
        private Player Player;
        private void Start()
        {
            Player = GetComponent<Player>();
        }

        private void Update()
        {
            ServerSend.PlayerTransform(Player);
        }
    }
}