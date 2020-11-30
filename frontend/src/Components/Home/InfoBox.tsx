import React from "react";
import { InfoCard } from "./InfoCard";

interface Props {
  InfoData: {
    Name: string;
    Title: string;
    Value: string;
  }[];
}

export const InfoBox: React.FC<Props> = ({ InfoData }) => {
  return (
    <div className="grid-container">
      {InfoData.map((o) => {
        return <InfoCard key={o.Name} Title={o.Title} Value={o.Value} />;
      })}
    </div>
  );
};
