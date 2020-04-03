using Mirror;
using System;
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
        [SyncVar(hook = nameof(OnDirectionIndexChanged))]
        public int directionIndex;
        
        public GameObject directionsPool;
        public GameObject spawningPlace;
        public GameObject bottomCenterPoint;

        void OnDirectionIndexChanged(int oldDirectionIndex, int newDirectionIndex)
        {
            GameObject go = Resources.Load<GameObject>("Prefabs/Directions/Directions_" + newDirectionIndex);
            go = GameObject.Instantiate(go);
            go.transform.parent = directionsPool.transform;
            go.transform.localPosition = spawningPlace.transform.localPosition;
            Debug.Log("距离底部还有多远：" + Vector3.Distance(go.transform.localPosition, bottomCenterPoint.transform.localPosition));
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            int x = index * 6;
            int y = 0;
            transform.localPosition = new Vector3(x, y, 0);


            //InvokeRepeating(nameof(GenerateDirections), 1, 1);//每隔一秒调用一次
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            InvokeRepeating(nameof(GenerateDirectionIndex), 1, 1);//每隔一秒调用一次
        }

        [ServerCallback]
        void GenerateDirectionIndex()
        {
            directionIndex = UnityEngine.Random.Range(0, 4);
        }
        
        void GenerateDirections()
        {
            GameObject go = Resources.Load<GameObject>("Prefabs/Directions/Directions_" + directionIndex);
            go = GameObject.Instantiate(go);
            go.transform.parent = directionsPool.transform;
            go.transform.localPosition = spawningPlace.transform.localPosition;
            Debug.Log("距离底部还有多远：" + Vector3.Distance(go.transform.localPosition, bottomCenterPoint.transform.localPosition));
        }

        private void OnGUI()
        {
            GUI.Box(new Rect(10f + (index * 110), 10f, 100f, 25f), score.ToString().PadLeft(10));
        }
    }

}
