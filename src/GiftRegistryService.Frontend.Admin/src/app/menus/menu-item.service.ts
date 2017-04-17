import { fetch } from "../utilities";
import { MenuItem } from "./menu-item.model";

export class MenuItemService {
    constructor(private _fetch = fetch) { }

    private static _instance: MenuItemService;

    public static get Instance() {
        this._instance = this._instance || new MenuItemService();
        return this._instance;
    }

    public get(): Promise<Array<MenuItem>> {
        return this._fetch({ url: "/api/menuitem/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { menuItems: Array<MenuItem> }).menuItems;
        });
    }

    public getById(id): Promise<MenuItem> {
        return this._fetch({ url: `/api/menuitem/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { menuItem: MenuItem }).menuItem;
        });
    }

    public add(menuItem) {
        return this._fetch({ url: `/api/menuitem/add`, method: "POST", data: { menuItem }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/menuitem/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
