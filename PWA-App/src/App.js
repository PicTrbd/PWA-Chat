import React, { Component } from 'react';
import './styles/App.css';
import WebSocketManager from './WebSocketManager';
import Cookies from 'universal-cookie';
import guid from 'guid';
import { slide as Menu } from 'react-burger-menu'
import AddChatBubbleForm from './components/AddChatBubbleForm';
import Header from './components/Header';
import pwaChat from './reducers/index';
import { Provider } from 'react-redux';
import { createStore } from 'redux';
import { retrieveUserId } from './actions';
import ChatBubbleListContainer from './containers/ChatBubbleListContainer';
import ChannelUsersContainer from './containers/ChannelUsersContainer';
import ChannelListContainer from './containers/ChannelListContainer';

let store = createStore(pwaChat);
let socketManager = new WebSocketManager();

store.subscribe(() => {
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

    socketManager.initialize('http://localhost:8080/chat', 'chatHub', pwaUserId);
    socketManager.hubProxy.on('addMessage', socketManager.addMessage);
    socketManager.hubProxy.on('retrieveroomdetails', socketManager.retrieveRoomDetails);
    socketManager.hubProxy.on('retrieveallrooms', socketManager.retrieveAllRooms);
    socketManager.startConnection();
  }

  async sendMessage(message) {
    try {
      await socketManager.hubProxy.invoke('sendmessage', store.getState().currentChannel.RoomName, JSON.stringify(message));
    } catch (error) {
      console.log(error);
    }
  };

  render() {
    return (
      <Provider store={store}>      
        <div className="mdl-layout mdl-js-layout mdl-layout--fixed-header">
          <Header />
          <Menu right>
            <ChannelListContainer socketManager={socketManager}/>
            <br/>
            <ChannelUsersContainer/>
          </Menu>
          <div id="chatbox">
            <ol className="chat">
              <ChatBubbleListContainer/>
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