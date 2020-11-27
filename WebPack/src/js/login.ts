import Axios, { AxiosRequestConfig, AxiosPromise, AxiosResponse, AxiosError } from 'axios';
import { SessionChecker } from './sessionCheck';

const usernameEl : HTMLInputElement = <HTMLInputElement> document.getElementById("Username");
const passwordEl : HTMLInputElement = <HTMLInputElement> document.getElementById("Password");
document.getElementById("SignIn").addEventListener("click", AttemptLogin)

function AttemptLogin() {
    let username = usernameEl.value;
    let password = passwordEl.value;

    Axios.get(`https://localhost:44329/Api/Users/Login/${username}/${password}`)
    .then( (response : AxiosResponse) => {
        sessionStorage.setItem("SessionKey", response.data)
    }).catch((error : AxiosError) => {
        switch (error.response.status) {
            case 400:
                alert("The password did not match the user.")
                break;
            case 404:
                alert("The user does not exist.")
                break;
        }
    })
}

SessionChecker.checkSession()