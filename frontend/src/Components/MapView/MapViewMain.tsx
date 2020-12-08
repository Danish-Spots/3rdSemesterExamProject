import Axios, { AxiosResponse, AxiosError } from "axios";
import React, { useContext, useEffect } from "react";
import RaspberryPi from "../../classes/RaspberryPi";
import { Map } from "../Map";
import "../../css/map.scss";
import { MapMarkerStoreContext } from "../../stores/MapMarkerStore";
import { UserStoreContext } from "../../stores/UserStore";

// interface Props {
//     MarkerData: {
//       Text: string;
//       Lat: number;
//       Lon: number;
//     }[];
//   }

export const MapViewMain: React.FC<{}> = () => {
  const mapMarkerStore = useContext(MapMarkerStoreContext);
  const userStore = useContext(UserStoreContext);

  useEffect(() => {
    const loadPinData = async (sessionKey: string) => {
      let pis: RaspberryPi[] = [];
      await Axios.get("https://fevr.azurewebsites.net/api/RaspberryPis")
        .then((response: AxiosResponse) => {
          response.data.forEach(
            (o: {
              id: number;
              location: string;
              isActive: boolean;
              profileID: number;
              longitude: number;
              latitude: number;
            }) => {
              console.log(o.profileID, userStore.profileID as number);
              if (+o.profileID === (userStore.profileID as number)) {
                let newPi: RaspberryPi = new RaspberryPi(
                  o.id,
                  o.location,
                  o.isActive,
                  o.profileID,
                  +o.longitude,
                  +o.latitude
                );
                console.log("Correct ProfileID");
                pis.push(newPi);
              }
            }
          );
        })
        .catch((error: AxiosError) => {
          console.log(error);
        });
      let newMarkerData = pis.map((o) => {
        return {
          ID: o.Id,
          Text: o.Location,
          Lat: +o.Latitude,
          Lon: +o.Longitude,
        };
      });
      mapMarkerStore.change_Markers(newMarkerData);
    };

    loadPinData(sessionStorage.getItem("SessionKey") as string);
  }, []);

  return (
    <div className="main-container">
      <h1>Map view</h1>
      <div className="map-container">
        <Map />
      </div>
    </div>
  );
};
