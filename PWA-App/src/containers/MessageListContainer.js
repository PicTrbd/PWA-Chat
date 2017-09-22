import React from 'react'
import { connect } from 'react-redux'
import MessageList from '../components/MessageList'

const MessageListContainer = ({ messages, userId }) => (
  <MessageList messages={messages} userId={userId} />
)

const mapStateToProps = state => ({
  messages: state.messages,
  userId: state.userId
})

const MessageListComponent = connect(
  mapStateToProps,
)(MessageListContainer);

export default MessageListComponent;