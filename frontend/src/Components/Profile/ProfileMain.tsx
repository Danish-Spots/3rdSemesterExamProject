import Axios, { AxiosError, AxiosResponse } from 'axios';
import React from 'react'
import Profile from '../../classes/Profile';
import ProfileCard from './ProfileCard';
import "../../css/profile.scss";
import { JsxEmit } from 'typescript';


interface ProfileMainProps {
}

export const ProfileMain: React.FC<ProfileMainProps> = ({}) => {
        
    
    const [profileData, setProfileData] = React.useState<Profile>(new Profile(0,"Not excisting profile", "null", new Date(2018, 11, 24, 10, 33, 30, 0), "null", "null","null"))


    const loadData = async () =>
    {
      

        let profile:Profile = new Profile(0,"Not excisting profile", "null",new Date(2018, 11, 24, 10, 33, 30, 0), "null", "null","null")
            console.log(profile)
        await Axios.get("https://fevr.azurewebsites.net/api/profiles/2").then((response: AxiosResponse) => {

            //profile = JSON.parse(response.data)
             profile.Id = response.data.id
             profile.CompanyName = response.data.companyName
             profile.City = response.data.city
             profile.JoinDate = response.data.joinDate
             profile.Phone = response.data.phone
             profile.Address = response.data.address
             profile.Country = response.data.country
            console.log(profile)        

            setProfileData(profile)     

        })
        .catch((error: AxiosError) => {
            console.log(error);
          });
          
  


    }

    React.useEffect( () => {
        loadData();
    }, []
    )

    
    return (

        <div className="main-container">
            <h1> Profiles </h1>
            <ProfileCard data={profileData}></ProfileCard>
        </div>

    );
}