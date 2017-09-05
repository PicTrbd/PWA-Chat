import React, { Component } from 'react';
import './styles/App.css';
import ChatBubbleList from './ChatBubbleList';
import AddChatBubbleForm from './AddChatBubbleForm'

class App extends Component {

  constructor(props) {
    super(props);
    this.state = { messages : { } };
  }

  handleFetch = function(path, input) {
    input.headers = {'Content-Type': 'application/json' }
    var request = fetch("http://localhost:8080/api/" + path, input)
      .then(response => {
        console.log(response);
        if (response.ok) return response.json();
       console.log(test);
      })
    return request;
  }

  post = function(path, body) {
    this.addChatBubble();
    //return this.handleFetch(path, { method: 'post', body: JSON.stringify(body) });
  };

  addMessage = function(message) {
    var timestamp = (new Date()).getTime();
    var newMessages = this.state.messages;
    newMessages['message-' + timestamp] = message;
    this.setState({ messages : newMessages });
   };

  sendMessage = function() {
    var params = 
    {
      sender: "Juju",
      message: "jé mang du ri"
    };
    this.post("message", params);
  };

  render() {
    return (
      <div className="mdl-layout mdl-js-layout mdl-layout--fixed-header">
      <header>
        <div className="mdl-layout--large-screen-only top-bar">
          <h3>Progressive Web App - Chat</h3>
        </div>
        <div className="tab-bar mdl-layout__tab-bar">
          <a className="mdl-layout__tab isActive">PWA - Chat</a>
        </div>
      </header>

      <div id="chatbox">
        <ol className="chat">
          <ChatBubbleList messages={this.state.messages} />
        </ol>
        <div id="bottom-area">
          <AddChatBubbleForm addMessage={this.addMessage.bind(this)}/>
        </div>
      </div>
    </div>
    );
  }
}

export default App;