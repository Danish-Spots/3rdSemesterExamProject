import React from 'react'
import ReactDOM from 'react-dom';
import "../../css/addDevice.scss";


interface AddDeviceModalProps {
     // isActive:boolean
}


export const AddDeviceModal: React.FC<AddDeviceModalProps> = () => {
    //  if (!isActive) return null;
      return ReactDOM.createPortal(
        <div className="OverlayContainer" >
        <div className="ModalContainer" >
          <h1>Add new device</h1>
          <form>


          <div className="AddDeviceHeader">

            <div className="inputContainer">
              <h2>Pending devices</h2>
            </div>
            <div className="inputContainer">
              <label>Profile ID</label>
              <label>#1350</label>
            </div>
          </div>

          <div className="pendingDeviceContainer">

          </div>

          </form>
        </div>
        </div>
      , document.getElementById('portal') as HTMLElement 
      )
}

export default AddDeviceModal