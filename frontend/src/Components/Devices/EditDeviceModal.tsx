import Axios, { AxiosError, AxiosResponse } from 'axios';
import React, { MouseEventHandler, ReactPortal, useEffect, useState } from 'react'
import ReactDOM from 'react-dom';
import RaspberryPi from '../../classes/RaspberryPi'

interface EditDeviceProps {
    closeModal: () => void
    showModal: boolean
    device: RaspberryPi
}

export const EditDeviceModal: React.FC<EditDeviceProps> = ({showModal, closeModal, device}) => {
    const [location, setLocation] = useState<string>("")
    const [isActive, setIsActive] = useState<boolean>(device.IsActive)
    if (!showModal) return null
    const EditDevice = async () => {
        
        // Grab Location coord
        console.log(device, device.Id)
        Axios.put(`https://fevr.azurewebsites.net/api/RaspberryPis/${device.Id}`, {
            id: device.Id, 
            location: location, 
            isActive: device.IsActive, 
            profileID: device.ProfileID, 
            longitude: device.Longitude,
            latitude: device.Latitude,
            isAccountConfirmed: device.IsAccountConfirmed
        }
        )
        .then((response: AxiosResponse) => {
            console.log("Updated Successfully!")
        })
        .catch((error: AxiosError) => {
            console.log(error)
        })
    }    
    

    return ReactDOM.createPortal(
        <div className="OverlayContainer">
            <div className="ModalContainer">
                <h3>Enter the information you wish to change</h3>
                <form>
                    <label>Location: 
                        <input type="text" value={location} onChange={e => setLocation(e.target.value)}/>
                    </label>
                    <br/>
                    <label>Is Active: 
                        <input type="checkbox" checked={isActive} onChange={e => setIsActive(e.target.checked)}/>
                    </label>
                    <button type="button" onClick={closeModal}>X</button>
                    <button type="button" onClick={closeModal}>Cancel</button>
                    <button type="button" onClick={EditDevice}>Confirm</button>
                </form>
                
            </div>
        </div>, document.getElementById("portal") as HTMLElement
    )
    
}

export default EditDeviceModal