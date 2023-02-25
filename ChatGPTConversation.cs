using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace ChatGPTWrapper {

    public class ChatGPTConversation : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField]
        private string _apiKey = null;

        enum Model {
            ChatGPT,
            Davinci,
            Curie
        }
        [SerializeField]
        private Model _model = Model.Davinci;
        private string _selectedModel = null;
        [SerializeField]
        private int _maxTokens = 3072;
        [SerializeField]
        private float _temperature = 0.6f;
        
        private string _uri = "https://api.openai.com/v1/completions";
        private List<(string, string)> _reqHeaders;
        

        private Requests requests = new Requests();
        private Prompt _prompt;
        private string _lastUserMsg;
        private string _lastChatGPTMsg;

        [Header("Prompt")]
        [SerializeField]
        private string _chatbotName = "ChatGPT";

        [TextArea(4,6)]
        [SerializeField]
        private string _initialPrompt = "You are ChatGPT, a large language model trained by OpenAI.";

        [Space(15)]
        public UnityStringEvent chatGPTResponse = new UnityStringEvent();


        private void OnEnable()
        {
            _prompt = new Prompt(_chatbotName, _initialPrompt);
            _reqHeaders = new List<(string, string)>
            { 
                ("Authorization", $"Bearer {_apiKey}"),
                ("Content-Type", "application/json")
            };
            switch (_model) {
                case Model.ChatGPT:
                    _selectedModel = null;
                    break;
                case Model.Davinci:
                    _selectedModel = "text-davinci-003";
                    break;
                case Model.Curie:
                    _selectedModel = "text-curie-001";
                    break;
            }
        }

        public void SendToChatGPT(string message)
        {
            if (_selectedModel != null) {
                _lastUserMsg = message;
                _prompt.AppendText(Prompt.Speaker.User, message);

                ChatGPTReq reqObj = new ChatGPTReq();
                reqObj.model = _selectedModel;
                reqObj.prompt = _prompt.CurrentPrompt;
                reqObj.max_tokens = _maxTokens;
                reqObj.temperature = _temperature;
                string json = JsonUtility.ToJson(reqObj);

                StartCoroutine(requests.PostReq<ChatGPTRes>(_uri, json, ResolveResponse, _reqHeaders));
            } else {
                Debug.LogError
                (
                    "It looks like you haven't setup the model name for ChatGPT's API."
                    + " Either select a different model, or add ChatGPT's model name in ChatGPTConversation's OnEnable()" 
                );
            }
        }

        private void ResolveResponse(ChatGPTRes res)
        {
            _lastChatGPTMsg = res.choices[0].text
                .TrimStart('\n')
                .Replace("<|im_end|>", "");

            _prompt.AppendText(Prompt.Speaker.ChatGPT, _lastChatGPTMsg);
            chatGPTResponse.Invoke(_lastChatGPTMsg);
        }
    }
}