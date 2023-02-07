using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ChatGPTWrapper {

    public class Requests
    {
        private void SetHeaders(ref UnityWebRequest req, List<(string, string)> headers)
        {
            for (int i = 0; i < headers.Count; i++)
            {
                req.SetRequestHeader(headers[i].Item1, headers[i].Item2);
            }
        }

        public IEnumerator GetReq<T>(string uri, System.Action<T> callback, List<(string, string)> headers = null)
        {
            UnityWebRequest webRequest = new UnityWebRequest(uri, "GET");
            if (headers != null) SetHeaders(ref webRequest, headers);
            webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.disposeDownloadHandlerOnDispose = true;

            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    var responseJson = JsonUtility.FromJson<T>(webRequest.downloadHandler.text);
                    callback(responseJson);
                    break;
            }
            webRequest.Dispose();
            
        }

        public IEnumerator PostReq<T>(string uri, string json, System.Action<T> callback, List<(string, string)> headers = null)
        {
            UnityWebRequest webRequest = new UnityWebRequest(uri, "POST");
            if (headers != null) SetHeaders(ref webRequest, headers);

            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.disposeDownloadHandlerOnDispose = true;
            webRequest.disposeUploadHandlerOnDispose = true;

            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    var responseJson = JsonUtility.FromJson<T>(webRequest.downloadHandler.text);
                    callback(responseJson);
                    break;
            }

            webRequest.Dispose();
        }
    }
}
