import React from 'react'
import DeviceListItem from './DeviceListItem'
import "../../css/devices.scss"
import Axios, { AxiosResponse, AxiosError } from "axios";


interface DevicesMainProps {
        
}

export const DevicesMain: React.FC<DevicesMainProps> = ({}) => {


    const [deviceData, setDeviceData] = React.useState<{ senName: number; postCode: number; city: string; isActive:boolean }[]>()

    React.useEffect(() => {
        const loadDevicesData = async () => 
        {
            Axios.get("https://fevr.azurewebsites.net/api/RaspberryPis")
            .then((response: AxiosResponse) => {
                setDeviceData(response.data);
              })
              .catch((error: AxiosError) => {
                console.log(error);
              });
        }

        console.log({deviceData})
    })


    return (

            <div className="main-container">
                <div className="head-container">  
                <h1>My Devices</h1>
                <button className="AddDeviceButton">Add device</button>
                </div>
                <div className="device-container">
                    <DeviceListItem id={1} senName="Entrance" postCode={4000} city="Roskilde" isActive={true} isEditable={false}></DeviceListItem>
                    <DeviceListItem id={2} senName="Back door" postCode={4000} city="Roskilde" isActive={true} isEditable={false}></DeviceListItem>
                    <DeviceListItem id={3} senName="Front door" postCode={4000} city="Roskilde" isActive={true} isEditable={false}></DeviceListItem>
                    <DeviceListItem id={4} senName="Libary" postCode={4000} city="Roskilde" isActive={true} isEditable={false}></DeviceListItem>
                </div>
            </div>
        );
}

export default DevicesMain