using System;
using System.Collections.Generic;

namespace ChatGPTWrapper {
    public struct ProxyReq
    {
        public string model;
        public List<Message> messages;
        public int max_tokens;
        public float temperature;
    }
}
