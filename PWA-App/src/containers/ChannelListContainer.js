import React from 'react'
import { connect } from 'react-redux'
import ChannelList from '../components/ChannelList'
import { changeChannel } from '../actions'
import guid from 'guid'

const ChannelListContainer = ({ channels, currentChannel, userId, onCreateChannel, onChangeChannel }) => (
    <ChannelList channels={channels} currentChannel={currentChannel} userId={userId} onCreateChannel={onCreateChannel}
      onChangeChannel={onChangeChannel}/>
)

async function onCreateChannel(socketManager) {
  var channelName = prompt("Entre un nom pour votre channel :");
  if (channelName != null && channelName !== "") {
    try {
      await socketManager.connection.invoke('createchannel', channelName);
    } catch (error) {
      console.log("No signalR connection initialized");
    }
  }
}

async function onChangeChannel(oldChannel, newChannel, userId, dispatch, socketManager) {
  try {
    await socketManager.connection.invoke('joinchannel', oldChannel.ChannelName, newChannel.ChannelName, userId);
    var newUserList = [userId.substring(0,8)];
    newChannel.Users.forEach(function(element) {
      newUserList.push(element.ClientId.substring(0, 8));
    }, this);
    dispatch(changeChannel(newChannel, newUserList, newChannel.Messages));
  } catch (error) {
    console.log(error);
  }
}

const mapStateToProps = state => ({
  channels: state.channels,
  currentChannel: state.currentChannel,
  userId: state.userId
})

const mapDispatchToProps = (dispatch, ownProps) => {
    return {
      onCreateChannel: () => {
        onCreateChannel(ownProps.socketManager);
      },
      onChangeChannel: (oldChannel, newChannel, userId) => {
        onChangeChannel(oldChannel, newChannel, userId, dispatch, ownProps.socketManager);
      }
    };
};

const ChannelListComponent = connect(
  mapStateToProps,
  mapDispatchToProps
)(ChannelListContainer);

export default ChannelListComponent;