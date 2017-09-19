import React, { Component } from 'react';

class ChannelUsers extends Component
{
    render() {
        return (
            <div>
                <div className="title-div">
                    <img className="title-icon" alt="icon-user" src={require('../images/icon-user.png')}/>
                    <span className="user-menu-title">Utilisateurs</span>
                </div>
                {
                    this.props.users.map(function(user) {
                        return (<span key={user} className="menu-item">{user}</span>);
                    })
                }
            </div>
        );
    }
}

export default ChannelUsers;