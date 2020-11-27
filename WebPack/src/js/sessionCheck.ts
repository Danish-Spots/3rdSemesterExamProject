import Axios, { AxiosRequestConfig, AxiosPromise, AxiosResponse, AxiosError } from 'axios';

interface UserSession {
    id: number
    key: string
    userID: number
}


export class SessionChecker{
 
    static checkSession(){
        let key = sessionStorage.getItem("SessionKey");
    
        Axios.get(`https://localhost:44329/api/Sessions/getSessionKey=${key}`)
        .then( (response: AxiosResponse<UserSession>)=> {
            if (response.status== 200){
                if (response.data.key != key){
                    alert("Invalid Session")
                    sessionStorage.removeItem("SessionKey")
                    console.log("Incorrect Session")
                }
                else{
                    console.log("Valid Session")
                }
            }
        })
        .catch((error: AxiosError)=>{
            if (error.response.status == 404){
                alert("Session not found")
                sessionStorage.removeItem("SessionKey")
                console.log("Invalid Session: " + error.response.status)
            }
        })
    }
}


