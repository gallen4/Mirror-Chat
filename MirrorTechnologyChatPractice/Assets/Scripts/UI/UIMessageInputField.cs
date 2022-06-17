using UnityEngine.UI;
using UnityEngine;

namespace NetworkChat
{
    public class UIMessageInputField : MonoBehaviour
    {
        public static UIMessageInputField Instance;

        [SerializeField] private InputField _MessageInputField;
        [SerializeField] private InputField _NicknameInputField;

        private void Awake()
        {
            Instance = this;
        }

        public string GetString()
        {
            return _MessageInputField.text;
        }

        public void ClearString()
        {
            _MessageInputField.text = "";
        }

        public string GetNickname()
        {
            return _NicknameInputField.text;
        }

        public void SendMessageToChat()
        {
            User.Local.SendMessageToChat();
        }

        public void JoinChat()
        {
            User.Local.JoinToChat();
        }

        public void ExitChat()
        {
            User.Local.Exit();
        }
    
        public bool IsEmpty => _MessageInputField.text == "";
    }
}
