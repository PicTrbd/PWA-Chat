import { hubConnection } from 'signalr-no-jquery';

class WebSocketManager {

    initialize(url, hubName, userId) {
        this.connection = hubConnection();
        this.connection.url = url;
        this.hubProxy = this.connection.createHubProxy(hubName);
        this.connection.qs = userId;
    }

    addMessage = function(json) {
        var newMessage = JSON.parse(json);
        var messageList = this.state.messages;
        messageList.push(newMessage);
        this.setState({messages: messageList});
      }

    startConnection() {
        this.connection.start()
        .done(function() { console.log('Now connected with ID : ' + this.connection.id)}.bind(this))
        .fail(function() { console.log('Could not connect')});
    }
}

export default WebSocketManager;