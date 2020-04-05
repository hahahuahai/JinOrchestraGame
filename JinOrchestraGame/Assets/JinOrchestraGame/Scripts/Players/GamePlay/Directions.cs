using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JO
{
    public class Directions : NetworkBehaviour
    {
        public float destroyAfter = 3;
        public override void OnStartServer()
        {
            base.OnStartServer();
            Invoke(nameof(DestroySelf), destroyAfter);
        }
        [Server]
        void DestroySelf()
        {
            Debug.Log("自动销毁");
            GameObject.Destroy(gameObject);
        }
    }
}