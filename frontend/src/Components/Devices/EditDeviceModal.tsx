import Axios, { AxiosError, AxiosResponse } from 'axios';
import React, { MouseEventHandler, ReactPortal, useEffect, useState } from 'react'
import ReactDOM from 'react-dom';
import "../../css/editDevice.scss"
import RaspberryPi from '../../classes/RaspberryPi'

interface EditDeviceProps {
    closeModal: () => void
    showModal: boolean
    device: RaspberryPi
}

export const EditDeviceModal: React.FC<EditDeviceProps> = ({showModal, closeModal, device}) => {
    const [location, setLocation] = useState<string>(device.Location)
    const [isActive, setIsActive] = useState<boolean>(device.IsActive)
    if (!showModal) return null
    const EditDevice = async () => {
        
        // Grab Location coord
        Axios.get("https://nominatim.openstreetmap.org/search?q=" + location.replace(" ", "%20")+"&format=jsonv2")
        .then((response: AxiosResponse) => {
            try {
                let data = response.data[0]
                if (data === undefined){
                    throw Error
                }
                else{
                    let lat = data["lat"]
                    let lon = data["lon"]
                    Axios.put(`https://fevr.azurewebsites.net/api/RaspberryPis/${device.Id}`, {
                        id: device.Id, 
                        location: location, 
                        isActive: isActive, 
                        profileID: device.ProfileID, 
                        longitude: lon,
                        latitude: lat,
                        isAccountConfirmed: device.IsAccountConfirmed
                    }
                    )
                    .then((response: AxiosResponse) => {
                        console.log("Updated Successfully!")
                        closeModal();
                    })
                    .catch((error: AxiosError) => {
                        console.log(error)
                    })
                }
            } 
            catch (error) {
                console.log("Error thrown")
            }
            
        })
        .catch((error: AxiosError) => {
            console.log("Location not found or api not available")
        })
       
    }    
    

    return ReactDOM.createPortal(
        <div className="OverlayContainer">
            <div className="ModalContainer-edit">
                <h3>Enter the information you wish to change</h3>
                <form>
                    <label>Location: 
                        <input id="devicename-input" type="text" value={location} onChange={e => setLocation(e.target.value)}/>
                    </label>
                    <br/>
                    <label>Is Active: 
                        <input id="isActive-input" type="checkbox" checked={isActive} onChange={e => setIsActive(e.target.checked)}/>
                    </label>
                    
                    <p>*After editing a device the page may be reloaded.</p>

                    
                    <div className="editButtonsContainer">
                    <button type="button" onClick={closeModal}>Cancel</button>
                    <button type="button" onClick={EditDevice}>Confirm</button>
                    </div>
                </form>
                
            </div>
        </div>, document.getElementById("portal") as HTMLElement
    )
    
}

export default EditDeviceModal