import React, { Component } from 'react';
import './styles/App.css';
import WebSocketManager from './WebSocketManager';
import ChatBubbleList from './ChatBubbleList';
import AddChatBubbleForm from './AddChatBubbleForm';
import Cookies from 'universal-cookie';
import guid from 'guid';

class App extends Component {

  constructor(props)
  {
    super(props);
    this.state = { messages : [ ], userId : "" };
  }

  componentDidMount() {
    var cookies = new Cookies();
    var pwaUserId = cookies.get('pwa-user');
    if (pwaUserId === undefined)
    {
      pwaUserId = guid.raw();
      cookies.set('pwa-user', pwaUserId, { path: '/' });
    }
    this.setState({ messages: [ ], userId : pwaUserId });

    this.socketManager = new WebSocketManager();
    this.socketManager.initialize('http://localhost:8080/chat', 'chatHub', pwaUserId);
    this.socketManager.hubProxy.on('addMessage', this.socketManager.addMessage.bind(this))
    this.socketManager.startConnection();
  }

  sendMessage = function(message) {
    this.socketManager.hubProxy.invoke('sendmessage', JSON.stringify(message))
    .done(function(){ console.log('Sent')})
    .fail(function(){ console.log('Fail to send')})
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
          <ChatBubbleList messages={this.state.messages} userId={this.state.userId}/>
        </ol>
        <div id="bottom-area">
          <AddChatBubbleForm sendMessage={this.sendMessage.bind(this)} userId={this.state.userId}/>
        </div>
      </div>
    </div>
    );
  }
}

export default App;