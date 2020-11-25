import Axios from "../../node_modules/axios/index";
import axios, { AxiosResponse,AxiosError} from "../../node_modules/axios/index"
import { ITest } from "./ITest";

let url : string = "https://fevr.azurewebsites.net/";
let TestsLabel : HTMLLabelElement = <HTMLLabelElement> document.getElementById("TestsLabel")
let FeverLabel : HTMLLabelElement = <HTMLLabelElement> document.getElementById("FeverLabel")
let NoFeverLabel :  HTMLLabelElement = <HTMLLabelElement> document.getElementById("NoFeverLabel")
let TestsTodayLabel :  HTMLLabelElement = <HTMLLabelElement> document.getElementById("TestsTodayLabel")
let FeverTodayLabel :  HTMLLabelElement = <HTMLLabelElement> document.getElementById("FeverTodayLabel")


 window.onload = function() {
     
    Axios.get<number>(url+"TestCount")
    .then(function(response:AxiosResponse<number>):void
    {
      let noTests : number = response.data;
      TestsLabel.innerHTML = String(noTests);
      console.log(noTests)
    })
    .catch(function (error:AxiosError) : void
    {
      console.log("Error: "+ error);
    })


    Axios.get<number>(url+ "FeverCount")
    .then(
        function(response:AxiosResponse<number>):void
        {
            let noFever : number = response.data;
            FeverLabel.innerHTML = String(noFever);
            console.log(noFever)
        }
    ) 
    .catch(function (error:AxiosError) : void
    {
      console.log("Error: "+ error);
    })

    Axios.get<number>(url+ "NoFeverCount")
    .then(
        function(response:AxiosResponse<number>):void
        {
            let noFever : number = response.data;
            NoFeverLabel.innerHTML = String(noFever);
            console.log(noFever)
        }
    ) 
    .catch(function (error:AxiosError) : void
    {
      console.log("Error: "+ error);
    })

    Axios.get<number>(url+ "FeverCount")
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