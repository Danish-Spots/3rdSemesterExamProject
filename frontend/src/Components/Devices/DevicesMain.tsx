import React from 'react'
import DeviceListItem from './DeviceListItem'
import "../../css/devices.scss"


interface DevicesMainProps {
        
}

export const DevicesMain: React.FC<DevicesMainProps> = ({}) => {
        return (

            <div className="main-container">
                <h1>Devices</h1>
                <div className="device-container">
                    <DeviceListItem id={1} senName="Entrance" postCode={4000} city="Roskilde" isActive={true} isEditable={false}></DeviceListItem>
                    <DeviceListItem id={2} senName="Back door" postCode={4000} city="Roskilde" isActive={true} isEditable={false}></DeviceListItem>
                    <DeviceListItem id={3} senName="Front door" postCode={4000} city="Roskilde" isActive={true} isEditable={false}></DeviceListItem>
                    <DeviceListItem id={4} senName="Libary entrance" postCode={4000} city="Roskilde" isActive={true} isEditable={false}></DeviceListItem>

                </div>
            </div>
        );
}

export default DevicesMain