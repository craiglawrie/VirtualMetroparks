
let adminViewer;
function ShowPanoramic(panoramicId) {
    fetch(domainAddress + `/api/panoramic/${panoramicId}`)
        .then(response => response.json())
        .then(panoramic => {
            adminViewer = pannellum.viewer('addHotspotPanorama', {
                "type": "equirectangular",
                "panorama": panoramic.ImageAddress,
                "autoLoad": true,
                "compass": false,
                "hotSpots": []
            });
            adminViewer.on('mousedown', function (mouseEvent) {
                processMouseDownPannellum(mouseEvent);
            });
            adminViewer.on('mouseup', function (mouseEvent) {
                processMouseUpPannellum(mouseEvent);
            });
        });
}

let clickTime = 200; // ms
let click = false;
let hotSpotYaw, hotSpotPitch;
function processMouseDownPannellum(mouseEvent) {
    click = true;
    setTimeout(function () { click = false; }, clickTime)
}

let hotspotTitle, hotspotDescription;
function processMouseUpPannellum(mouseEvent) {
    if (click) {
        hotspotTitle = document.querySelector("#new-hotspot-title").value || "Please enter a title";
        hotspotDescription = document.querySelector("#new-hotspot-description").value || "Please enter a description";

        hotSpotYaw = adminViewer.mouseEventToCoords(mouseEvent)[1];
        hotSpotPitch = adminViewer.mouseEventToCoords(mouseEvent)[0];

        adminViewer.removeHotSpot("temp hotspot");

        let newHotspot = {
            "pitch": hotSpotPitch,
            "yaw": hotSpotYaw,
            "type": "info",
            "text": hotspotTitle + '\n' + hotspotDescription,
            "id": "temp hotspot"
        };

        adminViewer.addHotSpot(newHotspot);
    }
}