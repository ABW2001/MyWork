let map;

function initMap() {
    const map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: -33.983061147979726, lng: 18.919417213577617  },
        zoom: 8,
    });

    map.addListener("click", (mapsMouseEvent) => {
        position = mapsMouseEvent.latLng
        position = JSON.stringify(position.toJSON(), null, 2)
        console.log(position);
        $.ajax('http://127.0.0.1:5000/', {
            type: 'POST',
            data: { 'mapData': position },
            dataType: 'json',
            timeout: 5000,
            success: function (data, status, xhr) {
                displayWeather(data);
            },
            error: function (jqXhr, textStatus, errorMessage) {
                console.log("error");
            }
        });
    });
}

function displayWeather(data) {
    for (i = 0; i <= 11; i++) {
        date = (data[i].startTime).substring(0, 10);
        time = (data[i].startTime).substring(11, 19);
        temp = data[i].values.temperature;
        cloud = data[i].values.cloudCover;
        wind = data[i].values.windSpeed;
        windDir = data[i].values.windDirection;
        document.getElementById(i + 1).innerHTML = date + " " + time + "<br>" + "Temperature: " + temp + "<br>" + "Cloud Cover: " +
            cloud + "<br>" + "Wind Speed: " + wind + "<br>" + "Wind Direction: ";
        var tbl = document.getElementById(i + 1);
        var canvas = document.createElement('canvas');
        canvas.id = "C" + (i + 1);
        canvas.width = 500;
        canvas.height = 500;
        canvas.style.zIndex = 8;
        canvas.style.position = "absolute";
        var ctx = canvas.getContext("2d");
        ctx.translate(20,50);
        ctx.rotate(windDir * Math.PI / 180);
        ctx.translate(0, 20);
        ctx.font = "20px Arial";
        ctx.fillText("<", 5, 0);
        tbl.appendChild(canvas);
    }
}