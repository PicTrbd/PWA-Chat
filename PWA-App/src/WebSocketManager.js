import { hubConnection } from 'signalr-no-jquery';

class WebSocketManager {

    initialize(url, hubName, userId) {
        this.connection = hubConnection();
        this.connection.url = url;
        this.hubProxy = this.connection.createHubProxy(hubName);
        this.connection.qs = 'id='.concat(userId);
    }

    addMessage = function(newMessage) {
        var messageList = this.state.messages;
        messageList.push(newMessage);
        this.setState({messages: messageList});
    }

    retrieveRoomDetails = function(roomDetails) {
        var newUserList = [];
        roomDetails.Users.forEach(function(element) {
          newUserList.push(element.Item2.substring(0, 8));
        }, this);
        this.setState({ currentChannel: roomDetails, messages: roomDetails.Messages, users: newUserList });
    }

    retrieveAllRooms = function(rooms) {
        this.setState({ channels : rooms });
    }

    startConnection() {
        this.connection.start()
        .done(function() { 
            console.log('Now connected with ID : ' + this.connection.id)
            this.retrieveMainRoomDetails()
            this.hubProxy.invoke('getAllRooms');
        }.bind(this))
        .fail(function() { console.log('Could not connect')});
    }

    retrieveMainRoomDetails() {
        this.hubProxy.invoke('getRoomDetails', "Main")
    }
}

export default WebSocketManager;