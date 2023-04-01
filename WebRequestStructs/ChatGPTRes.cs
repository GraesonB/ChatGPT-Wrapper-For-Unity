using System;
using System.Collections.Generic;

namespace ChatGPTWrapper {
    [Serializable]
    public struct ChatGPTRes
    {
        public List<ChatGPTChoices> choices;
    }
}
