import React from 'react';
import ReactDOM from 'react-dom';
import App from './components/App';
import pwaChat from './reducers/index';
import { Provider } from 'react-redux';
import { createStore } from 'redux';
import guid from 'guid';
import Cookies from 'universal-cookie';
import { retrieveUserId } from './actions';
import WebSocketManager from './WebSocketManager';


const applicationServerPublicKey = 'BMiZDeWBmOzC1PVd4FFK5BKFzF36jzlfsOjq4kOLoDfnEgNIuubR1upxNBwgLm5b5c7RAHppSkG9V6ewntGvenw';

let isSubscribed = false;
let swRegistration = null;
let clientId = "";
let store = createStore(pwaChat);
let socketManager = new WebSocketManager();
let cookies = new Cookies();

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
      navigator.serviceWorker.ready.then(x => initialiseUI());
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
      return response.json();    
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
    var subJSObject = JSON.parse(JSON.stringify(subscription)); 
    var auth = subJSObject.keys.auth; 
    var p256dh = subJSObject.keys.p256dh;
    var sub = {endpoint: subscription.endpoint, p256dh: p256dh, auth: auth}
    var subscriptionResult = await handleFetch("https://pwachatpush-api.azurewebsites.net/subscribe", { method: 'post', mode: 'cors', body: JSON.stringify(sub) });
    //var subscriptionResult = await handleFetch("http://localhost:8080/subscribe", { method: 'post', mode: 'cors', body: JSON.stringify(sub) });
    if (subscriptionResult !== undefined) {
      clientId = subscriptionResult.clientId;
      console.log("Old user ID retrieved : " + clientId);
    }
    else {
      clientId = guid.raw();
      console.log("New user ID generated : " + clientId);      
    }
    
    var pwaUserId = cookies.get('pwa-user');
    if (pwaUserId === undefined || pwaUserId === '')
    {    
      pwaUserId = clientId;
      console.log("User ID set : " + pwaUserId);
      initialiseApp(pwaUserId);
    }  
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

function initialiseApp(pwaUserId) {
  cookies.set('pwa-user', pwaUserId, { path: '/' });
  store.dispatch(retrieveUserId(pwaUserId));
  socketManager.initialize('https://pwachatpush-api.azurewebsites.net/chat', 'chatHub', pwaUserId);
  //socketManager.initialize('http://localhost:8080/chat', 'chatHub', pwaUserId);
  socketManager.connection.on('addMessage', socketManager.addMessage);
  socketManager.connection.on('retrievechanneldetails', socketManager.retrieveChannelDetails);
  socketManager.connection.on('retrieveallchannels', socketManager.retrieveAllChannels);
  socketManager.startConnection();

  ReactDOM.render(
    <Provider store={store}>
      <App socketManager={socketManager}/>
    </Provider>, document.getElementById('root'));
}

var pwaUserId = cookies.get('pwa-user');
if (pwaUserId !== undefined && pwaUserId !== '')
{    
  initialiseApp(pwaUserId);
}

export { store };