const messages = (state = [], action) => {
    switch (action.type) {
        case 'UPDATE_MESSAGE_LIST':
            return action.messages;
        case 'CHANGE_CHANNEL':
            return action.messages;
        case 'SINGLE_CHANNEL_RETRIEVED':
            return action.messages;
        default:
            return state
    }
}

export { messages }