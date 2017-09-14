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

    getRoomDetails = function(roomDetails) {
        console.log(roomDetails);
        this.setState({ currentRoom: roomDetails })
    }

    startConnection() {
        this.connection.start()
        .done(function() { 
            console.log('Now connected with ID : ' + this.connection.id)
            this.retrieveMainRoomDetails()
        }.bind(this))
        .fail(function() { console.log('Could not connect')});
    }

    retrieveMainRoomDetails() {
        console.log("Retrive Main Room Details");
        this.hubProxy.invoke('retrieveRoomDetails', "Main")
    }
}

export default WebSocketManager;