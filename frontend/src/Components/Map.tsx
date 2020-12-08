import { LatLngBounds } from "leaflet";
import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import { MapMarkerStoreContext } from "../stores/MapMarkerStore";

export const Map: React.FC<{}> = observer(() => {
  const mapMarkerStore = useContext(MapMarkerStoreContext);

  return (
    <div>
      <MapContainer
        bounds={new LatLngBounds([54, 11], [58, 12])}
        scrollWheelZoom={true}
      >
        <TileLayer
          attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        {mapMarkerStore.markers.map((o) => {
          return (
            <Marker key={o.ID} position={[o.Lat, o.Lon]}>
              <Popup>{o.Text}</Popup>
            </Marker>
          );
        })}
      </MapContainer>
    </div>
  );
});
