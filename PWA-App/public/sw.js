'use strict';

self.addEventListener('fetch', function(event) {

});

self.addEventListener('notificationclose', event => {
  console.log("Notification has been closed");
});

self.addEventListener('push', event => {
  var payload = "";
  try {
    payload = event.data.json();    
  } catch (error) {
    console.log("Error parsing the JSON");
  }

  if (payload.From !== undefined) {
    var options = {
      body: "\nFrom : "
            .concat(payload.From.substring(0, 8)),         
      icon: "https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678092-sign-add-128.png",
    }
    event.waitUntil(self.registration.showNotification("You have a new message !", options));
  }
  else {
    var options = {
      body: "This is a test notification",         
      icon: "https://cdn0.iconfinder.com/data/icons/small-n-flat/24/678092-sign-add-128.png",
    }
    event.waitUntil(self.registration.showNotification("Test notification !!!", options));    
  }
});