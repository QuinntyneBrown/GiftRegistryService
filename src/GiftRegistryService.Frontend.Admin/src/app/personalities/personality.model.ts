export class Personality { 
    public id:any;
    public name: any;
    public imageUrl: any;
    public firstname: any;
    public lastname: any;
    public description: any;
    public abstract: any;

    public fromJSON(data: any): Personality {
        let personality = new Personality();
        personality.name = data.name;
        return personality;
    }
}
