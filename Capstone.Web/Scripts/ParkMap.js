

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
    let leftOffDetails = {};
    let poiDetails = {};
    let i = 0;
    fetch(domainAddress + `/api/park/${parkName}`, {
        method: 'GET',
        credentials: 'include'
    })
        .then(response => response.json())
        .then(park => {
            // location list
            var parkCenter = { name: 'Park Center', lat: park.Latitude, lng: park.Longitude, link: 'http://www.google.com' };

            // map positioning
            var map = new google.maps.Map(document.getElementById('map'), {
                zoom: park.Zoom,
                center: parkCenter,
                streetViewControl: false,
                mapTypeId: google.maps.MapTypeId.HYBRID
            });

            // draw trails, loops, paths, etc
            let userVisitedPanoramicIds = park.UserVisitedPanoramics.map(function (pan) { return pan.PanoramicId; });
            park.Trails.forEach(trail => {

                // Draw trail, regardless of visited status
                trail.PanoramicsInTrail.forEach(panoramic => {
                    panoramic.Connections.forEach(connection => {

                        let destination = trail.PanoramicsInTrail.filter(function (pan) { return pan.PanoramicId === connection.DestinationId; })[0];

                        let connectionSource = new google.maps.LatLng(panoramic.Latitude, panoramic.Longitude);
                        let connectionDest = new google.maps.LatLng(destination.Latitude, destination.Longitude);

                        let trailSection = new google.maps.Polyline({
                            path: [connectionSource, connectionDest],
                            strokeColor: "#FFFFFF",
                            strokeOpacity: 1,
                            strokeWeight: 8,
                            map: map
                        });
                    })
                });

                // Draw red progress for visited locations
                trail.PanoramicsInTrail.forEach(panoramic => {
                    panoramic.Connections.forEach(connection => {

                        let destination = trail.PanoramicsInTrail.filter(function (pan) { return pan.PanoramicId === connection.DestinationId; })[0];

                        let connectionSource = new google.maps.LatLng(panoramic.Latitude, panoramic.Longitude);
                        let connectionDest = new google.maps.LatLng(destination.Latitude, destination.Longitude);

                        if (userVisitedPanoramicIds.includes(panoramic.PanoramicId) && userVisitedPanoramicIds.includes(destination.PanoramicId)) {
                            let visitedSection = new google.maps.Polyline({
                                path: [connectionSource, connectionDest],
                                strokeColor: "#FF0000",
                                strokeOpacity: 1,
                                strokeWeight: 4,
                                map: map
                            });
                        }
                    })
                });
            });

            // markers for each Trail Head
            park.Trails.forEach(function (trail) {
                trailDetails.name = trail.Name + ` - Trail Head`;
                trailDetails.lat = trail.TrailHead.Latitude;
                trailDetails.lng = trail.TrailHead.Longitude;
                trailDetails.link = domainAddress + `/VirtualTrails/ViewTrail/?trailName=${trail.Name}&panoramicId=${trail.TrailHead.PanoramicId}`;

                marker[i] = new google.maps.Marker({
                    position: trailDetails,
                    title: trailDetails.name,
                    map: map,
                    destinationLink: trailDetails.link,
                    icon: `https://contattafiles.s3.us-west-1.amazonaws.com/tecommunity/oKyGFk97qM30TAV/hiking_tourism.png`
                });

                google.maps.event.addListener(marker[i], 'click', function () {
                    window.location.href = this.destinationLink;
                })

                i++;
            });

            // markers for each Point of Interest
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
                        destinationLink: poiDetails.link,
                        icon: `https://contattafiles.s3.us-west-1.amazonaws.com/tecommunity/rKRs-rJdGQmJ8DU/cluster.png`
                    });

                    google.maps.event.addListener(marker[i], 'click', function () {
                        window.location.href = this.destinationLink;
                    })

                    i++;
                });
            });

            // markers for Pick Up Where You Left Off
            park.Trails.forEach(trail => {
                trail.PanoramicsInTrail.forEach(panoramic => {
                    if (userVisitedPanoramicIds.includes(panoramic.PanoramicId)) {
                        let visitedConnections = panoramic.Connections.filter(function (connection) { return userVisitedPanoramicIds.includes(connection.DestinationId) });

                        if (visitedConnections.length > 0 && visitedConnections.length < panoramic.Connections.length) {

                            leftOffDetails.name = trail.Name + ` - Pick Up Where You Left Off`;
                            leftOffDetails.lat = panoramic.Latitude;
                            leftOffDetails.lng = panoramic.Longitude;
                            leftOffDetails.link = domainAddress + `/VirtualTrails/ViewTrail/?trailName=${trail.Name}&panoramicId=${panoramic.PanoramicId}`;

                            marker[i] = new google.maps.Marker({
                                position: leftOffDetails,
                                title: leftOffDetails.name,
                                map: map,
                                destinationLink: leftOffDetails.link,
                                icon: `https://contattafiles.s3.us-west-1.amazonaws.com/tecommunity/yoVZuT5sPPOVZNa/hiking.png`
                            });

                            google.maps.event.addListener(marker[i], 'click', function () {
                                window.location.href = this.destinationLink;
                            })

                            i++;
                        }
                    }

                });
            });
        });


}

