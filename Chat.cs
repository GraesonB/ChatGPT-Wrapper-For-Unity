using System;
using System.Collections.Generic;

namespace ChatGPTWrapper {
    // Due to OpenAI's new chat completions api, this replaces the old "Prompt" class, but the prompt class is still used for the older models.
    public class Chat
    {
        private string _initialPrompt;
        private  List<Message> _currentChat = new List<Message>();

        public Chat(string initialPrompt) {
            _initialPrompt = initialPrompt;
            Message systemMessage = new Message("system", initialPrompt);
            _currentChat.Add(systemMessage);
        }
        public List<Message> CurrentChat { get { return _currentChat; } }

        public enum Speaker {
            User,
            ChatGPT
        }

        public void AppendMessage(Speaker speaker, string text)
        {
        
            switch (speaker)
            {
                case Speaker.User:
                    Message userMessage = new Message("user", text);
                    _currentChat.Add(userMessage);
                    break;
                case Speaker.ChatGPT:
                    Message chatGPTMessage = new Message("assistant", text);
                    _currentChat.Add(chatGPTMessage);
                    break;
            }
        }
    }
}
