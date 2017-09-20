import React, { Component } from 'react';
import './styles/App.css';
import WebSocketManager from './WebSocketManager';
import Cookies from 'universal-cookie';
import guid from 'guid';
import { slide as Menu } from 'react-burger-menu'
import ChatBubbleList from './components/ChatBubbleList';
import AddChatBubbleForm from './components/AddChatBubbleForm';
import Header from './components/Header';
import ChannelUsers from './components/ChannelUsers';
import ChannelList from './components/ChannelList';

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

  async sendMessage(message) {
    try {
      await this.socketManager.hubProxy.invoke('sendmessage', this.state.currentChannel.RoomName, JSON.stringify(message));
    } catch (error) {
      console.log("Fail to send");
    }
  };

  changeChannel(oldChannel, newChannel) {
    this.socketManager.hubProxy.invoke('joinroom', oldChannel.RoomName, newChannel.RoomName, this.state.userId);
    var newUserList = [];
    newChannel.Users.forEach(function(element) {
      newUserList.push(element.Item2.substring(0, 8));
    }, this);
    this.setState({ currentChannel : newChannel, users : newUserList, messages : newChannel.Messages });
  }

  async createChannel() {
    var channelName = prompt("Entre un nom pour votre channel :");
    if (channelName != null && channelName !== "") {
      try {
        await this.socketManager.hubProxy.invoke('createroom', channelName);
      } catch (error) {
        console.log("No signalR connection initialized");
      }
    }
  }

  render() {
    return (
      <div className="mdl-layout mdl-js-layout mdl-layout--fixed-header">
        <Header />
        <Menu right>
          <ChannelList channels={this.state.channels} createChannel={this.createChannel.bind(this)} 
                changeChannel={this.changeChannel.bind(this)} currentChannel={this.state.currentChannel} />
          <br/>
          <ChannelUsers users={this.state.users} />
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