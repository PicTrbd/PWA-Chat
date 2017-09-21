import React from 'react'

const ChannelUsers = ({ users }) => (
    <div>
        <div className="title-div">
            <img className="title-icon" alt="icon-user" src={require('../images/icon-user.png')}/>
            <span className="user-menu-title">Users</span>
        </div>
        {
            users.map(function(user) {
                return (<span key={user} className="menu-item">{user}</span>);
            })
        }
    </div>
)

export default ChannelUsers;