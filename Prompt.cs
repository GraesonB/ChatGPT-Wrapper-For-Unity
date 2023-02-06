namespace ChatGPTWrapper {
    public class Prompt
    {
        private string _currentPrompt = "You are ChatGPT, a large language model trained by OpenAI.";
        public string CurrentPrompt
        {
            get { return _currentPrompt; }
        }

        public enum Speaker {
            User,
            ChatGPT
        }

        public void AppendText(Speaker speaker, string text)
        {
            switch (speaker)
            {
                case Speaker.User:
                    _currentPrompt += " \n User: " + text + " \n ChatGPT: ";
                    break;
                case Speaker.ChatGPT:
                    _currentPrompt += text;
                    break;
            }
        }
    }
}
