using UnityEngine;
using Mirror;
namespace JO
{

    /*
        Documentation: https://mirror-networking.com/docs/Components/NetworkRoomManager.html
        API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkRoomManager.html

        See Also: NetworkManager
        Documentation: https://mirror-networking.com/docs/Components/NetworkManager.html
        API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
    */

    /// <summary>
    /// This is a specialized NetworkManager that includes a networked room.
    /// The room has slots that track the joined players, and a maximum player count that is enforced.
    /// It requires that the NetworkRoomPlayer component be on the room player objects.
    /// NetworkRoomManager is derived from NetworkManager, and so it implements many of the virtual functions provided by the NetworkManager class.
    /// </summary>
    [AddComponentMenu("")]
    public class JONetworkRoomManager : NetworkRoomManager
    {
        bool showStartButton;

        #region Server Callbacks

        /// <summary>
        /// This is called on the server when it is told that a client has finished switching from the room scene to a game player scene.
        /// <para>When switching from the room, the room-player is replaced with a game-player object. This callback function gives an opportunity to apply state from the room-player to the game-player object.</para>
        /// </summary>
        /// <param name="conn">The connection of the player</param>
        /// <param name="roomPlayer">The room player object.</param>
        /// <param name="gamePlayer">The game player object.</param>
        /// <returns>False to not allow this player to replace the room player.</returns>
        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            GamePlayer playerScore = gamePlayer.GetComponent<GamePlayer>();
            playerScore.index = roomPlayer.GetComponent<NetworkRoomPlayer>().index;

            return true;
        }

        /// <summary>
        /// This is called on the server when all the players in the room are ready.
        /// <para>The default implementation of this function uses ServerChangeScene() to switch to the game player scene. By implementing this callback you can customize what happens when all the players in the room are ready, such as adding a countdown or a confirmation for a group leader.</para>
        /// </summary>
        public override void OnRoomServerPlayersReady()
        {
            Debug.Log("isHeadless:" + isHeadless);
            if (isHeadless)
            {
                base.OnRoomServerPlayersReady();
            }
            else
            {
                showStartButton = true;
            }
        }

        #endregion


        #region Optional UI

        public override void OnGUI()
        {
            base.OnGUI();

            if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "Start Game"))
            {
                showStartButton = false;

                ServerChangeScene(GameplayScene);
            }
        }

        #endregion
    }

}