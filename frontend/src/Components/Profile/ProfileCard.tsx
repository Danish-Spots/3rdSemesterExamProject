import React, { ChangeEvent, useContext, useState } from "react";
import Moment from "moment";
import { ProfileStoreContext } from "../../stores/ProfileStore";
import { observer } from "mobx-react-lite";
import Profile from "../../classes/Profile";
import Axios, { AxiosResponse, AxiosError } from "axios";

interface Props {
  profile: Profile;
}

export const ProfileCard: React.FC<Props> = observer(({ profile }) => {
  const profileStore = useContext(ProfileStoreContext);

  let [newProfile, setNewProfile] = useState<Profile>(profileStore.profile);

  const updateProfileField = (e: any) => {
    const { name, value } = e.target;
    setNewProfile({ ...newProfile, [name]: value });
  };

  const loadData = async () => {
    let profile: Profile = new Profile(
      0,
      "Not excisting profile",
      "null",
      new Date(2018, 11, 24, 10, 33, 30, 0),
      "null",
      "null",
      "null"
    );
    console.log(profile);
    await Axios.get("https://fevr.azurewebsites.net/api/profiles/2")
      .then((response: AxiosResponse) => {
        profile.Id = response.data.id;
        profile.CompanyName = response.data.companyName;
        profile.City = response.data.city;
        profile.JoinDate = response.data.joinDate;
        profile.Phone = response.data.phone;
        profile.Address = response.data.address;
        profile.Country = response.data.country;
        console.log(profile);

        profileStore.change_setProfile(profile);
        setNewProfile(profile);
      })
      .catch((error: AxiosError) => {
        console.log(error);
      });
  };

  React.useEffect(() => {
    loadData();
  }, []);

  const saveInformation = async () => {
    await Axios.put(
      `https://fevr.azurewebsites.net/api/profiles/${newProfile.Id}`,
      newProfile
    )
      .then((response: AxiosResponse) => {
        profileStore.change_setProfile(newProfile);
        profileStore.change_setIsEditing(false);
      })
      .catch((error: AxiosError) => {
        console.log(error);
      });
  };

  return (
    <div className="profile-container">
      <div className="profile-flex">
        <img
          src="https://www.flaticon.com/svg/static/icons/svg/3011/3011270.svg"
          alt="profile icon"
        ></img>

        {!profileStore.isEditing ? (
          <div className="profileData-container">
            <h3>{profileStore.profile.CompanyName}</h3>
            <label>
              {profileStore.profile.Country} - {profileStore.profile.City},{" "}
              {profileStore.profile.Address}
            </label>
            <label>+{profileStore.profile.Phone}</label>
            <label>
              Joined:{" "}
              {Moment(profileStore.profile.JoinDate).format("d MMM YYYY")}{" "}
            </label>
          </div>
        ) : (
          <div className="profileData-container">
            <label>
              Company Name:
              <input
                type="text"
                name="CompanyName"
                value={newProfile.CompanyName}
                onChange={updateProfileField}
              />
            </label>
            <label>
              Country:
              <input
                type="text"
                name="Country"
                value={newProfile.Country}
                onChange={updateProfileField}
              />
            </label>
            <label>
              City:
              <input
                type="text"
                name="City"
                value={newProfile.City}
                onChange={updateProfileField}
              />
            </label>
            <label>
              Address:
              <input
                type="text"
                name="Address"
                value={newProfile.Address}
                onChange={updateProfileField}
              />
            </label>
            <label>
              Phone:
              <input
                type="number"
                name="Phone"
                value={newProfile.Phone}
                onChange={updateProfileField}
              />
            </label>
            <label>
              Joined:{" "}
              {Moment(profileStore.profile.JoinDate).format("d MMM YYYY")}{" "}
            </label>
          </div>
        )}
      </div>

      {!profileStore.isEditing ? (
        <button
          onClick={() => {
            profileStore.change_setIsEditing(true);
          }}
        >
          Update information
        </button>
      ) : (
        <button onClick={saveInformation}>Save information</button>
      )}
    </div>
  );
});

export default ProfileCard;
