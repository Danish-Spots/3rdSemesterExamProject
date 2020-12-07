import Axios, { AxiosError, AxiosResponse } from 'axios';
import React from 'react'
import Profile from '../../classes/Profile';
import ProfileCard from './ProfileCard';
import "../../css/profile.scss";


interface ProfileMainProps {
}

export const ProfileMain: React.FC<ProfileMainProps> = ({}) => {
        
    
    const [profileData, setProfileData] = React.useState<Profile>(new Profile(0,"Not excisting profile", "null", new Date(2018, 11, 24, 10, 33, 30, 0), "null", "null","null"))


    const loadData = async () =>
    {
        let profile:Profile = new Profile(0,"Not excisting profile", "null",new Date(2018, 11, 24, 10, 33, 30, 0), "null", "null","null")
            console.log(profile)
        await Axios.get("https://fevr.azurewebsites.net/api/profiles/2").then((response: AxiosResponse) => {
        response.data.forEach(
                (o: {
                    id: number,
                    companyName:string,
                    city:string,
                    joinDate: Date,
                    phone:string,
                    address:string,
                    country:string
                }) => {
                  let newProfile: Profile = new Profile(
                    o.id,
                    o.companyName,
                    o.city,
                    o.joinDate,
                    o.phone,
                    o.address,
                    o.country 
                  );    
                  console.log(profileData)
                }
                ); 


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
            <ProfileCard ></ProfileCard>
        </div>

    );
}