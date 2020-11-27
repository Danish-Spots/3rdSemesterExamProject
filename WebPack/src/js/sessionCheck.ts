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
            switch(response.status){
                case 200:
                    if (response.data.key != key){
                        alert("Invalid Session")
                        sessionStorage.removeItem("SessionKey")
                    }
                    break;
            }
        })
        .catch((error: AxiosError)=>{
            switch(error.response.status){
                case 404:
                    alert("Session not found")
                    sessionStorage.removeItem("SessionKey")
                    break;
            }
        })
    }
}
