export default class MarkerData {
    ID: number;
    Text: string;
    Lat: number;
    Lon: number;

    constructor(id : number, text : string, lat : number, lon : number) {
        this.ID = id;
        this.Text = text;
        this.Lat = lat;
        this.Lon = lon
    }

}