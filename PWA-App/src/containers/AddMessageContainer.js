import React from 'react'
import { connect } from 'react-redux'
import AddMessage from '../components/AddMessage'
import guid from 'guid'

const AddMessageContainer = ({ userId, currentChannel, onSendMessage }) => (
    <AddMessage userId={userId} currentChannel={currentChannel} onSendMessage={onSendMessage}/>
)

async function sendMessage(userId, message, currentChannel, socketManager) {
    try {
        var newMessage = 
        {
            Message: message,
            UserId: userId,
            Id: guid.raw(),
            Date: new Date()
        };
        await socketManager.hubProxy.invoke('sendmessage', currentChannel.RoomName, JSON.stringify(newMessage));
    } catch (error) {
      console.log(error);
    }
};

const mapStateToProps = state => ({
    userId: state.userId,
    currentChannel: state.currentChannel
})

const mapDispatchToProps = (dispatch, ownProps) => {
    return {
      onSendMessage: (userId, message, currentChannel) => {
        sendMessage(userId, message, currentChannel, ownProps.socketManager);
      },
    };
};

const AddMessageComponent = connect(
  mapStateToProps,
  mapDispatchToProps
)(AddMessageContainer);

export default AddMessageComponent;