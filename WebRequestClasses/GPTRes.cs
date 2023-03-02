using System;
using System.Collections.Generic;

namespace ChatGPTWrapper {
    [Serializable]
    public class GPTRes
    {
        public string id;
        public List<GPTChoices> choices;

    }
}
