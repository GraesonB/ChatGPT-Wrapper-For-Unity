using System;
using System.Collections.Generic;

namespace ChatGPTWrapper {
    [Serializable]
    public struct GPTRes
    {
        public string id;
        public List<GPTChoices> choices;

    }
}
