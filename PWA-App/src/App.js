import React, { Component } from 'react';
import './styles/App.css';
import WebSocketManager from './WebSocketManager';
import Cookies from 'universal-cookie';
import guid from 'guid';
import { slide as Menu } from 'react-burger-menu'
import Header from './components/Header';
import pwaChat from './reducers/index';
import { Provider } from 'react-redux';
import { createStore } from 'redux';
import { retrieveUserId } from './actions';
import MessageListContainer from './containers/MessageListContainer';
import ChannelUsersContainer from './containers/ChannelUsersContainer';
import ChannelListContainer from './containers/ChannelListContainer';
import AddMessageContainer from './containers/AddMessageContainer';

let store = createStore(pwaChat);
let socketManager = new WebSocketManager();

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
              <MessageListContainer/>
            </ol>
            <div id="bottom-area">
              <AddMessageContainer socketManager={socketManager}/>
            </div>
          </div>
      </div>
    </Provider>
    );
  }
}

export { store };
export default App;