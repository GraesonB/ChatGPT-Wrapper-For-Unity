using System;
using System.Collections.Generic;

namespace ChatGPTWrapper {
    public struct ChatGPTReq
    {
        public string model;
        public List<Message> messages;
        public int max_tokens;
        public float temperature;
    }
}
