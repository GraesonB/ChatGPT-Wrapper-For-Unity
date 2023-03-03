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
        public UnityEvent<string> onGrumpyClick = new UnityEvent<string>();
        public UnityEvent<string> onFriendlyClick = new UnityEvent<string>();

        private void OnEnable()
        {

            // _convo = GetComponent<ChatGPTConversation>();
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            VisualElement box = root.Q<VisualElement>("box");
            Button requestOne = root.Q<Button>("request1");
            Button grumpy = root.Q<Button>("grumpy");
            Button friendly = root.Q<Button>("friendly");
            inputText = root.Q<TextField>("input-text");
            chatGPTText = root.Q<Label>("chatgpt-text");


            requestOne.clicked += () => SendMessage();
            grumpy.clicked += () => SetGrumpy();
            friendly.clicked += () => SetFriendly();
        }

        void SetGrumpy() {
          onGrumpyClick.Invoke("You are not an assistant. You are a very grumpy old man with a very pessimistic worldview inside a video game. Answer every question very negatively.");
        }

        void SetFriendly() {
          onFriendlyClick.Invoke("You are not an assistant. You are a very friendly person with a very optimistic worldview inside a video game.");
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
