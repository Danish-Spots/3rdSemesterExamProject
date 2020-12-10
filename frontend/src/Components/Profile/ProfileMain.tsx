import Axios, { AxiosError, AxiosResponse } from "axios";
import React, { useContext } from "react";
import Profile from "../../classes/Profile";
import ProfileCard from "./ProfileCard";
import "../../css/profile.scss";
import { observer } from "mobx-react-lite";
import { UserStoreContext } from '../../stores/UserStore';


interface ProfileMainProps {}

export const ProfileMain: React.FC<ProfileMainProps> = observer(() => {
  const userStore = useContext(UserStoreContext);
  let profileId = userStore.profileID

  return (
    <div className="main-container">
      <h1> Profiles </h1>
      <ProfileCard  userID={profileId} />
    </div>
  );
});
