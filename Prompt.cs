namespace ChatGPTWrapper {
    public class Prompt
    {
        private string _initialPrompt;
        private string _chatbotName;
        public Prompt(string chatbotName, string initialPrompt) {
            _initialPrompt = initialPrompt;
            _chatbotName = chatbotName;
        }
        private string _currentPrompt;
        public string CurrentPrompt { get { return _currentPrompt; } }

        public enum Speaker {
            User,
            Bot
        }

        public void AppendText(Speaker speaker, string text)
        {
            if (_currentPrompt == null) _currentPrompt = _initialPrompt;
            switch (speaker)
            {
                case Speaker.User:
                    _currentPrompt += " \n User: " + text + " \n " + _chatbotName + ": ";
                    break;
                case Speaker.Bot:
                    _currentPrompt += text;
                    break;
            }
        }
    }
}
