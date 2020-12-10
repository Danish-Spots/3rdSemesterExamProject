import React, { useEffect, useState } from "react"
import ReactDOM from "react-dom"
import RaspberryPi from "../../classes/RaspberryPi"
import DeleteDeviceModal from "./DeleteDeviceModal"
import DeleteDevice from "./DeleteDeviceModal"
import EditDeviceModal from "./EditDeviceModal"

interface DeviceListItemProps {
    device:RaspberryPi 
}

export const DeviceListItem: React.FC<DeviceListItemProps> = (
    {device}) => {
    let [modalIsShown, setModalIsShown] = useState<boolean>(false)
    let [modalIsShownEdit, setModalIsShownEdit] = useState<boolean>(false)
    

    return(
        
          <div className="deviceListItem">
                <div className="text-container">
                    <label>#{device.Id}</label>
                    {/** Commenting Ctr+K+C, uncomment Ctr+U+C */}
                    <label id="deviceLocation">{device.Location}</label>
                    {/* <label>{senName}</label> */}
                   {/* <label>{postCode}</label>*/}
                    {/*<label>{city}</label>*/ }
                    <label>{device.IsActive ? "Active" : "Inactive"}</label>
                </div>
                
                <div className="buttons-container">
                    <button id="EditButton" className="editButton" onClick={() => setModalIsShownEdit(true)}>
                        <img alt="Edit icon" src="/edit-pencil.png"></img>
                    </button>
                    <button id="DeleteButton" className="deleteButton" onClick={() => setModalIsShown(true)}>                                           
                    <img alt="Delete icon" src="/trashbin.png"></img>
                    </button>
                    <DeleteDeviceModal device={device} showModal={modalIsShown} closeModal={() => setModalIsShown(false)}/>   
                    
                </div>
                <EditDeviceModal device={device} showModal={modalIsShownEdit} closeModal={() => setModalIsShownEdit(false)}/>
            </div>
        
 
    )
    
}
export default DeviceListItem

