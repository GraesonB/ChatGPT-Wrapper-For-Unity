using System;
using System.Collections.Generic;

namespace ChatGPTWrapper {
    [Serializable]
    public class ChatGPTResError
    {
        public Error error;

    }

    [Serializable]
    public class Error {
      public string message;
    }
}