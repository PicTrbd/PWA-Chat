import { combineReducers } from 'redux'
import { messages } from './messages'
import { users, userId } from './users'
import { channels, currentChannel } from './channels'

const pwaChat = combineReducers({
  messages,
  users,
  userId,
  channels,
  currentChannel
})

export default pwaChat;