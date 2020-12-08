import React from "react";

interface Props {
  Title: string;
  Value: string;
}

export const InfoCard: React.FC<Props> = ({ Title, Value }) => {
  return (
    <div className="grid">
      <label className="info-title">{Title}</label>
      <label className="info-value">{Value}</label>
    </div>
  );
};
