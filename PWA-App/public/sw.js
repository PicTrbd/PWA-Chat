'use strict';

self.addEventListener('notificationclose', event => {
  console.log("Notification has been closed");
});

self.addEventListener('push', event => {
  var payload = event.data.json(),
   options = {
    body: "\nOwner : "
          .concat(payload.ChannelOwner)
          .concat("\nChannel Name : ")
          .concat(payload.ChannelName),              
    icon: payload.Icon,
  }
  event.waitUntil(self.registration.showNotification("A new channel has been created !", options));
});