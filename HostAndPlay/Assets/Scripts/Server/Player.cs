using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Server
{
    public class Player : MonoBehaviour
    {
        public int id;
        public string username;

        public void Initialize(int _id, string _username)
        {
            id = _id;
            username = _username;

            if (id == 1)
            {
                gameObject.AddComponent<LocalPlayerController>();
            }
            else
            {
                gameObject.AddComponent<PlayerController>();
            }
        }
    }
}