import React, { useContext } from 'react'
import DeviceListItem from './DeviceListItem'
import "../../css/devices.scss"
import Axios, { AxiosResponse, AxiosError } from "axios";
import AddDeviceModal from "./AddDeviceModal"
import RaspberryPi from '../../classes/RaspberryPi';
import { UserStoreContext } from '../../stores/UserStore';

interface DevicesMainProps {
        
}

export const DevicesMain: React.FC<DevicesMainProps> = ({}) => {

    const [modalIsShown, setModalIsShown] = React.useState<boolean>(false)
    const [deviceData, setDeviceData] = React.useState<RaspberryPi[]>()
    const userStore = useContext(UserStoreContext);
    let profileId = userStore.profileID
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
            longitude: string;
            latitude: string;
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
       if (o.ProfileID === profileId && o.IsAccountConfirmed === true) 
       {
           userPiList.push(o)
       } 
    } )

    setDeviceData([...userPiList])
  };


    React.useEffect(() => {
      loadPiData()
    }, [])


    
    return (

            <div className="main-container">
                <div className="head-container">  
                <h1>My Devices</h1>

                    <div className="modalContainer">
                             <button onClick={() => setModalIsShown(true)} className="AddDeviceButton">Add device</button>
                             <AddDeviceModal loadData={loadPiData} closeModal={() => setModalIsShown(false)} showModal={modalIsShown}/>
                    </div>

                </div>
                <div className="devices-header">
                    <h3>ID</h3>
                    <h3>Location</h3>
                    <h3>Status</h3>
                    <h2></h2>
                </div>

                <div className="device-container">
                    
                    {
                        deviceData?.map( (o:RaspberryPi) =>  <DeviceListItem key={o.Id} device={o}/>)
                    }


                </div>

            </div>
        );
}

export default DevicesMain