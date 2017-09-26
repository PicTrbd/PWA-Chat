//import { hubConnection } from 'signalr-no-jquery';
import { store } from './index'
import { allChannelsRetrieved, singleChannelRetrieved, updateMessageList } from './actions'
import * as signalR from '@aspnet/signalr-client'

class WebSocketManager {

    initialize(url, hubName, userId) {
        //url = url + "?token=" + userId
        var transportType = signalR.TransportType.WebSockets;
        var http = new signalR.HttpConnection(url, { transport: transportType, coocu: "foo"});
        this.connection = new signalR.HubConnection(http);
        this.connection.qs = "vfdjokvo<"
        //this.connection = hubConnection();
        //this.connection.url = url;
        //this.hubProxy = this.connection.createHubProxy(hubName);
        //this.connection.qs = 'id='.concat(userId);
    }

    addMessage = function(newMessage) {
        var messageList = [...store.getState().messages, newMessage];
        store.dispatch(updateMessageList(messageList));
    }

    retrieveRoomDetails = function(channelDetails) {
        var newUserList = [];
        channelDetails.Users.forEach(function(element) {
          newUserList.push(element.Item2.substring(0, 8));
        }, this);
        store.dispatch(singleChannelRetrieved(channelDetails, channelDetails.Messages, newUserList));
    }

    retrieveAllRooms = function(channels) {
        store.dispatch(allChannelsRetrieved(channels));
    }

    async startConnection() {
        try {
            console.log(this.connection)
            await this.connection.start()
            console.log('Now connected with ID : ' + this.connection.connection.connectionId);
            //console.log('Now connected with ID : ' + this.connection.id)
            //this.retrieveMainRoomDetails()
            //this.hubProxy.invoke('getAllRooms');
        }
        catch (error) {
            console.log(error);
            console.log("Could not connect");
        }
    }

    retrieveMainRoomDetails() {
        this.hubProxy.invoke('getRoomDetails', "Main")
    }
}

export default WebSocketManager;