var audio = new Audio();
audio.addEventListener("ended", function () {
    playAudio();
});
var audioFile;
var soundClips = [];
let viewer;
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
                        "targetPitch": "same",
                        "clickHandlerFunc": function () {
                            processNewPanoramicVisit(connection.DestinationId);
                        }
                    };
                    panoramicHotSpots.push(hotSpot);
                });

                panoramic.LastSeenImages.forEach(image => {
                    let hotSpot = {
                        "pitch": image.Pitch,
                        "yaw": image.Yaw,
                        "type": "info",
                        "text": image.Title + "\n" + image.Description,
                        "clickHandlerFunc": function () {
                            $.fancybox.open(`
                                    <div>
                                        <h2>`+ image.Title + `</h2>
                                        <img src="`+ image.ImageAddress + `" />
                                    </div> `);
                        }
                    };
                    panoramicHotSpots.push(hotSpot);
                });

                panoramic.LastSeenVideos.forEach(video => {
                    let hotSpot = {
                        "pitch": video.Pitch,
                        "yaw": video.Yaw,
                        "type": "info",
                        "text": video.Title + "\n" + video.Description,
                        "clickHandlerFunc": function () {
                            $.fancybox.open(`
                                    <div>
                                        <h2>`+ video.Title + `</h2>
                                        <iframe width="1000px" height="600px" src="`+ video.VideoAddress + `" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
                                    </div> `);
                        }
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

            viewer = pannellum.viewer('panorama', viewerParameters);
            viewer.on('mouseup', function (mouseEvent) {
                processClickOnPannellum(mouseEvent);
            });

            processNewPanoramicVisit(panoramicId);
        });

}

function processClickOnPannellum(mouseEvent) {
    let mouseYaw = viewer.mouseEventToCoords(mouseEvent)[1];
    let mousePitch = viewer.mouseEventToCoords(mouseEvent)[0];
}

function processNewPanoramicVisit(panoramicId) {
    setBackgroundAudioForNewPanoramic(panoramicId);
    fetch(domainAddress + `/api/visited/${panoramicId}`, {
        method: 'POST',
        credentials: 'include'
    });
}

function setBackgroundAudioForNewPanoramic(destinationId) {
    fetch(domainAddress + `/api/panoramic/${destinationId}`)
        .then(response => response.json())
        .then(panoramic => {
            soundClips = panoramic.BackgroundSoundClips.map(function (soundClip) { return soundClip.AudioAddress });
            if (!soundClips.includes(audioFile) || successfulBackgroundStart !== undefined) {
                playAudio();
            }
        });
}

var successfulBackgroundStart;
function playAudio() {
    audio.pause();
    audio.currentTime = 0;
    audio.src = getNewAudioFileFromArray(soundClips);
    audio.play()
        .then(success => { successfulBackgroundStart = success; })
        .catch(error => { successfulBackgroundStart = error; });
}

function getNewAudioFileFromArray(soundClips) {
    if (soundClips.length > 0) {
        let clipSelection = Math.floor(Math.random() * Math.floor(soundClips.length));
        audioFile = soundClips[clipSelection];
    }
    return audioFile;
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

let mutedIcons = ["/Content/images/icons/sound stop.png", "/Content/images/icons/sound play.png"]
function toggleAudioMute() {
    audio.muted = !audio.muted;
    let muteButton = document.querySelector('img[onclick="toggleAudioMute()"]')
    muteButton.setAttribute("src", mutedIcons.filter(function (imageSrc) { return imageSrc !== muteButton.getAttribute("src") })[0]);
}
