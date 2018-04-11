﻿
let domainAddress = 'http://localhost:55900';



function initMap() {
    // location list
    var southChagrin = { name: 'South Chagrin', lat: 41.4185, lng: -81.4252, link: 'http://www.google.com' };
    var clevelandMetroparksZoo = { name: 'Cleveland Metroparks Zoo', lat: 41.4459, lng: -81.7126, link: 'http://www.google.com' };
    var bradleyWoods = { name: 'Bradley Woods Reservation', lat: 41.412204, lng: -81.9562316, link: 'http://www.google.com' };
    var administrativeOffices = { name: 'Cleveland Metroparks Administrative Offices', lat: 41.4456, lng: -81.7178, link: 'http://www.google.com' };

    // map positioning
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 10,
        center: administrativeOffices,
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
                console.log(parkDetails.link);

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

    /*
     
    // manual markers
    // South Chagrin
    var southChagrinMarker = new google.maps.Marker({
        position: southChagrin,
        title: southChagrin.name,
        map: map
    });
    google.maps.event.addListener(southChagrinMarker, 'click', function () {
        window.location.href = southChagrin.link;
    });

    // Bradley Woods
    var bradleyWoodsMarker = new google.maps.Marker({
        position: bradleyWoods,
        title: bradleyWoods.name,
        map: map
    });
    google.maps.event.addListener(bradleyWoodsMarker, 'click', function () {
        window.location.href = bradleyWoods.link;
    });

    // Zoo
    var clevelandMetroparksZooMarker = new google.maps.Marker({
        position: clevelandMetroparksZoo,
        title: clevelandMetroparksZoo.name,
        map: map
    });
    google.maps.event.addListener(clevelandMetroparksZooMarker, 'click', function () {
        window.location.href = clevelandMetroparksZoo.link;
    });

    // Administrative Offices
    var administrativeOfficesMarker = new google.maps.Marker({
        position: administrativeOffices,
        title: administrativeOffices.name,
        map: map
    });
    google.maps.event.addListener(administrativeOfficesMarker, 'click', function () {
        window.location.href = administrativeOffices.link;
    });

    */
}



