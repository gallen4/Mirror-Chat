using UnityEngine.UI;
using UnityEngine;
using Mirror;

namespace NetworkChat
{
    public class CanvasHUD : MonoBehaviour
    {
        public Button buttonHost;
        public Button buttonStopHost;
        public Button buttonStartClient;
        public Button buttonStopClient;

        private void Start()
        {
            buttonHost.onClick.AddListener(ButtonHost);
            buttonStopHost.onClick.AddListener(ButtonStopHost);
            buttonStopClient.onClick.AddListener(ButtonStopClient);
        }
        public void ButtonHost()
        {
            NetworkManager.singleton.StartHost();
        }

        public void ButtonStopHost()
        {
            NetworkManager.singleton.StopHost();
        }

        private void ButtonStopClient()
        {
            NetworkManager.singleton.StopClient();
        }
    }
}
