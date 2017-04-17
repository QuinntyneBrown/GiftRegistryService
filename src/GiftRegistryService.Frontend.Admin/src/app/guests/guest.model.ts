export class Guest { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Guest {
        let guest = new Guest();
        guest.name = data.name;
        return guest;
    }
}
