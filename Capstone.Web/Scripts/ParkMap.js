
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

            ///* Hard-coded tour data below this line.*/
            //pannellum.viewer('panorama', {
            //    "default": {
            //        "firstScene": "0",
            //        "sceneFadeDuration": 1000,
            //        "autoLoad": true,
            //        "compass": false
            //    },

            //    "scenes": {
            //        "0": {
            //            "pitch": 0,
            //            "yaw": 120,
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/m44owqT.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -3,
            //                    "yaw": 129,
            //                    "type": "scene",
            //                    "sceneId": "1",
            //                    "targetYaw": 55,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "1": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/6DGsx7r.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -2,
            //                    "yaw": -128,
            //                    "type": "scene",
            //                    "sceneId": "0",
            //                    "targetYaw": -10,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -3,
            //                    "yaw": 72,
            //                    "type": "scene",
            //                    "sceneId": "2",
            //                    "targetYaw": 40,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "2": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/7g1ObAR.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -4,
            //                    "yaw": -135,
            //                    "type": "scene",
            //                    "sceneId": "1",
            //                    "targetYaw": -120,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -3,
            //                    "yaw": 55,
            //                    "type": "scene",
            //                    "sceneId": "3",
            //                    "targetYaw": 80,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "3": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/qFMVIyd.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -4,
            //                    "yaw": -101,
            //                    "type": "scene",
            //                    "sceneId": "2",
            //                    "targetYaw": -120,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -2,
            //                    "yaw": 93,
            //                    "type": "scene",
            //                    "sceneId": "4",
            //                    "targetYaw": -150,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "4": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/P6cwRuQ.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -3,
            //                    "yaw": 33,
            //                    "type": "scene",
            //                    "sceneId": "3",
            //                    "targetYaw": -105,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -2,
            //                    "yaw": -140,
            //                    "type": "scene",
            //                    "sceneId": "5",
            //                    "targetYaw": -250,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "5": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/3i2FW1A.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -3,
            //                    "yaw": -65,
            //                    "type": "scene",
            //                    "sceneId": "4",
            //                    "targetYaw": 45,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -2,
            //                    "yaw": -250,
            //                    "type": "scene",
            //                    "sceneId": "6",
            //                    "targetYaw": -150,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "6": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/Kd4ZSxD.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -3,
            //                    "yaw": 32,
            //                    "type": "scene",
            //                    "sceneId": "5",
            //                    "targetYaw": -65,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -4,
            //                    "yaw": -145,
            //                    "type": "scene",
            //                    "sceneId": "7",
            //                    "targetYaw": -220,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "7": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/jd5MI0x.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": 0,
            //                    "yaw": -29,
            //                    "type": "scene",
            //                    "sceneId": "6",
            //                    "targetYaw": 35,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -2,
            //                    "yaw": -221,
            //                    "type": "scene",
            //                    "sceneId": "8",
            //                    "targetYaw": -250,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "8": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/iSZjIpe.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -3,
            //                    "yaw": -60,
            //                    "type": "scene",
            //                    "sceneId": "7",
            //                    "targetYaw": -20,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -1,
            //                    "yaw": -252,
            //                    "type": "scene",
            //                    "sceneId": "9",
            //                    "targetYaw": -250,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "9": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/FtlSfOp.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -3,
            //                    "yaw": -75,
            //                    "type": "scene",
            //                    "sceneId": "8",
            //                    "targetYaw": -50,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -1,
            //                    "yaw": 109,
            //                    "type": "scene",
            //                    "sceneId": "10",
            //                    "targetYaw": 30,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "10": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/wed9OKv.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -3,
            //                    "yaw": -154,
            //                    "type": "scene",
            //                    "sceneId": "9",
            //                    "targetYaw": -75,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -1,
            //                    "yaw": 21,
            //                    "type": "scene",
            //                    "sceneId": "11",
            //                    "targetYaw": -150,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "11": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/9m6O4bm.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -3,
            //                    "yaw": 38,
            //                    "type": "scene",
            //                    "sceneId": "10",
            //                    "targetYaw": -155,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -1,
            //                    "yaw": -145,
            //                    "type": "scene",
            //                    "sceneId": "12",
            //                    "targetYaw": -100,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "12": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/awkNJsM.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -2,
            //                    "yaw": 68,
            //                    "type": "scene",
            //                    "sceneId": "11",
            //                    "targetYaw": 25,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -2,
            //                    "yaw": -100,
            //                    "type": "scene",
            //                    "sceneId": "13",
            //                    "targetYaw": -185,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "13": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/f0zUlBR.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -2,
            //                    "yaw": -12,
            //                    "type": "scene",
            //                    "sceneId": "12",
            //                    "targetYaw": 60,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -4,
            //                    "yaw": -196,
            //                    "type": "scene",
            //                    "sceneId": "14",
            //                    "targetYaw": -245,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "14": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/t0i5UxZ.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": 1,
            //                    "yaw": -71,
            //                    "type": "scene",
            //                    "sceneId": "13",
            //                    "targetYaw": 0,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -2,
            //                    "yaw": -258,
            //                    "type": "scene",
            //                    "sceneId": "15",
            //                    "targetYaw": -310,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "15": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/eJlo3rW.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -4,
            //                    "yaw": -138,
            //                    "type": "scene",
            //                    "sceneId": "14",
            //                    "targetYaw": -70,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": 4,
            //                    "yaw": -320,
            //                    "type": "scene",
            //                    "sceneId": "16",
            //                    "targetYaw": -320,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "16": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/D0NRnVU.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -7,
            //                    "yaw": -127,
            //                    "type": "scene",
            //                    "sceneId": "15",
            //                    "targetYaw": -150,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -5,
            //                    "yaw": -333,
            //                    "type": "scene",
            //                    "sceneId": "17",
            //                    "targetYaw": -160,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "17": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/eZjYSql.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -2,
            //                    "yaw": 5,
            //                    "type": "scene",
            //                    "sceneId": "16",
            //                    "targetYaw": -130,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -4,
            //                    "yaw": -158,
            //                    "type": "scene",
            //                    "sceneId": "18",
            //                    "targetYaw": -250,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "18": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/Ii3UKpS.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": 0,
            //                    "yaw": -34,
            //                    "type": "scene",
            //                    "sceneId": "17",
            //                    "targetYaw": 20,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -29,
            //                    "yaw": 81,
            //                    "type": "scene",
            //                    "sceneId": "19",
            //                    "targetYaw": 90,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "19": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/cMGjZ7H.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": 20,
            //                    "yaw": -145,
            //                    "type": "scene",
            //                    "sceneId": "18",
            //                    "targetYaw": 0,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -25,
            //                    "yaw": 83,
            //                    "type": "scene",
            //                    "sceneId": "20",
            //                    "targetYaw": 0,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "20": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/xqlQsZS.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": 15,
            //                    "yaw": -20,
            //                    "type": "scene",
            //                    "sceneId": "19",
            //                    "targetYaw": -140,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -24,
            //                    "yaw": 5,
            //                    "type": "scene",
            //                    "sceneId": "21",
            //                    "targetYaw": 70,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "21": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/M1usx7u.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": 18,
            //                    "yaw": -112,
            //                    "type": "scene",
            //                    "sceneId": "20",
            //                    "targetYaw": -50,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -6,
            //                    "yaw": 63,
            //                    "type": "scene",
            //                    "sceneId": "22",
            //                    "targetYaw": 70,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "22": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/nQRXR6W.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": 1,
            //                    "yaw": -110,
            //                    "type": "scene",
            //                    "sceneId": "21",
            //                    "targetYaw": -90,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -3,
            //                    "yaw": 57,
            //                    "type": "scene",
            //                    "sceneId": "23",
            //                    "targetYaw": 20,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "23": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/QY9uBiJ.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -2,
            //                    "yaw": -165,
            //                    "type": "scene",
            //                    "sceneId": "22",
            //                    "targetYaw": -90,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -3,
            //                    "yaw": 10,
            //                    "type": "scene",
            //                    "sceneId": "24",
            //                    "targetYaw": 0,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "24": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/JB6ouLO.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -12,
            //                    "yaw": -195,
            //                    "type": "scene",
            //                    "sceneId": "23",
            //                    "targetYaw": -170,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -12,
            //                    "yaw": -23,
            //                    "type": "scene",
            //                    "sceneId": "25",
            //                    "targetYaw": 0,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "25": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/FLthASw.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": -6,
            //                    "yaw": -150,
            //                    "type": "scene",
            //                    "sceneId": "24",
            //                    "targetYaw": -180,
            //                    "targetPitch": "same"
            //                },
            //                {
            //                    "pitch": -27,
            //                    "yaw": 108,
            //                    "type": "scene",
            //                    "sceneId": "26",
            //                    "targetYaw": -120,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        },

            //        "26": {
            //            "type": "equirectangular",
            //            "panorama": "https://i.imgur.com/NU2xdeS.jpg",
            //            "hotSpots": [
            //                {
            //                    "pitch": 24,
            //                    "yaw": -45,
            //                    "type": "scene",
            //                    "sceneId": "25",
            //                    "targetYaw": -180,
            //                    "targetPitch": "same"
            //                }
            //            ]
            //        }
            //    }
            //});
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