
'use strict';

const CACHE_VERSION = 1;
let CURRENT_CACHES = {
  offline: 'offline-v' + CACHE_VERSION
};
const OFFLINE_URL = 'offline.html';

function createCacheBustedRequest(url) {
  let request = new Request(url, {cache: 'reload'});
  if ('cache' in request) {
    return request;
  }

  let bustedUrl = new URL(url, self.location.href);
  bustedUrl.search += (bustedUrl.search ? '&' : '') + 'cachebust=' + Date.now();
  return new Request(bustedUrl);
}

self.addEventListener('install', event => {
  event.waitUntil(
    fetch(createCacheBustedRequest(OFFLINE_URL)).then(function(response) {
      return caches.open(CURRENT_CACHES.offline).then(function(cache) {
        return cache.put(OFFLINE_URL, response);
      });
    })
  );
});

self.addEventListener('activate', event => {
  let expectedCacheNames = Object.keys(CURRENT_CACHES).map(function(key) {
    return CURRENT_CACHES[key];
  });

  event.waitUntil(
    caches.keys().then(cacheNames => {
      return Promise.all(
        cacheNames.map(cacheName => {
          if (expectedCacheNames.indexOf(cacheName) === -1) {
            console.log('Deleting out of date cache:', cacheName);
            return caches.delete(cacheName);
          }
        })
      );
    })
  );
});

self.addEventListener('fetch', event => {
  if (event.request.mode === 'navigate' ||
      (event.request.method === 'GET' &&
       event.request.headers.get('accept').includes('text/html'))) {
    event.respondWith(
      fetch(event.request).catch(error => {
        console.log('Fetch failed; returning offline page instead.', error);
        return caches.match(OFFLINE_URL);
      })
    );
  }

});

self.registration.showNotification('Notre belle Cyntia a rejoint le chat', {
  body: "Regardez moi ce corp d'Apollon",
  image: "https://scontent-cdt1-1.xx.fbcdn.net/v/t1.0-9/530211_509034549111205_1866025720_n.jpg?oh=839e8ef32d8a1af3be37412ecf8b6279&oe=5A52885B",
  icon: 'images/icons/icon-128x128.png'
});

self.addEventListener('notificationclose', evt => {
  console.log("Notif has been closed");
});

self.addEventListener('push', event => {
  const title = "New notification";
  const options = {
    body: event.data.text(),
  }
  if (Notification.permission === 'granted')
    event.waitUntil(self.registration.showNotification(title, options));
});