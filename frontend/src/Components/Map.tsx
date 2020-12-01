import { LatLngTuple } from "leaflet";
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
  function getMapCenter(): LatLngTuple {
    let sumLat: number = 0;
    let sumLon: number = 0;

    MarkerData.forEach((o) => {
      sumLat += o.Lat;
      sumLon += o.Lon;
    });

    sumLat /= MarkerData.length;
    sumLon /= MarkerData.length;

    return [sumLat, sumLon];
  }

  function getMapZoom(): number {
    let center: LatLngTuple = getMapCenter();
    let longestDistance: number = 0;

    MarkerData.forEach((o) => {
      let latDistance: number = Math.abs(o.Lat - center[0]);
      let lonDistance: number = Math.abs(o.Lon - center[1]);
      if (latDistance > longestDistance) {
        longestDistance = latDistance;
      }
      if (lonDistance > longestDistance) {
        longestDistance = latDistance;
      }
    });
    return longestDistance;
  }

  return (
    <MapContainer
      center={getMapCenter()}
      zoom={getMapZoom()}
      scrollWheelZoom={true}
    >
      <TileLayer
        attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />
      {MarkerData.map((o) => {
        return (
          <Marker position={[o.Lat, o.Lon]}>
            <Popup>{o.Text}</Popup>
          </Marker>
        );
      })}
    </MapContainer>
  );
};
