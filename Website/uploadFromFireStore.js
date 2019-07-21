//(function() {

    // Initialize Firebase
    var config = {
        apiKey: "AIzaSyD4xDQSXbe-qKINz9Q3fASdWVm1Rgm2r4w",
        authDomain: "unityoptics-eafc0.firebaseio.com/",
        databaseURL: "https://unityoptics-eafc0.firebaseio.com/",
        projectId: "unityoptics-eafc0",
        storagebucket: "gs://unityoptics-eafc0.appspot.com",
        messagingSenderId: "1062769710701"
    };
    firebase.initializeApp(config);

    const docRef = firestore.collection("samples").doc("sampleData");
    const outputHeader = document.querySelector('#SampleOutput');
    const inputTextField = document.querySelector('latestSampleOutput');
    const saveButton = document.querySelector('saveButton');
    const loadButton = document.querySelector('loadButton');

    saveButton.addEventListener("click", function() {
        const textToSave = inputTextField.value; 
        console.log("Saving: " + textToSave + " to Firestore");

        docRef.set({
            sampledata: textToSave
        }).then(function() {
            console.log("Status saved!");
        }).catch(function (error) {
            console.log("ERROR: ", error)
        });
    })

    loadButton.addEventListener("click", function() {
        docRef.get().then(function (doc) {
            if (doc && doc.exists) {
                const myData = doc.data();
                outputHeader.innerText = "Sample data status: " + myData.sampledata;
            }
        }).catch(function (error) {
            console.log("ERROR: ", error);
        });
    });

    getRealTimeUpdates = function() {
        docRef.onSnapshot({includeMetadataChanges: true}, function (doc) {
            if (doc && doc.exists) {
                const myData = doc.data();
                console.log("Received document: ", doc);
                outputHeader.innerText = "Sample data status: " + myData.sampledata;
            }
        });
    }

    getRealTimeUpdates();
// })