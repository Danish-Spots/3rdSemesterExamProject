import React from "react"

interface DeviceListItemProps {
    id:number
    senName:string
    postCode:number
    city:string
    isActive:boolean
    isEditable:boolean
}

export const DeviceListItem: React.FC<DeviceListItemProps> = ({id,senName,postCode,city,isActive}) => 
{
    return(
        
          <div className="deviceListItem">
                <div className="text-container">
                    <label>{id}</label>
                    <label>{senName}</label>
                    <label>{postCode}</label>
                    <label>{city}</label>
                    <label>{isActive ? "Active" : "Inactive"}</label>
                    <button className="editButton"></button>
                    <button className="deleteButton"></button>
                </div>
            </div>
        
 
    )
    
}
export default DeviceListItem

