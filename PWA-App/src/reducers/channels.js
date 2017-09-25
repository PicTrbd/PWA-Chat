const channels = (state = [], action) => {
    switch (action.type) {
        case 'ALL_CHANNELS_RETRIEVED':
            return action.channels;
        default:
            return state
    }
}

const currentChannel = (state = {}, action) => {
    switch (action.type) {
        case 'SINGLE_CHANNEL_RETRIEVED':
            return action.currentChannel;
        case 'CHANGE_CHANNEL':
            return action.currentChannel;
        default:
            return state;
    }
}

export { channels };
export { currentChannel };