// Initialize Firebase
var config = {
    apiKey: "AIzaSyD4xDQSXbe-qKINz9Q3fASdWVm1Rgm2r4w",
    authDomain: "unityoptics-eafc0.firebaseio.com/",
    databaseURL: "https://unityoptics-eafc0.firebaseio.com/",
    projectId: "unityoptics-eafc0",
    storageBucket: "gs://unityoptics-eafc0.appspot.com",
    messagingSenderId: "1062769710701"
};
firebase.initializeApp(config);

// Create a reference with an initial file path and name
var storage = firebase.storage();
var pathReference = storage.ref('gameData/AdObj3/viewdata_20190722154001.json');

// Create a storage reference from our storage service
var storageRef = storage.ref();

// Create a reference from a Google Cloud Storage URI
var gsReference = storage.refFromURL('gs://unityoptics-eafc0.appspot.com/gameData/AdObj3/viewdata_20190722154001.json')


// Variable to store file data
var jsonVar 

// Request JSON data
storageRef.child('gameData/AdObj3/viewdata_20190722154001.json').getDownloadURL().then(function(url) {
    // `url` is the download URL for 'gameData/viewdata_20190722154001.json'
  
    // This can be downloaded directly:
    var xhr = new XMLHttpRequest();
    xhr.responseType = 'json';
    xhr.onload = function(event) {
      jsonVar = xhr.response;
    };
    xhr.open('GET', url);
    xhr.send();
  
}).catch(function(error) {
    switch (error.code) {
        case 'storage/object-not-found':
            // File does not exit
            break;
        case 'storage/unauthorized':
            // User doesn't have permission to access the object
            break;
        case 'storage/canceled':
            // User canceled the upload
            break;
        case 'storage/unknown':
            // Unknown error occured
            break;
    }
});

console.log("This is my data: " + jsonVar)