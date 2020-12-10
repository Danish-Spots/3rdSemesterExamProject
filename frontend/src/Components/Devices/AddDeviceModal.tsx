import React, { useContext, useEffect } from 'react'
import ReactDOM from 'react-dom';
import RaspberryPi from '../../classes/RaspberryPi';
import "../../css/addDevice.scss";
import { UserStoreContext } from "../../stores/UserStore"
import Axios, { AxiosError, AxiosResponse } from "axios"
import AddDeviceListItem from './AddDeviceListItem';


interface AddDeviceModalProps {
     // isActive:boolean
     closeModal: () => void
     showModal: Boolean
     loadData: () => void 
}


export const AddDeviceModal: React.FC<AddDeviceModalProps> = ({closeModal, showModal, loadData}) => {
    //  if (!isActive) return null;
    const userStore = useContext(UserStoreContext);
    const [deviceData, setDeviceData] = React.useState<RaspberryPi[]>()
    let profileId = userStore.profileID
    
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
         if (o.ProfileID === profileId) 
         {
           if (o.IsAccountConfirmed === false){
            userPiList.push(o)
           }  
         } 
      })
  
      setDeviceData([...userPiList])
    };
    React.useEffect(() => {
      loadPiData()
    }, [])

    

    if (!showModal) return null
      return ReactDOM.createPortal(
        <div className="OverlayContainer" >
        <div className="ModalContainer" >
          <div className="addDeviceTitle">   
          <h1>Add new device</h1>       
          <button id="closeButton" onClick={closeModal}>X</button>
          </div>
          
          <form>
          <div className="AddDeviceHeader">

            <div className="inputContainer">
              <h2>Pending devices</h2>
            </div>
            <div className="inputContainer">
              <label>Profile ID</label>
              <label>#{profileId}</label>
            </div>            
          </div>

          <div className="pendingDeviceContainer">
            {
              deviceData?.map((o:RaspberryPi) => <AddDeviceListItem loadData={loadData} closeModal={closeModal} key={o.Id} device={o}/>)
            }
          </div>
            
          </form>
          
        </div>
        </div>
      , document.getElementById('portal') as HTMLElement 
      )
}

export default AddDeviceModal