export default class Profile
{
    Id: number;
    CompanyName:string;
    City:string;
    JoinDate: Date;
    Phone:string;
    Address:string;
    Country:string;


    /**
     *
     */
    constructor(   
        id: number,
        companyName:string,
        city:string,
        joinDate: Date,
        phone:string,
        address:string,
        country:string
) 

        {
            this.Id = id;
            this.CompanyName = companyName;
            this.City = city;
            this.Address = address;
            this.Country = country;
            this.Phone = phone;
            this.JoinDate = joinDate;
        
        }
}