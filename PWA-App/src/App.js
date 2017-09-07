import React, { Component } from 'react';
import './styles/App.css';
import ChatBubbleList from './ChatBubbleList';
import AddChatBubbleForm from './AddChatBubbleForm'

class App extends Component {

  constructor(props) {
    super(props);
    this.state = { messages : [ ] };
  }

  componentDidMount() {
    this.retrieveMessages()
  }

  retrieveMessages = function () {
    setInterval(function() {
      this.get("messages", {})
      .then((value) => {
        this.setState({ messages: value});
      })
    }.bind(this), 1000)
  }

  handleFetch = function(path, input) {
    input.headers = {'Content-Type': 'application/json'}
    var request = fetch("http://localhost:8080/api/" + path, input)
      .then(response => {
        console.log(response);
        if (response.status === 200)
          return response.json();        
        })
    return request;
  }

  get = function(path, body) {
    return this.handleFetch(path, { method: 'get', mode: 'cors'});
  };

  post = function(path, body) {
    return this.handleFetch(path, { method: 'post', mode: 'cors', body: JSON.stringify(body) });
  };

  addMessage = function(message) {
    var newMessages = this.state.messages;
    newMessages.push(message);
    this.post("message", message).then(() => {
      this.setState({ messages : newMessages });      
    });
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