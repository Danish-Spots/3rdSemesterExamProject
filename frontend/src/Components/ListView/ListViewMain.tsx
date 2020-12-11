import Axios, { AxiosError, AxiosResponse } from 'axios';
import { reaction } from 'mobx';
import React from 'react'
import Test from '../../classes/Test';
import { TestListItem } from './TestListItem';
import Moment from "moment"

interface ListViewMainProps {

}

export const ListViewMain: React.FC<ListViewMainProps> = ({}) => {

    const [TestsData, SetTestsData] = React.useState<{    Id:number;
        Temperature: number;
        TimeOfDataRecording:Date;
        RaspberryPiID: number;
        HasFever: boolean;
        TemperatureF: number;}[]>([])


    const loadData = async () =>
    {
        let testList:Test[] = []

        Axios.get("https://fevr.azurewebsites.net/api/Tests")
        .then((response: AxiosResponse) =>{
            response.data.forEach(
                (o: {
                    id:number;
                    temperature: number;
                    timeOfDataRecording:Date;
                    raspberryPiID: number;
                    hasFever: boolean;
                    temperatureF: number;
                }) => {
                  let newTest: Test = new Test(
                    o.id,
                    o.temperature,
                    o.timeOfDataRecording,
                    o.raspberryPiID,
                    o.hasFever,
                    o.temperatureF
                  );
                  testList.push(newTest);
                }
              );
              SetTestsData(testList)
            })
            .catch((error: AxiosError) => {
              console.log(error);
            });

        }
        

    React.useEffect(() => {
        loadData();
    }, [])



        return (
            
            <div className="main-container">
                                <h1>Tests list view</h1>
                <div className="testList-container">

                <div className="test-header">
                    <h3>ID</h3>
                    <h3>Temperature</h3>
                    <h3>Has fever</h3>
                    <h3>Sensor ID</h3>
                    <h3>Recorded</h3>
                </div>
                     {TestsData.map((o) =>  {return <TestListItem key={o.Id} test= {o} />;})}
                </div>
            </div>
        );
}