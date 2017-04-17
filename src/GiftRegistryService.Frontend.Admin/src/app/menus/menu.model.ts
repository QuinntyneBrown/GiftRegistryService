export class Menu { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Menu {
        let menu = new Menu();
        menu.name = data.name;
        return menu;
    }
}
