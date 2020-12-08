export default class Test{
    Id:number;
    Temperature: number;
    TimeOfDataRecording:Date;
    RaspberryPiID: number;
    HasFever: boolean;
    TemperatureF: number;


    constructor(    
        id:number,
        temperature: number,
        timeOfDataRecording:Date,
        raspberryPiID: number,
        hasFever: boolean,
        temperatureF: number) {
        this.Id=id;
        this.Temperature=temperature;
        this.TimeOfDataRecording = timeOfDataRecording;
        this.RaspberryPiID = raspberryPiID;
        this.HasFever=hasFever;
        this.TemperatureF = temperatureF;



    }




}