        //var myData = JSON.parse(data)
        //console.log(data.data[0].objName);

        // Load google charts
        google.charts.load('current', {'packages':['corechart', 'bar']});
        google.charts.setOnLoadCallback(drawChart);
        google.charts.setOnLoadCallback(drawBar);

        // Draw the chart and set the chart values
        function drawChart() {
        var data = google.visualization.arrayToDataTable([
        ['Objects', 'per Time Viewed'],
        ['Object #1', 8],
        ['Object #2', 3],
        ['Object #3', 2]
        ]);

        // adds a title, and sets width and height
        var options = {'title':'View Time per Object', 'width':550, 'height':400};

        // Display the chart inside the <div> element with id="viewchart"
        var chart = new google.visualization.PieChart(document.getElementById('pie_chart_div'));
        chart.draw(data, options);
        }

        function drawBar() {
        var data = google.visualization.arrayToDataTable([
        ['Plugin Objects', 'Min', 'Max'],
        ['#1', 4.469, 5.428],
        ['#2', 0.604, 3.416],
        ['#3', 4.031, 5.852]
        ]);

        var options = {

        title: 'Minimum and Maximum Distance From Player',

        width: 550,
        height: 400
        };

        var chart = new google.charts.Bar(document.getElementById('bar_graph_div'));
        chart.draw(data, google.charts.Bar.convertOptions(options));
        }

        chart[0].style.position = "abosolute";+
        //chart[0].style.left = "100";
        //chart[0].style.top = "150";
