# ChatGPT Unity Wrapper
A Unity wrapper for ChatGPT's unofficially launched official API. This wrapper serves as an easy-to-use tool for those looking to explore ChatGPT applications in game development.

## 🚨***IMPORTANT NOTE***🚨 <br>
\<2023-02-25\> <br/>
As of right now, OpenAI has moved the unreleased model name to something else, and to my knowledge no one has found the new model name. In other words, we are stuck with Davinci until the full release of ChatGPT.

~This project does not include the model name for ChatGPT's unreleased API, you'll have to find that on the web yourself.~ This wrapper also include's Open AI's Davinci GPT-3 model as an option. Davinci is a very capable model and can serve as a placeholder for you until the full release of ChatGPT's API.

## Guide
1. Either download the zip and extract it into an existing Unity project, or clone the repo into your project's Assets directory.

2. Create an empty game object, and add the "ChatGPTConversation" script to it.
![Screenshot 2023-02-05 at 10 27 48 PM](https://user-images.githubusercontent.com/89364458/216890753-4b35b810-5f3e-4212-9591-024968d289ad.png)

3. Add your API key (you can generate your own key on OpenAI's website), and set your parameters.

4. Use the UnityEvent below the parameters to subscribe something to ChatGPT's response (likely a function/method that takes a string as an argument).

5. Create a UnityEvent<string> somewhere else in your project, and add ChatGPTConversation's method "SendToChatGPT" to the newly created event.

Congrats, now whenever your new UnityEvent is invoked, a request will be sent to ChatGPT's API, and whatever is subscribed to ChatGPT's response will be notified as soon as a response is received. Have fun!


Here's a screenshot of a demo I put together with a simple UI. The UI files are included in the repo, however, you'll have to setup the UnityEvents and plugin the UI elements yourself. If anyone is interested I can also upload the whole project file.
![Screenshot 2023-02-05 at 10 47 13 PM](https://user-images.githubusercontent.com/89364458/216893256-efe3d9e2-fb7d-4833-bae5-9dcb0e9d5717.png)


## Credits
This repo gave me the idea to do this in Unity: <br>
https://github.com/acheong08/ChatGPT
