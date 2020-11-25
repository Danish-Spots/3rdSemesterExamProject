import Axios from "../../node_modules/axios/index";
import axios, { AxiosResponse,AxiosError} from "../../node_modules/axios/index"
import { ITest } from "./ITest";

let url : string = "https://fevr.azurewebsites.net/";
let localurl :string =  "https://localhost:44329/"
let noTestsLabel : HTMLLabelElement = <HTMLLabelElement> document.getElementById("NoTestsLabel")
let noFeverLabel : HTMLLabelElement = <HTMLLabelElement> document.getElementById("NoFeverLabel")
let noNegativeLabel :  HTMLLabelElement = <HTMLLabelElement> document.getElementById("NoNegativeLabel")

window.onload =   function(){ noNegativeLabel.innerText = "Teve";
    console.log("Teve")
}

 window.onload = function() {
     
    Axios.get<number>(url+"NoTests")
    .then(function(response:AxiosResponse<number>):void
    {
      let noTests : number = response.data;
      noTestsLabel.innerHTML = String(noTests);
      console.log(noTests)
    })
    .catch(function (error:AxiosError) : void
    {
      console.log("Error: "+ error);
    })


    Axios.get<number>(url+ "NoFever")
    .then(
        function(response:AxiosResponse<number>):void
        {
            let noFever : number = response.data;
            noFeverLabel.innerHTML = String(noFever);
            console.log(noFever)
        }
    ) 

    .catch(function (error:AxiosError) : void
    {
      console.log("Error: "+ error);
    })
}