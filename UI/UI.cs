using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace ChatGPTWrapper {
    public class UI : MonoBehaviour
    {
        private Requests _requests = new Requests();
        private TextField inputText;
        private Label chatGPTText;


        public UnityEvent<string> onButtonClick = new UnityEvent<string>();

        private void OnEnable()
        {

            // _convo = GetComponent<ChatGPTConversation>();
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            VisualElement box = root.Q<VisualElement>("box");
            Button requestOne = root.Q<Button>("request1");
            Button requestTwo = root.Q<Button>("request2");
            inputText = root.Q<TextField>("input-text");
            chatGPTText = root.Q<Label>("chatgpt-text");


            requestOne.clicked += () => SendMessage();
        }

        void SendMessage()
        {
            onButtonClick.Invoke(inputText.text);
            inputText.value = "";
        }

        public void SetChatGPTText(string msg)
        {
            chatGPTText.text = msg;
        }
    }
}
