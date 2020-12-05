import React, { useEffect, useState } from "react";
import { Map } from "../Map";
import { InfoBox } from "./InfoBox";
import "../../css/home.scss";
import Axios, { AxiosResponse, AxiosError } from "axios";
import RaspberryPi from "../../classes/RaspberryPi";

export const HomeMain: React.FC<{}> = () => {
  const [infoData, setInfoData] = useState<
    { Name: string; Title: string; Value: string }[]
  >([
    { Name: "testcount", Title: "Total number of tests", Value: "0" },
    { Name: "fevercount", Title: "Fever detected", Value: "0" },
    { Name: "nofevercount", Title: "Negative tests", Value: "0" },
    { Name: "testcounttoday", Title: "Tests today", Value: "0" },
    {
      Name: "fevercounttoday",
      Title: "Fever detected today",
      Value: "No data",
    },
    {
      Name: "highesttemptoday",
      Title: "Highest temperature today",
      Value: "No data",
    },
    { Name: "highesttemp", Title: "Highest temperature", Value: "No data" },
    {
      Name: "mostfeverslocation",
      Title: "Most fevers at",
      Value: "No location",
    },
  ]);

  const [markerData, setMarkerData] = useState<
    { ID: number; Text: string; Lat: number; Lon: number }[]
  >([]);

  useEffect(() => {
    const loadCardData = async () => {
      let testCount: string = "0";
      let feverCount: string = "0";
      let noFeverCount: string = "0";
      await Axios.get("https://fevr.azurewebsites.net/TestCount")
        .then((response: AxiosResponse) => {
          testCount = response.data;
        })
        .catch((error: AxiosError) => {
          console.log(error);
        });
      await Axios.get("https://fevr.azurewebsites.net/FeverCount")
        .then((response: AxiosResponse) => {
          feverCount = response.data;
        })
        .catch((error: AxiosError) => {
          console.log(error);
        });
      await Axios.get("https://fevr.azurewebsites.net/NoFeverCount")
        .then((response: AxiosResponse) => {
          noFeverCount = response.data;
        })
        .catch((error: AxiosError) => {
          console.log(error);
        });
      setInfoData(
        infoData.map((o) => {
          switch (o.Name) {
            case "testcount":
              return { ...o, Value: testCount };
            case "fevercount":
              return { ...o, Value: feverCount };
            case "nofevercount":
              return { ...o, Value: noFeverCount };
          }
          return o;
        })
      );
    };

    const loadPinData = async () => {
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
    loadCardData();
    loadPinData();
  }, []);

  return (
    <div className="main-container">
      <h1>Home</h1>
      <InfoBox InfoData={infoData} />
      <h1>Devices</h1>
      <Map MarkerData={markerData} />
    </div>
  );
};
