import React from 'react'
import Profile from '../../classes/Profile';

interface ProfileCardProps {
    data:Profile
        
}

export const ProfileCard: React.FC<ProfileCardProps> = (data) => {
        return (
            <div className="profile-container">
                    <img src="https://www.flaticon.com/svg/static/icons/svg/3011/3011270.svg" alt="profile icon"></img>
                    
                
                    <div className="profileData-container">
                        <h3>{data.data.CompanyName}</h3>
                       <label>{data.data.Country} - {data.data.City}, {data.data.Address}</label>
                        <label>+{data.data.Phone}</label>
                        <label>Joined: </label>
                    </div>

                    <button>Update information</button>
            </div>
        );
}

export default ProfileCard