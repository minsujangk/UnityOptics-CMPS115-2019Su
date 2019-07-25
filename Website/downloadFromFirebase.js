function main() {
    console.log("In main")

    // Init vars 
    var obj1;
    var obj2;
    var obj3;
    var pickup;
    var read;

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
    // var pathReference = storage.ref('gameData/save_20190714210639_AdObj1.json');

    // Create a storage reference from our storage service
    var storageRef = storage.ref();

    // Create a reference from a Google Cloud Storage URI
    // var gsReference = storage.refFromURL('gs://unityoptics-eafc0.appspot.com/gameData/save_20190714210639_AdObj1.json')


    // Variable to store file data
    var jsonVar 

    console.log("BEFORE REQUESTING JSON")
    // Request OBJ1
    storageRef.child('gameData/AdObj1/viewdata_20190723211438.json').getDownloadURL().then(function(url) {
        // `url` is the download URL for 'gameData/viewdata_20190722154001.json'
    
        // This can be downloaded directly:
        var xhr = new XMLHttpRequest();
        xhr.responseType = "string";
        xhr.onload = function(event) {
        jsonVar = xhr.response;
        console.log("ADOBJ1 RESPONSE: " + jsonVar);
        obj1 = jsonVar

        localStorage.setItem("1", obj1)
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

    // Request obj2
    storageRef.child('gameData/AdObj2/viewdata_20190723211438.json').getDownloadURL().then(function(url) {
        // `url` is the download URL for 'gameData/viewdata_20190722154001.json'
    
        // This can be downloaded directly:
        var xhr = new XMLHttpRequest();
        xhr.responseType = "string";
        xhr.onload = function(event) {
        jsonVar = xhr.response;
        console.log("ADOBJ2 RESPONSE: " + jsonVar);
        obj2 = jsonVar
        localStorage.setItem("2", obj2)
        
        };
        xhr.open('GET', url);
        xhr.send();
    });

    // Request obj3
    storageRef.child('gameData/AdObj3/viewdata_20190723211438.json').getDownloadURL().then(function(url) {
        // `url` is the download URL for 'gameData/viewdata_20190722154001.json'
    
        // This can be downloaded directly:
        var xhr = new XMLHttpRequest();
        xhr.responseType = "string";
        xhr.onload = function(event) {
        jsonVar = xhr.response;
        console.log("ADOBJ3 RESPONSE: " + jsonVar);
        obj3 = jsonVar
        localStorage.setItem("3", obj3)

        };
        xhr.open('GET', url);
        xhr.send();
    });

    // Request pickup
    storageRef.child('gameData/AdObj1/pickup/pickup_20190721144705.json').getDownloadURL().then(function(url) {
        // `url` is the download URL for 'gameData/viewdata_20190722154001.json'
    
        // This can be downloaded directly:
        var xhr = new XMLHttpRequest();
        xhr.responseType = "string";
        xhr.onload = function(event) {
        jsonVar = xhr.response;
        console.log("PICKUP RESPONSE: " + jsonVar);
        pickup = jsonVar
        localStorage.setItem("4", pickup)

        };
        xhr.open('GET', url);
        xhr.send();
    });

    // Request read
    storageRef.child('gameData/read/read_20190722151524.json').getDownloadURL().then(function(url) {
        // `url` is the download URL for 'gameData/viewdata_20190722154001.json'
    
        // This can be downloaded directly:
        var xhr = new XMLHttpRequest();
        xhr.responseType = "string";
        xhr.onload = function(event) {
        jsonVar = xhr.response;
        console.log("READ RESPONSE: " + jsonVar);
        read = jsonVar
        localStorage.setItem("5", read)
        newitem = localStorage.getItem(5)
        console.log("Got this back from local storage: " + newitem)
        };
        xhr.open('GET', url);
        xhr.send();
    });
}