import Axios, { AxiosError, AxiosResponse } from "axios"
import React, { useEffect, useState } from "react"
import RaspberryPi from "../../classes/RaspberryPi"


interface AddDeviceListItemProps {
    device:RaspberryPi 
    closeModal: () => void
    loadData: () => void
}

export const AddDeviceListItem: React.FC<AddDeviceListItemProps> = (
    {device, closeModal, loadData}) => {

    const ConfirmDevice = () => {
        Axios.put(`https://fevr.azurewebsites.net/api/RaspberryPis/${device.Id}`, {
                        id: device.Id, 
                        location: device.Location, 
                        isActive: device.IsActive, 
                        profileID: device.ProfileID, 
                        longitude: device.Longitude.toString(),
                        latitude: device.Latitude.toString(),
                        isAccountConfirmed: true
                    }
                    )
                    .then((response: AxiosResponse) => {
                        console.log("Updated Successfully!")
                        closeModal()
                        loadData()
                        
                    })
                    .catch((error: AxiosError) => {
                        console.log({
                            id: device.Id, 
                            location: device.Location, 
                            isActive: device.IsActive, 
                            profileID: device.ProfileID, 
                            longitude: device.Longitude,
                            latitude: device.Latitude,
                            isAccountConfirmed: true
                        })
                        console.log(error)
                    })
    }

    return(
        
          <div className="deviceListItem">
                <div className="text-container">
                    <label>#{device.Id}</label>

                    {/** Commenting Ctr+K+C, uncomment Ctr+U+C */}
                    <label>{device.Location}</label>
                    {/* <label>{senName}</label> */}
                   {/* <label>{postCode}</label>*/}
                    {/*<label>{city}</label>*/ }
                    <label>{device.IsActive ? "Active" : "Inactive"}</label>
                    <label>{device.IsAccountConfirmed ? "Confirmed" : "Not Confirmed"}</label>
                </div>
                
                <div className="buttons-container">
                    <button type="button" onClick={() => ConfirmDevice()}> Confirm Device
                    </button>  
                </div>
            </div>
        
 
    )
    
}
export default AddDeviceListItem

