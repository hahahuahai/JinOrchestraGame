using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{
    public class GamePlayer : NetworkBehaviour
    {
        [SyncVar]
        public int index;
        [SyncVar]
        public uint score;

        public override void OnStartClient()
        {
            base.OnStartClient();

            int x = index * 6;
            int y = 0;
            transform.localPosition = new Vector3(x, y, 0);
        }

        private void OnGUI()
        {
            GUI.Box(new Rect(10f + (index * 110), 10f, 100f, 25f), score.ToString().PadLeft(10));
        }
    }

}
