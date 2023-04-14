using UnityEngine;
using System.Collections.Generic;
using Reqs;

namespace ChatGPTWrapper {

    public class ChatGPTConversation : MonoBehaviour
    {
        [SerializeField]
        private bool _useProxy = false;
        [SerializeField]
        private string _proxyUri = null;

        [SerializeField]
        private string _apiKey = null;

        public enum Model {
            ChatGPT,
            Davinci,
            Curie
        }
        [SerializeField]
        public Model _model = Model.ChatGPT;
        private string _selectedModel = null;
        [SerializeField]
        private int _maxTokens = 500;
        [SerializeField]
        private float _temperature = 0.5f;
        
        private string _uri;
        private List<(string, string)> _reqHeaders;
        

        private Requests requests = new Requests();
        private Prompt _prompt;
        private Chat _chat;
        private string _lastUserMsg;
        private string _lastChatGPTMsg;

        [SerializeField]
        private string _chatbotName = "ChatGPT";

        [TextArea(4,6)]
        [SerializeField]
        private string _initialPrompt = "You are ChatGPT, a large language model trained by OpenAI.";

        public UnityStringEvent chatGPTResponse = new UnityStringEvent();

        private void OnEnable()
        {
            
            TextAsset textAsset = Resources.Load<TextAsset>("APIKEY");
            if (textAsset != null) {
                _apiKey = textAsset.text;
            }
            
            
            _reqHeaders = new List<(string, string)>
            { 
                ("Authorization", $"Bearer {_apiKey}"),
                ("Content-Type", "application/json")
            };
            switch (_model) {
                case Model.ChatGPT:
                    _chat = new Chat(_initialPrompt);
                    _uri = "https://api.openai.com/v1/chat/completions";
                    _selectedModel = "gpt-3.5-turbo";
                    break;
                case Model.Davinci:
                    _prompt = new Prompt(_chatbotName, _initialPrompt);
                    _uri = "https://api.openai.com/v1/completions";
                    _selectedModel = "text-davinci-003";
                    break;
                case Model.Curie:
                    _prompt = new Prompt(_chatbotName, _initialPrompt);
                    _uri = "https://api.openai.com/v1/completions";
                    _selectedModel = "text-curie-001";
                    break;
            }
        }

        public void ResetChat(string initialPrompt) {
            switch (_model) {
                case Model.ChatGPT:
                    _chat = new Chat(initialPrompt);
                    break;
                default:
                    _prompt = new Prompt(_chatbotName, initialPrompt);
                    break;
            }
        }

        public void SendToChatGPT(string message)
        {
            _lastUserMsg = message;

            if (_model == Model.ChatGPT) {
                if (_useProxy) {
                    ProxyReq proxyReq = new ProxyReq();
                    proxyReq.max_tokens = _maxTokens;
                    proxyReq.temperature = _temperature;
                    proxyReq.messages = new List<Message>(_chat.CurrentChat);
                    proxyReq.messages.Add(new Message("user", message));

                    string proxyJson = JsonUtility.ToJson(proxyReq);

                    StartCoroutine(requests.PostReq<ChatGPTRes>(_proxyUri, proxyJson, ResolveChatGPT, _reqHeaders));
                } else {
                    ChatGPTReq chatGPTReq = new ChatGPTReq();
                    chatGPTReq.model = _selectedModel;
                    chatGPTReq.max_tokens = _maxTokens;
                    chatGPTReq.temperature = _temperature;
                    chatGPTReq.messages = _chat.CurrentChat;
                    chatGPTReq.messages.Add(new Message("user", message));
            
                    string chatGPTJson = JsonUtility.ToJson(chatGPTReq);
                    
                    StartCoroutine(requests.PostReq<ChatGPTRes>(_uri, chatGPTJson, ResolveChatGPT, _reqHeaders));
                }
                
            } else {

                _prompt.AppendText(Prompt.Speaker.User, message);

                GPTReq reqObj = new GPTReq();
                reqObj.model = _selectedModel;
                reqObj.prompt = _prompt.CurrentPrompt;
                reqObj.max_tokens = _maxTokens;
                reqObj.temperature = _temperature;
                string json = JsonUtility.ToJson(reqObj);

                StartCoroutine(requests.PostReq<GPTRes>(_uri, json, ResolveGPT, _reqHeaders));
            }
        }

        private void ResolveChatGPT(ChatGPTRes res)
        {
            _lastChatGPTMsg = res.choices[0].message.content;
            _chat.AppendMessage(Chat.Speaker.User, _lastUserMsg);
            _chat.AppendMessage(Chat.Speaker.ChatGPT, _lastChatGPTMsg);
            chatGPTResponse.Invoke(_lastChatGPTMsg);
        }

        private void ResolveGPT(GPTRes res)
        {
            _lastChatGPTMsg = res.choices[0].text
                .TrimStart('\n')
                .Replace("<|im_end|>", "");

            _prompt.AppendText(Prompt.Speaker.Bot, _lastChatGPTMsg);
            chatGPTResponse.Invoke(_lastChatGPTMsg);
        }
    }
}
