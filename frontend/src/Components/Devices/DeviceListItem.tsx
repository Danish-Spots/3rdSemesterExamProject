import React from "react"
import RaspberryPi from "../../classes/RaspberryPi"

interface DeviceListItemProps {
    device:RaspberryPi 
}

export const DeviceListItem: React.FC<DeviceListItemProps> = (
    {device}) => 
{
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
                </div>

                <div className="buttons-container">
                    <button id="EditButton" className="editButton">
                        <img alt="Edit icon" src="/edit-pencil.png"></img>
                    </button>
                    <button id="DeleteButton" className="deleteButton">
                    <img alt="Delete icon" src="/trashbin.png"></img>
                    </button>
                </div>
            </div>
        
 
    )
    
}
export default DeviceListItem

