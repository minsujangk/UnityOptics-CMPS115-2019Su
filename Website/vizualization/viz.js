

function visualizeData() {
    // Load from local storage, abusing DOM window class
    obj1String = localStorage.getItem("1");
    obj2String = localStorage.getItem("2");
    obj3String = localStorage.getItem("3");
    pickupString = localStorage.getItem("4");
    readString = localStorage.getItem("5");
    window.obj1 = JSON.parse(window.obj1String)
    window.obj2 = JSON.parse(window.obj2String)
    window.obj3 = JSON.parse(window.obj3String)
    window.pickup = JSON.parse(window.pickupString)
    window.read = JSON.parse(window.readString)

    console.log("READ FROM LOCAL STORAGE: " + typeof(window.read))

    /** Load google charts */
    google.charts.load('current', {'packages':['corechart', 'bar']});
    google.charts.setOnLoadCallback(drawChart);
    google.charts.setOnLoadCallback(drawBar);
    google.charts.setOnLoadCallback(drawDonut);
    google.charts.setOnLoadCallback(drawHorizontalBar);

    /**constant variables for the width and height*/
    var WIDTH  = 550;
    var HEIGHT = 400;

    var interactionData = {
        totalThrows : 0.0,
        totalPickups : 0.0,
    
        /**
         *This function adds 1 to the counter for total throws.
        */
        addToThrowCount : function() {
            this.totalThrows += 1;
        },
    
        /**
         *This function adds 1 to the counter for total pick up interactions.
        */
        addToPickUpCount : function() {
            this.totalPickups += 1;
        }
    }

    /**This script calculates data from our json files in order to use later
     *in visualizations.
    */
    var obj1Data = {
        viewTime : 0.0,
        min : 0.0,
        max : 0.0,
    
        /**
         *This function finds a minimum distance from the ad.
        *@param {double} minimum This is a minimum value to be compared.
        */
        calcMin : function(minimum) {
        if(this.min == 0.0) {
            this.min = minimum;
        }else if(this.min > minimum) {
            this.min = minimum;
        }
        },
    
        /**
         *This function finds a maximum distance from the ad.
        *@param {double} maximum This is the maximum value to be compared.
        */
        calcMax : function(maximum) {
        if(this.max == 0.0) {
            this.max = maximum;
        }else if(this.max < maximum) {
            this.max = maximum;
        }
        },
    
        /**
         *This function calculates the total view time.
        *@param {double} time This is the time to add to viewTime.
        */
        calcTime : function(time) {
        this.viewTime += time;
        }
    }
    
    var obj2Data = {
        viewTime : 0.0,
        min : 0.0,
        max : 0.0,
    
        /**
         *This function finds a minimum distance from the ad.
        *@param {double} minimum This is a minimum value to be compared.
        */
        calcMin : function(minimum) {
        if(this.min == 0.0) {
            this.min = minimum;
        }else if(this.min > minimum) {
            this.min = minimum;
        }
        },
    
        /**
         *This function finds a maximum distance from the ad.
        *@param {double} maximum This is the maximum value to be compared.
        */
        calcMax : function(maximum) {
        if(this.max == 0.0) {
            this.max = maximum;
        }else if(this.max < maximum) {
            this.max = maximum;
        }
        },
    
        /**
         *This function calculates the total view time.
        *@param {double} time This is the time to add to viewTime.
        */
        calcTime : function(time) {
        this.viewTime += time;
        }
    }
    
    var obj3Data = {
        viewTime : 0.0,
        min : 0.0,
        max : 0.0,
    
        /**
         *This function finds a minimum distance from the ad.
        *@param {double} minimum This is a minimum value to be compared.
        */
        calcMin : function(minimum) {
        if(this.min == 0.0) {
            this.min = minimum;
        }else if(this.min > minimum) {
            this.min = minimum;
        }
        },
    
        /**
         *This function finds a maximum distance from the ad.
        *@param {double} maximum This is the maximum value to be compared.
        */
        calcMax : function(maximum) {
        if(this.max == 0.0) {
            this.max = maximum;
        }else if(this.max < maximum) {
            this.max = maximum;
        }
        },
    
        /**
         *This function calculates the total view time.
        *@param {double} time This is the time to add to viewTime.
        */
        calcTime : function(time) {
        this.viewTime += time;
        }
    }

    /** Draw the chart and set the chart values*/
    function drawChart() {
    var data = google.visualization.arrayToDataTable([
    ['Objects', 'per Time Viewed'],
    ['Object #1', obj1Data.viewTime],
    ['Object #2', obj2Data.viewTime],
    ['Object #3', obj3Data.viewTime]
    ]);

    /** adds a title, and sets width and height*/
    var options = {'title':'View Time per Object', 'width':WIDTH, 'height':HEIGHT};

    /** Display the chart inside the <div> element with id="viewchart" */
    var chart = new google.visualization.PieChart(document.getElementById('pie_chart_div'));
    chart.draw(data, options);
    }

    function drawBar() {
    var data = google.visualization.arrayToDataTable([
    ['Plugin Objects', 'Min', 'Max'],
    ['#1', obj1Data.min, obj1Data.max],
    ['#2', obj2Data.min, obj2Data.max],
    ['#3', obj3Data.min, obj3Data.max]
    ]);

    var options = {
        title: 'Minimum and Maximum Distance From Player',
        width: WIDTH,
        height: HEIGHT
    };

    var chart = new google.charts.Bar(document.getElementById('bar_graph_div'));
    chart.draw(data, google.charts.Bar.convertOptions(options));
    }

    function drawDonut() {
    var data = google.visualization.arrayToDataTable([
    ['Task', 'Hours per Day'],
    ['Time spent viewing Ad',     adData.durationOfAdView],
    ['Time NOT spent viewing Ad', (adData.elapsedTime-adData.durationOfAdView)]
    ]);

    var options = {
        title: 'Time View Breakdown',
        pieHole: 0.4,
        width: WIDTH,
        height: HEIGHT,
        slices : { 1: {offset: 0.2},
        },
    };

    var chart = new google.visualization.PieChart(document.getElementById('donut_graph_div'));
    chart.draw(data, options);
    }

    function drawHorizontalBar() {
            var data = google.visualization.arrayToDataTable([
            ["Action", "Interactions", { role: "style" } ],
            ["Picked Up", interactionData.totalPickups, "#11f7ff"],
            ["Thrown", interactionData.totalThrows, "#ff5011"]
            ]);

            var view = new google.visualization.DataView(data);
            view.setColumns([0, 1,
                            { calc: "stringify",
                            sourceColumn: 1,
                            type: "string",
                            role: "annotation" },
                            2]);

            var options = {
            title: "Object Interaction",
            width: WIDTH,
            height: HEIGHT,
            bar: {groupWidth: "95%"},
            legend: { position: "none" },
            };
            var chart = new google.visualization.BarChart(document.getElementById("horiz_bar_graph_div"));
            chart.draw(view, options);
        }


    var adData = {
    elapsedTime : 0.0,
    durationOfAdView : 0.0,

    /**
     *This function sets the elapsed time.
    *@param {double} time This is the total elapsed time to be set.
    */
    setElapsedTime : function(time) {
        this.elapsedTime = time;
    },

    /**
     *This function maintains a running total of time spent reading the ad.
    *@param {double} duration This is the total time to be added
    */
    addToAdViewTime : function(duration) {
        this.durationOfAdView += duration;
    }
    }

    /**gathers all the related ad data from the json object*/
    for(var i = 0; i < window.read.data.length; i++) {
    /**This 'read.data.length' is just a stand in for the json object we get
     *from the read data in firebase.
    */
    
    adData.setElapsedTime(window.read.data[i].time);

    adData.addToAdViewTime(window.read.data[i].duration);
    }


    /**gathers all the realted interaction data from the json data in the  pickup folder on firebase*/
    for(var i = 0; i < window.pickup.data.length; i++) {
    /**
     *The 'pickup.data.length' above is a placeholder for the json data in the
    *pickup folder on firebase.
    */

    if(pickup.data[i].type == 1) {
        /**object has been picked up*/
        interactionData.addToPickUpCount();

    }else {
        /**object has been thrown (data[i].type == 2)*/
        interactionData.addToThrowCount();
    }
    }

    //--------------OBJ1--------------------//
    //calculates total view time for Obj1
    for(var i = 0; i < window.obj1.data.length; i++) {
    obj1Data.calcTime(window.obj1.data[i].time);

    //calculates min distance
    obj1Data.calcMin(window.obj1.data[i].minDist);

    //calculates max distance
    obj1Data.calcMax(window.obj1.data[i].maxDist);
    }
    //--------------OBJ2--------------------//
    //calculates total view time for Obj2
    for(var i = 0; i < window.obj2.data.length; i++) {
    obj2Data.calcTime(window.obj2.data[i].time);

    //calculates min distance
    obj2Data.calcMin(window.obj2.data[i].minDist);

    //calculates max distance
    obj2Data.calcMax(window.obj2.data[i].maxDist);
    }

    //--------------OBJ3--------------------//
    //calculates total view time for Obj3
    for(var i = 0; i < window.obj3.data.length; i++) {
    obj3Data.calcTime(window.obj3.data[i].time);

    //calculates min distance
    obj3Data.calcMin(window.obj3.data[i].minDist);

    //calculates max distance
    obj3Data.calcMax(window.obj3.data[i].maxDist);
    }

}