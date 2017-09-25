import React from 'react'
import { connect } from 'react-redux'
import ChannelUsers from '../components/ChannelUsers'

const ChannelUsersContainer = ({ users }) => (
  <ChannelUsers users={users} />
)

const mapStateToProps = state => ({
  users: state.users
})

const ChannelUsersComponent = connect(
  mapStateToProps,
)(ChannelUsersContainer);

export default ChannelUsersComponent;