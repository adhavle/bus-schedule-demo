const baseApiPath = "http://localhost:5103/transitInfo/";

$(document).ready(function() {
    console.log("client side scripts loaded");

    // disappear the stops and schedule columns to start
    $('#stops').addClass('transitInfo-display-none');
    $('#schedule').addClass('transitInfo-display-none');

    showRoutes();
});

async function showRoutes() {
    const url = `${baseApiPath}routes`;
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }

        const result = await response.json();
        console.log(result);

        var routesHtml = "<p>Routes</p>";
        result.forEach(r => {
            routesHtml += `<button type="button" class="btn btn-primary" `;
            routesHtml += `onclick="showStops(${r.topLevelRouteId})"`
            routesHtml += `>${r.routeName}</button><br><br>`;
        });

        $("#routes").html(routesHtml);
    } catch (error) {
        console.error(error.message);
    }
}

async function showStops(tlrId) {
    const url = `${baseApiPath}stops/${tlrId}`;

    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }

        const result = await response.json();
        console.log(result);

        /// TBD: add spacing, fix button width
        var stopsHtml = "<p>Select a stop</p>";
        result.stops.forEach(s => {
            stopsHtml += `<button type="button" class="btn btn-secondary" `
            stopsHtml += `onclick="showNextScheduledTime(${result.routeId}, ${s.stopId})">`
            stopsHtml +=`${s.address}</button><br>`;
        });

        // update list of stops
        $("#stops").html(stopsHtml);
        // disappear the routes column
        $('#routes').addClass('transitInfo-display-none');
        // make the stops column visible
        $('#stops').removeClass('transitInfo-display-none');
    } catch (error) {
        console.error(error.message);
    }
}

async function showNextScheduledTime(routeId, stopId) {
    const url = `${baseApiPath}nextScheduledTime/${routeId}/${stopId}`;

    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }

        const result = await response.json();
        console.log(result);

        scheduleHtml = "";
        if (result.isServiceEndedForDay) {
            scheduleHtml += `<p>Sorry, no further service at ${result.address} for today.</p>`;
        }
        else {
            scheduleHtml += `<p>Your next bus at ${result.address} will be at ${result.nextScheduledTime}`;
        }

        // update list of stops
        $("#schedule").html(scheduleHtml);
        // disappear the routes column
        $('#stops').addClass('transitInfo-display-none');
        // make the stops column visible
        $('#schedule').removeClass('transitInfo-display-none');

    } catch(error) {
        console.log(error.message);
    }
}