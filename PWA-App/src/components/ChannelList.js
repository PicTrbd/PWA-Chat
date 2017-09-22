import React from 'react'

const ChannelList = ({ channels, currentChannel, userId, onCreateChannel, onChangeChannel }) => (
    <div>
        <div className="title-div">
            <img className="title-icon" alt="icon-channel" src={require('../images/icon-channel.png')}/>
            <span className="channel-menu-title">Channels</span>
            <input id="add-channel" type="button" value="+" onClick={onCreateChannel}/>
        </div>
        {
            channels.map(function(channel) {
                if (channel.RoomName === currentChannel.RoomName)
                    return (<input type="button" disabled key={channel.RoomName} className="menu-item selected-channel" value={channel.RoomName}/>);
                return (<input type="button" key={channel.RoomName} onClick={() => onChangeChannel(currentChannel, channel, userId)} className="menu-item menu-link" value={channel.RoomName}/>);
            })
        }
    </div>
)

export default ChannelList;