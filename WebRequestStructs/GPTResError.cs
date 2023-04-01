using System;
using System.Collections.Generic;

namespace ChatGPTWrapper {
    [Serializable]
    public struct ChatGPTResError
    {
        public Error error;

    }

    [Serializable]
    public struct Error {
      public string message;
    }
}