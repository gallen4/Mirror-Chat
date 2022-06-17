using UnityEngine.Events;
using UnityEngine;
using Mirror;

namespace NetworkChat
{
    [System.Serializable]
    public class UserData
    {
        public int Id;
        public string Nickname;

        public UserData(int ID, string nickname)
        {
            Id = ID;
            Nickname = nickname;
        }
    }
       
    public class User : NetworkBehaviour
    {
        public static User Local
        {
            get
            {
                var x = NetworkClient.localPlayer;

                if (x != null)
                    return x.GetComponent<User>();

                return null;
            }
        }

        private UserData userData;
        private UIMessageInputField messageInputField;

        public static UnityAction<int,  string> ReceiveMessageToChat;

        public UserData Data => userData;

        public override void OnStopServer()
        {
            base.OnStopServer();

            UserList.Instance.SvRemoveCurrentUser(Data.Id);
        }

        private void Start()
        {
            messageInputField = UIMessageInputField.Instance;

            userData = new UserData((int) netId, "Nickname");
        }

        private void Update()
        {
            if (!hasAuthority) return;
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat();
            }
        }

        #region Join
        public void JoinToChat()
        {
            Data.Nickname = messageInputField.GetNickname();

            CMDAddUser(Data.Id, Data.Nickname);
        }

        [Command]
        private void CMDAddUser(int id, string nickname)
        {
            UserList.Instance.SvAddCurrentUser(id, nickname);
        }

        [Command]
        private void CMDRemoveUser(int id)
        {
            UserList.Instance.SvRemoveCurrentUser(id);
        }
        #endregion

        #region Chat
        public void SendMessageToChat()
        { 
            if (!hasAuthority) return;  
            if (messageInputField.IsEmpty) return;

            CMDSendMessageToChat(userData.Id, messageInputField.GetString());

            messageInputField.ClearString();
        }

        public void Join()
        {
            NetworkManager.singleton.StartHost();
        }

        public void Exit()
        {
            NetworkManager.singleton.StopHost();
        }

        [Command]
        private void CMDSendMessageToChat(int userID, string message)
        {
            Debug.Log($"User send message to server. Message: {message}");

            SVPostMessage(userID, message);
        }

        [Server]
        private void SVPostMessage(int userID, string message)
        {
            Debug.Log($"Server received message by user. Message: {message}");

            RPCReceiveMessage(userID, message);
        }

        [ClientRpc]
        private void RPCReceiveMessage(int userID, string message)
        {
            Debug.Log($"User received message. Message: {message}");

            ReceiveMessageToChat?.Invoke(userID, message);
        }
        #endregion
    }
}
