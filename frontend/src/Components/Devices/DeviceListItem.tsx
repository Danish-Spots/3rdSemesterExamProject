import React from "react"


export const DeviceListItem: React.FC<{}> = () => 
{
    return(
        <div className="device-container">
          <div className="deviceListItem">
            <div>
                <label>#1</label>
                <label>SensEntrance</label>
                <label>4000</label>
                <label>Roskilde Eriksvej</label>
                <label>Active</label>
                <button className="editButton"></button>
                <button className="deleteButton"></button>
            </div>
            </div>
        </div>
 
    )
    
}