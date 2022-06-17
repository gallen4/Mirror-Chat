using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Mirror;

namespace NetworkChat
{
    public class UIMessageViewer : MonoBehaviour
    {
        [SerializeField] private UIMessageBox _MessageBox;
        [SerializeField] private Transform _MessagePanel;
        [SerializeField] private UIMessageBox _UserBox; 
        [SerializeField] private Transform _UserListPanel;

        private void Start()
        {
            User.ReceiveMessageToChat += OnReceiveMessageToChat;
            UserList.UpdateUserList += OnUpdateUserList;
        }

        private void OnReceiveMessageToChat(int userID, string message)
        {
            AppendMessage(userID, message);
        } 

        private void AppendMessage(int userID ,string message)
        {
            UIMessageBox messageBox = Instantiate(_MessageBox);

            messageBox.SetText(userID + " : " + message);
            messageBox.transform.SetParent(_MessagePanel);
            messageBox.transform.localScale = Vector3.one;

            if (userID == User.Local.Data.Id)
            {
                messageBox.SetStyleBySelf();
            }
            else
            {
                messageBox.SetStyleBeSender();
            }
        }

        private void OnUpdateUserList(List<UserData> userList)
        {
            for(int i = 0; i < _UserListPanel.childCount; ++i)
            {
                Destroy(_UserListPanel.GetChild(i).gameObject);
            }

            for (int i = 0; i < userList.Count; ++i)
            {
                UIMessageBox messageBox = Instantiate(_UserBox);

                messageBox.SetText(userList[i].Nickname);
                messageBox.transform.SetParent(_UserListPanel);
                messageBox.transform.localScale = Vector3.one;
            }
        }
    }
}
