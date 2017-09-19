import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';

ReactDOM.render(<App />, document.getElementById('root'));

const applicationServerPublicKey = 'BMiZDeWBmOzC1PVd4FFK5BKFzF36jzlfsOjq4kOLoDfnEgNIuubR1upxNBwgLm5b5c7RAHppSkG9V6ewntGvenw';

let isSubscribed = false;
let swRegistration = null;

function urlB64ToUint8Array(base64String) {
  const padding = '='.repeat((4 - base64String.length % 4) % 4);
  const base64 = (base64String + padding).replace(/-/g, '+').replace(/_/g, '/');
  const rawData = window.atob(base64);
  const outputArray = new Uint8Array(rawData.length);

  for (let i = 0; i < rawData.length; ++i) {
    outputArray[i] = rawData.charCodeAt(i);
  }
  return outputArray;
}

async function registerServiceWorker() {
  if ('serviceWorker' in navigator && 'PushManager' in window) {
    try {
      var swReg = await navigator.serviceWorker.register('sw.js')
      swRegistration = swReg;
      initialiseUI();
    } catch (error) {
      console.log('ServiceWorker Error', error);
    }
  }
}

async function initialiseUI() {
  if (isSubscribed) {
    unsubscribeUser();
  } 
  else {
    subscribeUser();
    var subscription = await swRegistration.pushManager.getSubscription();
    isSubscribed = !(subscription === null);
  }
}
    
async function handleFetch(path, input) {
  input.headers = {'Content-Type': 'application/json'}
  try {
    var response = await fetch(path, input);
    if (response.status === 200)
      return response;        
  } catch (error) {
    console.log(error);
  }
  return response;
}
  
async function subscribeUser() {
  var applicationServerKey = urlB64ToUint8Array(applicationServerPublicKey);
  try {
  var subscription = await swRegistration.pushManager.subscribe({
    userVisibleOnly: true,
    applicationServerKey: applicationServerKey
  });
  isSubscribed = true;
  handleFetch("http://localhost:8080/subscribe", { method: 'post', mode: 'cors', body: JSON.stringify(subscription) });
  } catch (error) {
    console.log("Failed to subscribe the user : ", error);
  }
}
  
async function unsubscribeUser() {
  try {
    var subscription = await swRegistration.pushManager.getSubscription()
    if (subscription) {
      isSubscribed = false;
      return subscription.unsubscribe();
    }
  } catch (error) {
    console.log('Error unsubscribing', error);
  }
}

registerServiceWorker();