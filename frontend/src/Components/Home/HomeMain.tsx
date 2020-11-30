import React from "react";
import { InfoBox } from "./InfoBox";

interface Props {
  InfoData: {
    Name: string;
    Title: string;
    Value: string;
  }[];
}

export const HomeMain: React.FC<Props> = ({ InfoData }) => {
  return (
    <div className="main-container">
      <h1>Home</h1>
      <InfoBox InfoData={InfoData} />
      <div className="map-container">Map</div>
    </div>
  );
};
