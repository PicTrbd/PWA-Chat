export const updateMessageList = messageList => {
    return {
        type: "UPDATE_MESSAGE_LIST",
        messages: messageList
    }
}

export const allChannelsRetrieved = channels => {
    return {
        type: 'ALL_CHANNELS_RETRIEVED',
        channels : channels
    }
}

export const singleChannelRetrieved = (channel, messages, userList) => {
    return {
        type: 'SINGLE_CHANNEL_RETRIEVED',
        currentChannel : channel,
        messages : messages,
        users : userList
    }
}

export const retrieveUserId = userId => {
    return {
        type: 'RETRIEVE_USERID',
        userId : userId
    }
}

export const changeChannel = (newChannel, userList, messageList) => {
    return {
        type: 'CHANGE_CHANNEL',
        currentChannel : newChannel,
        users : userList,
        messages : messageList
    }
}
