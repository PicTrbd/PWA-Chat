import React from 'react'
import { store } from '../App'
import { connect } from 'react-redux'
import ChannelList from '../components/ChannelList'
import { changeChannel } from '../actions'

const ChannelListContainer = ({ channels, currentChannel, onCreateChannel, onChangeChannel }) => (
    <ChannelList channels={channels} currentChannel={currentChannel} onCreateChannel={onCreateChannel}
      onChangeChannel={onChangeChannel}/>
)

async function onCreateChannel(socketManager) {
  var channelName = prompt("Entre un nom pour votre banane :");
  if (channelName != null && channelName !== "") {
    try {
      await socketManager.hubProxy.invoke('createroom', channelName);
    } catch (error) {
      console.log("No signalR connection initialized");
    }
  }
}

function onChangeChannel(oldChannel, newChannel, dispatch, socketManager) {
    socketManager.hubProxy.invoke('joinroom', oldChannel.RoomName, newChannel.RoomName, store.getState().userId);
    var newUserList = [];
    newChannel.Users.forEach(function(element) {
      newUserList.push(element.Item2.substring(0, 8));
    }, this);
    dispatch(changeChannel(newChannel, newUserList, newChannel.Messages));
}

const mapStateToProps = state => ({
  channels: state.channels,
  currentChannel: state.currentChannel
})

const mapDispatchToProps = (dispatch, ownProps) => {
    return {
      onCreateChannel: () => {
        onCreateChannel(ownProps.socketManager);
      },
      onChangeChannel: (oldChannel, newChannel) => {
        onChangeChannel(oldChannel, newChannel, dispatch, ownProps.socketManager);
      }
    };
};

const ChannelListComponent = connect(
  mapStateToProps,
  mapDispatchToProps
)(ChannelListContainer);

export default ChannelListComponent;