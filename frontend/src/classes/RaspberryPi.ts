export default class RaspberryPi {
    Id: number;
    Location: string;
    IsActive: boolean;
    ProfileID: number;
    Longitude: number;
    Latitude: number;
    IsAccountConfirmed: boolean;

    constructor(id: number, location: string, isActive: boolean, profileID: number, longitude: number, latitude: number, isAccountConfirmed: boolean) {
        this.Id = id;
        this.Location = location;
        this.IsActive = isActive;
        this.ProfileID = profileID;
        this.Longitude = longitude;
        this.Latitude = latitude;
        this.IsAccountConfirmed = isAccountConfirmed;
    }
}