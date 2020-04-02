using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class PlayerScore : NetworkBehaviour
    {
        [SyncVar]
        public int index;
        [SyncVar]
        public uint score;

        private void OnGUI()
        {
            GUI.Box(new Rect(10f + (index * 110), 10f, 100f, 25f), score.ToString().PadLeft(10));
        }
    }

}
