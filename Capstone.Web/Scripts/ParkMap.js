
let domainAddress = 'http://localhost:55900';

let hostname = window.location.hostname;

if (hostname !== "localhost") {
    domainAddress = hostname;
}

function initMap() {
    // location list
    var administrativeOffices = { name: 'Cleveland Metroparks Administrative Offices', lat: 41.4456, lng: -81.7178, link: 'http://www.google.com' };

    // map positioning
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 10,
        center: administrativeOffices,
        streetViewControl: false,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    // markers from api
    let marker = [];
    let parkDetails = {};
    fetch(domainAddress + '/api/parks')
        .then(response => response.json())
        .then(parks => {
            let i = 0;
            parks.forEach(function (park) {
                parkDetails.name = park.Name;
                parkDetails.lat = park.Latitude;
                parkDetails.lng = park.Longitude;
                parkDetails.link = domainAddress + `/VirtualTrails/ChooseTrail/${park.Name}`;

                marker[i] = new google.maps.Marker({
                    position: parkDetails,
                    title: parkDetails.name,
                    map: map,
                    destinationLink: parkDetails.link
                });

                google.maps.event.addListener(marker[i], 'click', function () {
                    window.location.href = this.destinationLink;
                })

                i++;
            });
        });
}



function initParkMap() {
    let parkName = window.location.pathname.substring(27);


    let marker = [];
    let trailDetails = {};
    let poiDetails = {};
    let i = 0;
    fetch(domainAddress + `/api/park/${parkName}`)
        .then(response => response.json())
        .then(park => {
            // location list
            var parkCenter = { name: 'Park Center', lat: park.Latitude, lng: park.Longitude, link: 'http://www.google.com' };

            // map positioning
            var map = new google.maps.Map(document.getElementById('map'), {
                zoom: park.Zoom,
                center: parkCenter,
                streetViewControl: false,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            // markers for each Trail Head
            park.Trails.forEach(function (trail) {
                trailDetails.name = trail.Name;
                trailDetails.lat = trail.TrailHead.Latitude;
                trailDetails.lng = trail.TrailHead.Longitude;
                trailDetails.link = domainAddress + `/VirtualTrails/ViewTrail/?trailName=${trail.Name}&panoramicId=${trail.TrailHead.PanoramicId}`;

                marker[i] = new google.maps.Marker({
                    position: trailDetails,
                    title: trailDetails.name,
                    map: map,
                    destinationLink: trailDetails.link
                });

                google.maps.event.addListener(marker[i], 'click', function () {
                    window.location.href = this.destinationLink;
                })

                i++;
            });

            // markers for each Trail Head
            park.Trails.forEach(function (trail) {
                trail.PointsOfInterest.forEach(function (pointOfInterest) {
                    poiDetails.name = `Point of Interest on ${trail.Name}`;
                    poiDetails.lat = pointOfInterest.Latitude;
                    poiDetails.lng = pointOfInterest.Longitude;
                    poiDetails.link = domainAddress + `/VirtualTrails/ViewTrail/?trailName=${trail.Name}&panoramicId=${pointOfInterest.PanoramicId}`;

                    marker[i] = new google.maps.Marker({
                        position: poiDetails,
                        title: poiDetails.name,
                        map: map,
                        destinationLink: poiDetails.link
                    });

                    google.maps.event.addListener(marker[i], 'click', function () {
                        window.location.href = this.destinationLink;
                    })

                    i++;
                });
            });
        });


}

function MakeTour() {
    let trailName = getParameterByName("trailName");
    let panoramicId = getParameterByName("panoramicId");

    fetch(domainAddress + `/api/trail/${trailName}`)
        .then(response => response.json())
        .then(trail => {
            let viewerParameters = {};
            viewerParameters["default"] = {
                "firstScene": panoramicId + "",
                "sceneFadeDuration": 1000,
                "autoLoad": true,
                "compass": false
            };
            viewerParameters["scenes"] = {};

            trail.PanoramicsInTrail.forEach(panoramic => {
                panoramicHotSpots = [];
                panoramic.Connections.forEach(connection => {
                    let hotSpot = {
                        "pitch": connection.HotspotPitch,
                        "yaw": connection.HotspotYaw,
                        "type": "scene",
                        "sceneId": "" + connection.DestinationId,
                        "targetYaw": connection.DestinationStartYaw,
                        "targetPitch": "same"
                    };
                    panoramicHotSpots.push(hotSpot);
                });

                viewerParameters["scenes"]["" + panoramic.PanoramicId] = {
                    "pitch": 0,
                    "yaw": 120,
                    "type": "equirectangular",
                    "panorama": panoramic.ImageAddress,
                    "hotSpots": panoramicHotSpots
                };
            });

            pannellum.viewer('panorama', viewerParameters);
            
        });
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}