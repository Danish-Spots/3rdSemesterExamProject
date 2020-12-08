import React from 'react'
import DeviceListItem from './DeviceListItem'
import "../../css/devices.scss"
import Axios, { AxiosResponse, AxiosError } from "axios";
import AddDeviceModal from "./AddDeviceModal"
import RaspberryPi from '../../classes/RaspberryPi';

interface DevicesMainProps {
        
}

export const DevicesMain: React.FC<DevicesMainProps> = ({}) => {

    const [modalIsShown, setModalIsShown] = React.useState<boolean>()
    const [deviceData, setDeviceData] = React.useState<RaspberryPi[]>()
    const triggerText = 'Add device';


//We save it in a constant so we can run it as Async
const loadPiData = async () => {
    let pis: RaspberryPi[] = [];
    await Axios.get("https://fevr.azurewebsites.net/api/RaspberryPis")
      .then((response: AxiosResponse) => {
        response.data.forEach(
          (o: {
            id: number;
            location: string;
            isActive: boolean;
            profileID: number;
            longitude: number;
            latitude: number;
            isAccountConfirmed: boolean;
          }) => {
            let newPi: RaspberryPi = new RaspberryPi(
              o.id,
              o.location,
              o.isActive,
              o.profileID,
              +o.longitude,
              +o.latitude,
              o.isAccountConfirmed
            );
            pis.push(newPi);
          }
        );
      })
      .catch((error: AxiosError) => {
        console.log(error);
      });

    let userPiList:RaspberryPi[] = []
    pis.forEach( (o) => 
    {
       if (o.ProfileID === 2) 
       {
           userPiList.push(o)
       } 
    } )

    setDeviceData([...userPiList])
  };


    React.useEffect(() => {

        loadPiData()
    })


    
    return (

            <div className="main-container">
                <div className="head-container">  
                <h1>My Devices</h1>

                    <div className="modalContainer">
                             <button onClick={() => setModalIsShown(true)} className="AddDeviceButton">Add device</button>
                       { modalIsShown ? <AddDeviceModal/> : null}
                    </div>

                </div>


                <div className="device-container">
                    {
                        deviceData?.map( (o:RaspberryPi) => <DeviceListItem device={o} ></DeviceListItem> )
                    }


                </div>

            </div>
        );
}

export default DevicesMain