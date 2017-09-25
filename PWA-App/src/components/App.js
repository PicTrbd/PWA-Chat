import React from 'react';
import '../styles/App.css';
import { slide as Menu } from 'react-burger-menu'
import Header from './Header';
import MessageListContainer from '../containers/MessageListContainer';
import ChannelUsersContainer from '../containers/ChannelUsersContainer';
import ChannelListContainer from '../containers/ChannelListContainer';
import AddMessageContainer from '../containers/AddMessageContainer';
import { socketManager } from '../index'

const App = () => (
  <div className="mdl-layout mdl-js-layout mdl-layout--fixed-header">
    <Header />
    <Menu right>
      <ChannelListContainer socketManager={socketManager}/>
      <ChannelUsersContainer/>
    </Menu>
    <div id="chatbox">
      <MessageListContainer/>
      <AddMessageContainer socketManager={socketManager}/>
    </div>
  </div>
)

export default App;