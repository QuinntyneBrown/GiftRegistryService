export class Contact { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Contact {
        let contact = new Contact();
        contact.name = data.name;
        return contact;
    }
}
