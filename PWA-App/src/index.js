import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';

ReactDOM.render(<App />, document.getElementById('root'));

const applicationServerPublicKey = 'BHN69JJwAFcESj_leQ2PIO8DhCwu5zwgN4kvzHbG9TB9-xMjQzllA-lBIedNrZklhwnQZMb9vLFlf_AUhTmX9nM';

let isSubscribed = false;
let swRegistration = null;

function urlB64ToUint8Array(base64String) {
  const padding = '='.repeat((4 - base64String.length % 4) % 4);
  const base64 = (base64String + padding)
    .replace(/-/g, '+')
    .replace(/_/g, '/');

  const rawData = window.atob(base64);
  const outputArray = new Uint8Array(rawData.length);

  for (let i = 0; i < rawData.length; ++i) {
    outputArray[i] = rawData.charCodeAt(i);
  }
  return outputArray;
}

if ('serviceWorker' in navigator && 'PushManager' in window) {
    console.log('Service Worker and Push is supported');
    navigator.serviceWorker.register('sw.js')
    .then(function(swReg) {
      console.log('Service Worker is registered', swReg);
      swRegistration = swReg;
      initialiseUI();
    })
    .catch(function(error) {
      console.error('Service Worker Error', error);
    });
  } 
  else {
    console.warn('Push messaging is not supported');
  }
  
  function initialiseUI() {
      if (isSubscribed) {
        unsubscribeUser();
      } else {
        subscribeUser();
  
    // Set the initial subscription value
    swRegistration.pushManager.getSubscription()
    .then(function(subscription) {
      isSubscribed = !(subscription === null);
  
      updateSubscriptionOnServer(subscription);
  
      if (isSubscribed) {
        console.log('User IS subscribed.');
      } else {
        console.log('User is NOT subscribed.');
      }
  
      isPermissionRefused();
    });
  }
}
  
  function subscribeUser() {
    const applicationServerKey = urlB64ToUint8Array(applicationServerPublicKey);
    swRegistration.pushManager.subscribe({
      userVisibleOnly: true,
      applicationServerKey: applicationServerKey
    })
    .then(function(subscription) {
      console.log('User is subscribed.');
  
      updateSubscriptionOnServer(subscription);
  
      isSubscribed = true;
  
      isPermissionRefused();
    })
    .catch(function(err) {
      console.log('Failed to subscribe the user: ', err);
      isPermissionRefused();
    });
  }
  
  function unsubscribeUser() {
    swRegistration.pushManager.getSubscription()
    .then(function(subscription) {
      if (subscription) {
        return subscription.unsubscribe();
      }
    })
    .catch(function(error) {
      console.log('Error unsubscribing', error);
    })
    .then(function() {
      updateSubscriptionOnServer(null);
      console.log('User is unsubscribed.');
      isSubscribed = false;
      isPermissionRefused();
    });
  }
  
  function isPermissionRefused() {
    if (Notification.permission === 'denied') {
        console.log("permission denied");
        updateSubscriptionOnServer(null);
        return;
    }
  }
  
  function updateSubscriptionOnServer(subscription) {
    // send the subscription to the serv
    if (subscription)
        console.log("subscription");
    else
        console.log("no subscription");
    }