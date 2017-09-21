const users = (state = [], action) => {
    switch (action.type) {
        case 'CHANGE_CHANNEL':
            return action.users;
        case 'SINGLE_CHANNEL_RETRIEVED':
            return action.users;
        default:
            return state
    }
}

const userId = (state = "", action) => {
    switch (action.type) {
        case 'RETRIEVE_USERID':
            return action.userId;
        default:
            return state
    }
}

export { userId };
export { users };