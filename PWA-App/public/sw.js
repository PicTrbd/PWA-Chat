'use strict';

self.addEventListener('notificationclose', event => {
  console.log("Notification has been closed");
});

self.addEventListener('push', event => {
  var payload = event.data;
  if (event.data.From !== undefined) {
    var options = {
      body: "\nFrom : "
            .concat(payload.From),         
      icon: "https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678092-sign-add-128.png",
    }
    event.waitUntil(self.registration.showNotification("You have a new message !", options));
  }
  else {
    var options = {
      body: "This is a test notification",         
      icon: "https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678092-sign-add-128.png",
    }
    event.waitUntil(self.registration.showNotification("Test notification !", options));    
  }
});