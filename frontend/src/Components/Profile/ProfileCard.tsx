import React from 'react'
import Profile from '../../classes/Profile';

interface ProfileCardProps {
        
}

export const ProfileCard: React.FC<ProfileCardProps> = () => {
        return (
            <div className="profile-container">
                    <img src="https://www.flaticon.com/svg/static/icons/svg/3011/3011270.svg" alt="profile icon"></img>
                    
                
                    <div className="profileData-container">
                        <h3>BIODICAL ApS ROSKILDE </h3>
                        <label>DK - 4000 Roskilde, Holbæksvej 42</label>
                        <label>+45 56 78 23</label>
                        <label>Joined: 05/12/2020</label>
                    </div>

                    <button>Update information</button>
            </div>
        );
}

export default ProfileCard