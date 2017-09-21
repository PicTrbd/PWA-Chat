import React from 'react'
import { connect } from 'react-redux'
import ChatBubbleList from '../components/ChatBubbleList'

const ChatBubbleListContainer = ({ messages, userId }) => (
  <ChatBubbleList messages={messages} userId={userId} />
)

const mapStateToProps = state => ({
  messages: state.messages,
  userId: state.userId
})

const ChatBubbleListComponent = connect(
  mapStateToProps,
)(ChatBubbleListContainer);

export default ChatBubbleListComponent;