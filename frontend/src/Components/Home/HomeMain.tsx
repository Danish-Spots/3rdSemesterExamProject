import React from "react";
import { Map } from "../Map";
import { InfoBox } from "./InfoBox";

interface Props {
  InfoData: {
    Name: string;
    Title: string;
    Value: string;
  }[];
  MarkerData: {
    Text: string;
    Lat: number;
    Lon: number;
  }[];
}

export const HomeMain: React.FC<Props> = ({ InfoData, MarkerData }) => {
  return (
    <div className="main-container">
      <h1>Home</h1>
      <InfoBox InfoData={InfoData} />
      <Map MarkerData={MarkerData} />
    </div>
  );
};
