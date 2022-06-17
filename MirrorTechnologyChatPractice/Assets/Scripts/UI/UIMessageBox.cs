using UnityEngine.UI;
using UnityEngine;

namespace NetworkChat
{
    public class UIMessageBox : MonoBehaviour
    {
        [SerializeField] private Text m_Text;
        [SerializeField] private Image m_BGImage;
        [SerializeField] private Color m_BGColorForSelf;
        [SerializeField] private Color m_BGColorForSender;

        public void SetText(string text)
        {
            m_Text.text = text;
        }

        public void SetStyleBySelf()
        {
            m_BGImage.color = m_BGColorForSelf;
            m_Text.alignment = TextAnchor.MiddleLeft;
        }

        public void SetStyleBeSender()
        {
            m_BGImage.color = m_BGColorForSender;
            m_Text.alignment = TextAnchor.MiddleRight;
        }
    }
}