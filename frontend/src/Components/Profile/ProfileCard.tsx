import React from 'react'
import Profile from '../../classes/Profile';
import Moment from "moment"

interface ProfileCardProps {
    data:Profile
        
}

export const ProfileCard: React.FC<ProfileCardProps> = ({data}) => {
        return (
            <div className="profile-container">
            <div className="profile-flex">
                    <img src="https://www.flaticon.com/svg/static/icons/svg/3011/3011270.svg" alt="profile icon"></img>
                    
                
                    <div className="profileData-container">
                        <h3>{data.CompanyName}</h3>
                       <label>{data.Country} - {data.City}, {data.Address}</label>
                        <label>+{data.Phone}</label>
                        <label>Joined: {Moment(data.JoinDate).format('d MMM YYYY')} </label>
                    </div>
            </div>
            <button>Update information</button>

            </div>
        );
}

export default ProfileCard