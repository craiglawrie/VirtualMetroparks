
let domainAddress = 'http://localhost:55900';



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
        .then(json => {
            let i = 0;
            json.forEach(function (park) {
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