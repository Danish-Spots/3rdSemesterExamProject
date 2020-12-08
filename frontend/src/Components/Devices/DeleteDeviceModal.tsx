import React, { MouseEventHandler, ReactPortal, useEffect, useState } from 'react'
import ReactDOM from 'react-dom';
import RaspberryPi from '../../classes/RaspberryPi'

interface DeleteDeviceProps {
    closeModal: () => void
    showModal: boolean
    device: RaspberryPi
}



export const DeleteDeviceModal: React.FC<DeleteDeviceProps> = ({showModal, closeModal, device}) => {
    if (!showModal) return null
    return ReactDOM.createPortal(
        <div className="OverlayContainer">
            <div className="ModalContainer">
                <h3>Are you sure that you want to delete this device from the system?</h3>
                <button onClick={closeModal}>X</button>
                <button>No</button>
                <button>Yes</button>
                <textarea>{device.Id}</textarea>
            </div>
        </div>, document.getElementById("portal") as HTMLElement)
    
}


// const modalRoot = document.getElementById('portal');

// class DeleteDeviceModal extends React.Component{
//     el: HTMLDivElement;
//     constructor(props: DeleteDeviceProps){
//         super(props)
//         this.el = document.createElement("div")
//         this.el.setAttribute("id", "portal-div-root")
//         this.exitClickHandler = this.exitClickHandler.bind(this)

//     }

//     componentDidMount(){
//         modalRoot?.appendChild(this.el)
//     }

//     exitClickHandler(){
//         modalRoot?.removeChild(this.el) 

//     }

//     // componentWillUnmount(){
//     //     modalRoot?.removeChild(this.el)
//     // }

//     render(){
//         return ReactDOM.createPortal(
//                         <div className="OverlayContainer">
//                         <div className="ModalContainer">
//                             <h3>Are you sure that you want to delete this device from the system?</h3>
//                             <button onClick={this.exitClickHandler}>X</button>
//                             <button>No</button>
//                             <button>Yes</button>
//                         </div>
//                     </div>,
//                     this.el
//                 )
//     }
// }

export default DeleteDeviceModal