import { FeatureGroup, LatLng, LatLngBounds, marker } from "leaflet";
import React from "react";
import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";

interface Props {
  MarkerData: {
    Text: string;
    Lat: number;
    Lon: number;
  }[];
}

export const Map: React.FC<Props> = ({ MarkerData }) => {
  function getMapBounds(): LatLngBounds {
    let ftGrp: FeatureGroup = new FeatureGroup(
      MarkerData.map((o) => marker(new LatLng(o.Lat, o.Lon)))
    );
    return ftGrp.getBounds();
  }
  return (
    <MapContainer bounds={getMapBounds()} scrollWheelZoom={true}>
      <TileLayer
        attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />
      {MarkerData.map((o) => {
        return (
          <Marker key={o.Text} position={[o.Lat, o.Lon]}>
            <Popup>{o.Text}</Popup>
          </Marker>
        );
      })}
    </MapContainer>
  );
};
