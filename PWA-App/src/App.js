import React, { Component } from 'react';
import './styles/App.css';
import WebSocketManager from './WebSocketManager';
import ChatBubbleList from './ChatBubbleList';
import AddChatBubbleForm from './AddChatBubbleForm';
import Cookies from 'universal-cookie';
import guid from 'guid';
import { slide as Menu } from 'react-burger-menu'

class App extends Component {

  constructor(props)
  {
    super(props);
    this.state = { messages : [ ], userId : "", users : [], channels : [], currentChannel: {} };
  }

  componentDidMount() {
    var cookies = new Cookies();
    var pwaUserId = cookies.get('pwa-user');
    if (pwaUserId === undefined)
    {
      pwaUserId = guid.raw();
      cookies.set('pwa-user', pwaUserId, { path: '/' });
    }
    var userList = this.state.users;
    userList.push(pwaUserId.substring(0, 8));
    this.setState({ messages: [ ], userId : pwaUserId, users : userList, currentChannel: {}});

    this.socketManager = new WebSocketManager();
    this.socketManager.initialize('http://localhost:8080/chat', 'chatHub', pwaUserId);
    this.socketManager.hubProxy.on('addMessage', this.socketManager.addMessage.bind(this));
    this.socketManager.hubProxy.on('retrieveroomdetails', this.socketManager.retrieveRoomDetails.bind(this));
    this.socketManager.hubProxy.on('retrieveallrooms', this.socketManager.retrieveAllRooms.bind(this));
    this.socketManager.startConnection();
  }

  sendMessage = function(message) {
    this.socketManager.hubProxy.invoke('sendmessage', this.state.currentChannel.RoomName, JSON.stringify(message))
    .done(function(){ console.log('Sent')})
    .fail(function(){ console.log('Fail to send')})
   };

   changeChannel(oldChannel, newChannel) {
    this.socketManager.hubProxy.invoke('joinroom', oldChannel.RoomName, newChannel.RoomName, this.state.userId);
    var newUserList = [];
    newChannel.Users.forEach(function(element) {
      newUserList.push(element.Item2.substring(0, 8));
    }, this);
    this.setState({ currentChannel : newChannel, users : newUserList, messages : newChannel.Messages });
   }

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
      <Menu right>
        <div className="title-div">
          <img className="title-icon" alt="icon-channel" src={require('./images/icon-channel.png')}/>
          <span className="channel-menu-title">Channels</span>
        </div>
        {
          this.state.channels.map(function(channel) {
            if (channel.RoomName === this.state.currentChannel.RoomName)
              return (<span key={channel.RoomName} className="menu-item selected-channel">{channel.RoomName}</span>);
            return (<a href="#" key={channel.RoomName} onClick={() => this.changeChannel(this.state.currentChannel, channel)} className="menu-item menu-link">{channel.RoomName}</a>);
          }.bind(this))
        }
        <br/>
        <div className="title-div">
          <img className="title-icon" alt="icon-user" src={require('./images/icon-user.png')}/>
          <span className="user-menu-title">Utilisateurs</span>
        </div>
        {
          this.state.users.map(function(user) {
            return (<span key={user} className="menu-item">{user}</span>);
          })
        }
      </Menu>
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