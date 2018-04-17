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
                console.log(panoramic.BackgroundSoundClips);
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

                panoramic.LastSeenImages.forEach(image => {
                    console.log(panoramic.GetLastSeenImagesByPanoramicId);
                    panoramicHotSpots = [];
                    panoramic.GetLastSeenImagesByPanoramicId.forEach(connection => {
                        let hotSpot = {
                            "pitch": connection.Pitch,
                            "yaw": connection.Yaw,
                            "type": "info",
                            "text": connection.Title connection.Description,
                            "image": connection.ImageAddress
                        };
                        panoramicHotSpots.push(hotSpot);
                    })
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

            if (trail.TrailHead.BackgroundSoundClips.length > 0) {
                let clipSelection = Math.floor(Math.random() * Math.floor(trail.TrailHead.BackgroundSoundClips.length));
                let audioFile = trail.TrailHead.BackgroundSoundClips[clipSelection].AudioAddress;
                audio = new Audio(audioFile);
                audio.play();
            }
        });
}