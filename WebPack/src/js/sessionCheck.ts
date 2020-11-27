import Axios, { AxiosRequestConfig, AxiosPromise, AxiosResponse, AxiosError } from 'axios';

export interface UserSession {
    id: number
    key: string
    userID: number
}

class SessionChecker{
 
    checkSession(){
        let key = sessionStorage.getItem("SessionKey");
    
        Axios.get(`https://fevr.azurewebsites.net/api/Sessions/getSessionKey=${key}`)
        .then( (response: AxiosResponse<UserSession>)=> {
            if (response.status== 200){
                if (response.data.key != key){
                    alert("Invalid Session")
                    sessionStorage.removeItem("SessionKey")
                }
            }
        })
        .catch((error: AxiosError)=>{
            if (error.response.status == 404){
                alert("Session not found")
                sessionStorage.removeItem("SessionKey")
            }
        })
    }
}
