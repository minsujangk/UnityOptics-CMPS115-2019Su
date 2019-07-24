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
var pathReference = storage.ref('gameData/save_20190714210639_AdObj1.json');

// Create a storage reference from our storage service
var storageRef = storage.ref();

// Create a reference from a Google Cloud Storage URI
// var gsReference = storage.refFromURL('gs://unityoptics-eafc0.appspot.com/gameData/save_20190714210639_AdObj1.json')


// Variable to store file data
var jsonVar 

// Request JSON data
storageRef.child('gameData/save_20190714210639_AdObj1.json').getDownloadURL().then(function(url) {
    // `url` is the download URL for 'gameData/viewdata_20190722154001.json'
  
    // This can be downloaded directly:
    var xhr = new XMLHttpRequest();
    xhr.responseType = "text";
    xhr.onload = function(event) {
      jsonVar = xhr.response;
      console.log("RESPONSE: " + jsonVar);
    };
    xhr.open('GET', url);
    xhr.send();
  
}).catch(function(error) {
    console.log("Did an error happen?")
    switch (error.code) {
        case 'storage/object-not-found':
            // File does not exit
            console.log("Error: OBJECT-NOT-FOUND")
            break;
        case 'storage/unauthorized':
            // User doesn't have permission to access the object
            console.log("Error: UNAUTHORIZED PERMISSION")
            break;
        case 'storage/canceled':
            // User canceled the upload
            console.log("Error: REQUEST CANCELLED")
            break;
        case 'storage/unknown':
            // Unknown error occured
            console.log("Error: UNKNOWN")
            break;
    }
});