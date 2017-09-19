import React, { Component } from 'react';

class ChannelList extends Component
{
    render() {
        return (
            <div>
                <div className="title-div">
                    <img className="title-icon" alt="icon-channel" src={require('../images/icon-channel.png')}/>
                    <span className="channel-menu-title">Channels</span>
                    <input id="add-channel" type="button" value="+" onClick={this.props.createChannel.bind(this)} />
                </div>
                {
                    this.props.channels.map(function(channel) {
                        if (channel.RoomName === this.props.currentChannel.RoomName)
                            return (<input type="button" disabled key={channel.RoomName} className="menu-item selected-channel" value={channel.RoomName}/>);
                        return (<input type="button" key={channel.RoomName} onClick={() => this.props.changeChannel(this.props.currentChannel, channel)} className="menu-item menu-link" value={channel.RoomName}/>);
                    }.bind(this))
                }
            </div>
        );
    }
}

export default ChannelList;