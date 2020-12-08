import { FeatureGroup, LatLng, LatLngBounds, marker } from "leaflet";
import React from "react";
import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";

interface Props {
  MarkerData: {
    ID: number;
    Text: string;
    Lat: number;
    Lon: number;
  }[];
}

export const Map: React.FC<Props> = ({ MarkerData }) => {
  function getMapBounds(
    data: { Text: string; Lat: number; Lon: number }[]
  ): LatLngBounds {
    let ftGrp: FeatureGroup = new FeatureGroup(
      data.map((o) => marker(new LatLng(o.Lat, o.Lon)))
    );
    return ftGrp.getBounds();
  }

  return (
    <div>
      {MarkerData.length > 0 ? (
        <MapContainer bounds={getMapBounds(MarkerData)} scrollWheelZoom={true}>
          <TileLayer
            attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          />
          {MarkerData.map((o) => {
            return (
              <Marker key={o.ID} position={[o.Lat, o.Lon]}>
                <Popup>{o.Text}</Popup>
              </Marker>
            );
          })}
        </MapContainer>
      ) : (
        <MapContainer center={[52, 12]} zoom={5} scrollWheelZoom={true}>
          <TileLayer
            attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          />
        </MapContainer>
      )}
    </div>
  );
};
