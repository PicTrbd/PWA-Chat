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
import pwaChat from './reducers/index';
import { Provider } from 'react-redux';
import { createStore } from 'redux';
import { retrieveUserId, changeChannel } from './actions';

let store = createStore(pwaChat);

let unsubscribe = store.subscribe(() => {
  console.log("Modification du state : ");
  console.log(store.getState());
})

class App extends Component {


  componentDidMount() {
    this.setState({});
    
    var cookies = new Cookies();
    var pwaUserId = cookies.get('pwa-user');
    if (pwaUserId === undefined)
    {
      pwaUserId = guid.raw();
      cookies.set('pwa-user', pwaUserId, { path: '/' });
    }
    store.dispatch(retrieveUserId(pwaUserId));

    this.socketManager = new WebSocketManager();
    this.socketManager.initialize('http://localhost:8080/chat', 'chatHub', pwaUserId);
    this.socketManager.hubProxy.on('addMessage', this.socketManager.addMessage);
    this.socketManager.hubProxy.on('retrieveroomdetails', this.socketManager.retrieveRoomDetails);
    this.socketManager.hubProxy.on('retrieveallrooms', this.socketManager.retrieveAllRooms);
    this.socketManager.startConnection();
  }

  async sendMessage(message) {
    try {
      await this.socketManager.hubProxy.invoke('sendmessage', store.getState().currentChannel.RoomName, JSON.stringify(message));
    } catch (error) {
      console.log(error);
    }
  };

  changeChannel(oldChannel, newChannel) {
    this.socketManager.hubProxy.invoke('joinroom', oldChannel.RoomName, newChannel.RoomName, store.getState().userId);
    var newUserList = [];
    newChannel.Users.forEach(function(element) {
      newUserList.push(element.Item2.substring(0, 8));
    }, this);
    store.dispatch(changeChannel(newChannel, newUserList, newChannel.Messages));
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
      <Provider store={store}>      
        <div className="mdl-layout mdl-js-layout mdl-layout--fixed-header">
          <Header />
          <Menu right>
            <ChannelList channels={store.getState().channels} createChannel={this.createChannel.bind(this)} 
                  changeChannel={this.changeChannel.bind(this)} currentChannel={store.getState().currentChannel} />
            <br/>
            <ChannelUsers users={store.getState().users} />
          </Menu>
          <div id="chatbox">
            <ol className="chat">
              <ChatBubbleList messages={store.getState().messages} userId={store.getState().userId}/>
            </ol>
            <div id="bottom-area">
              <AddChatBubbleForm sendMessage={this.sendMessage.bind(this)} userId={store.getState().userId}/>
            </div>
          </div>
      </div>
    </Provider>
    );
  }
}

export { store };
export default App;