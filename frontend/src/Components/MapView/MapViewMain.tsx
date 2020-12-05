import Axios, { AxiosResponse, AxiosError } from "axios";
import { marker } from "leaflet";
import React, { useEffect, useState } from "react";
import RaspberryPi from "../../classes/RaspberryPi";
import { Map } from "../Map";
import "../../css/map.scss";


// interface Props {
//     MarkerData: {
//       Text: string;
//       Lat: number;
//       Lon: number;
//     }[];
//   }

export const MapViewMain: React.FC<{}> = ({}) => {
  const [markerData, setMarkerData] = useState<
    { ID: number; Text: string; Lat: number; Lon: number }[]
  >([]);

  useEffect(() => {
    const loadPinData = async (sessionKey: string) => {
      let userID: number = 0;
      let profileID: number = 0;
      let pis: RaspberryPi[] = [];
      console.log(sessionKey);
      await Axios.get(
        "https://fevr.azurewebsites.net/api/Sessions/getSessionKey=" +
          sessionKey
      ).then((response: AxiosResponse) => {
        const data: { id: number; key: string; userID: number } = response.data;
        userID = data.userID;
      });
      await Axios.get(
        "https://fevr.azurewebsites.net/api/Users/" + userID
      ).then((response: AxiosResponse) => {
        const data: {
          id: number;
          userName: string;
          password: string;
          email: string;
          profileID: number;
        } = response.data;
        profileID = data.profileID;
      });
      console.log(userID);
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
              if (+o.profileID === profileID) {
                let newPi: RaspberryPi = new RaspberryPi(
                  o.id,
                  o.location,
                  o.isActive,
                  o.profileID,
                  +o.longitude,
                  +o.latitude
                );
                pis.push(newPi);
              }
            }
          );
        })
        .catch((error: AxiosError) => {
          console.log(error);
        });
      let newMarkerData = pis.map((o) => {
        console.log(o);
        return {
          ID: o.Id,
          Text: o.Location,
          Lat: +o.Latitude,
          Lon: +o.Longitude,
        };
      });
      setMarkerData([...newMarkerData]);
    };

    loadPinData(sessionStorage.getItem("SessionKey") as string);
  }, []);

  return( 
  
        <div className="main-container">
          <h1>Map view</h1>
          <div className="map-container">
          <Map MarkerData={markerData} />
          </div>
        </div>
      )
};
