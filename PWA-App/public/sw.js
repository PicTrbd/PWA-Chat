'use strict';

self.addEventListener('notificationclose', event => {
  console.log("Notification has been closed");
});

self.addEventListener('push', event => {
  var payload = event.data.json();
  if (payload.Type === "NewChannel") {
    var options = {
      body: "\nOwner : "
            .concat(payload.ChannelOwner)
            .concat("\nChannel Name : ")
            .concat(payload.ChannelName),              
      icon: payload.Icon,
    }
    event.waitUntil(self.registration.showNotification("A new channel has been created !", options));
  }
  if (payload.Type === "NewMessage") {
    var options = {
      body: "\nFrom : "
            .concat(payload.From),         
      icon: payload.Icon,
    }
    event.waitUntil(self.registration.showNotification("You have a new message !", options));
  }
});