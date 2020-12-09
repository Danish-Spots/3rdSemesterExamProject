import Axios, { AxiosError, AxiosResponse } from 'axios';
import React, { MouseEventHandler, ReactPortal, useEffect, useState } from 'react'
import ReactDOM from 'react-dom';
import RaspberryPi from '../../classes/RaspberryPi'
import "../../css/deleteDevice.scss";


interface DeleteDeviceProps {
    closeModal: () => void
    showModal: boolean
    device: RaspberryPi
}

export const DeleteDeviceModal: React.FC<DeleteDeviceProps> = ({showModal, closeModal, device}) => {
    
    const DeleteDevice = async () => {
        Axios.delete(`https://fevr.azurewebsites.net/api/RaspberryPis/${device.Id}`
        )
        .then((response: AxiosResponse) => {
            //Waiting on styles
            console.log("Deleted Succesfully")
        })
        .catch((error: AxiosError) => {
            //Waiting on Styles
            console.log(error)
        })
    }
    
    if (!showModal) return null
    return ReactDOM.createPortal(
        <div className="OverlayContainer-delete">
            <div className="ModalContainer-delete">
                <h3>Are you sure that you want to delete this device from the system?</h3>
                
                <div className="deleteButtonsContainer">
                <button onClick={closeModal}>No</button>
                <button onClick={DeleteDevice}>Yes</button>
                </div>
            
            </div>
        </div>, document.getElementById("portal") as HTMLElement)
    
}

export default DeleteDeviceModal