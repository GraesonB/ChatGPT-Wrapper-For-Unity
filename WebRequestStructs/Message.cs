using System;
using System.Collections.Generic;

namespace ChatGPTWrapper {

    [Serializable]
    public class Message 
    {
        public string role;
        public string content;

        public Message(string r, string c) {
            role = r;
            content = c;
        }
    }
}