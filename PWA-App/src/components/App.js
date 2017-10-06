import React from 'react';
import '../styles/App.css';
import { slide as Menu } from 'react-burger-menu'
import { connect } from 'react-redux'
import Header from './Header';
import MessageListContainer from '../containers/MessageListContainer';
import ChannelUsersContainer from '../containers/ChannelUsersContainer';
import ChannelListContainer from '../containers/ChannelListContainer';
import AddMessageContainer from '../containers/AddMessageContainer';

const App = (props) => (

  <div className="mdl-layout mdl-js-layout mdl-layout--fixed-header">
    <Header />
    <Menu right>
      <ChannelListContainer socketManager={props.socketManager}/>
      <ChannelUsersContainer/>
    </Menu>
    <div id="chatbox">
      <MessageListContainer/>
      <AddMessageContainer socketManager={props.socketManager}/>
    </div>
    <div id="bottom-div-scroll"/>
  </div>
)

export default App;
