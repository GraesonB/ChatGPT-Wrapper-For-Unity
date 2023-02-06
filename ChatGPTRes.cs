using System;
using System.Collections.Generic;

namespace ChatGPTWrapper {
    [Serializable]
    public class ChatGPTRes
    {
        public string id;
        public List<Choices> choices;

    }
}
