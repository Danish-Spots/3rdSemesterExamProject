import { FeatureGroup, LatLng, LatLngBounds, marker } from 'leaflet';
import { action, makeObservable, observable } from 'mobx'
import { createContext } from 'react';
import MapMarker from "../classes/MapMarker"

class MapMarkerStore {
    @observable markers : MapMarker[] = [new MapMarker(0, "Loading...", 52, 12)];
    @observable bounds : LatLngBounds = new LatLngBounds([52, 10], [54, 12]);

    @action
    change_UpdateBounds() {
        let bounds = new LatLngBounds([52, 10], [54, 12]);
        if (this.markers.length > 0) {
            let ftGrp: FeatureGroup = new FeatureGroup(
            this.markers.map((o) => marker(new LatLng(o.Lat, o.Lon))));
            bounds = ftGrp.getBounds(); 
        }
        this.bounds = bounds
        }

    @action
    change_setDefault() {
        this.markers = [new MapMarker(0, "Loading...", 52, 12)];
        this.change_UpdateBounds();
    }

    @action
    change_Markers(markers : MapMarker[]) {
        this.markers = markers;
        this.change_UpdateBounds();
    }

    @action
    change_AddMarker(marker : MapMarker) {
        this.markers.push(marker);
        this.change_UpdateBounds();
    }

}

export const MapMarkerStoreContext = createContext(makeObservable(new MapMarkerStore()));