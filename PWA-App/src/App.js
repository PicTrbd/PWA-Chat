import React, { Component } from 'react';
import './styles/App.css';
import ChatBubbleList from './ChatBubbleList';
import AddChatBubbleForm from './AddChatBubbleForm';
import Cookies from 'universal-cookie';
import guid from 'guid';
import { hubConnection } from 'signalr-no-jquery';

class App extends Component {

  constructor(props)
  {
    super(props);
    this.state = { messages : [ ], userId : "" };

    this.connection = hubConnection();
    this.connection.url = 'http://localhost:8080/chat'
    this.hubProxy = this.connection.createHubProxy('chatHub');
    console.log(this.connection);
    this.hubProxy.on('addMessage', function(json) {
      console.log(json);
    });
    
    this.connection.start()
    .done(function()
    { 
      console.log('Now connected, connection ID=' + this.connection.id);
      console.log(this.hubProxy)
      this.hubProxy.invoke('sendmessage', "Coucou")
    }.bind(this))
    .fail(function(){ console.log('Could not connect'); });
  }

  componentDidMount() {
    var cookies = new Cookies();
    var pwaUserId = cookies.get('pwa-user');
    if (pwaUserId === undefined)
    {
      pwaUserId = guid.raw();
      cookies.set('pwa-user', pwaUserId, { path: '/' });
    }
    this.setState({userId : pwaUserId });
    console.log(this.state);    
  }

  handleFetch = function(path, input) {
    input.headers = {'Content-Type': 'application/json'}
    var request = fetch("http://localhost:8080/api/" + path, input)
      .then(response => {
        if (response.status === 200)
          return response.json();        
        })
    return request;
  }

  addMessage = function(message) {
    var newMessages = this.state.messages;
    newMessages.push(message);
    this.hubProxy.invoke('sendmessage', JSON.stringify(message))
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
          <AddChatBubbleForm addMessage={this.addMessage.bind(this)} userId={this.state.userId}/>
        </div>
      </div>
    </div>
    );
  }
}

export default App;