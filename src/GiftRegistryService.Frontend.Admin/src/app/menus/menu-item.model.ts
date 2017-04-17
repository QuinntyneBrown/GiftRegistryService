export class MenuItem { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): MenuItem {
        let menuItem = new MenuItem();
        menuItem.name = data.name;
        return menuItem;
    }
}
