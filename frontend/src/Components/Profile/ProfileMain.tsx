import Axios, { AxiosError, AxiosResponse } from "axios";
import React, { useContext } from "react";
import Profile from "../../classes/Profile";
import ProfileCard from "./ProfileCard";
import "../../css/profile.scss";
import { ProfileStoreContext } from "../../stores/ProfileStore";
import { observer } from "mobx-react-lite";

interface ProfileMainProps {}

export const ProfileMain: React.FC<ProfileMainProps> = observer(() => {
  const profileStore = useContext(ProfileStoreContext);

  return (
    <div className="main-container">
      <h1> Profiles </h1>
      <ProfileCard profile={profileStore.profile} />
    </div>
  );
});
