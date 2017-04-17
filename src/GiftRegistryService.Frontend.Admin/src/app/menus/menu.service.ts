import { fetch } from "../utilities";
import { Menu } from "./menu.model";

export class MenuService {
    constructor(private _fetch = fetch) { }

    private static _instance: MenuService;

    public static get Instance() {
        this._instance = this._instance || new MenuService();
        return this._instance;
    }

    public get(): Promise<Array<Menu>> {
        return this._fetch({ url: "/api/menu/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { menus: Array<Menu> }).menus;
        });
    }

    public getById(id): Promise<Menu> {
        return this._fetch({ url: `/api/menu/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { menu: Menu }).menu;
        });
    }

    public add(menu) {
        return this._fetch({ url: `/api/menu/add`, method: "POST", data: { menu }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/menu/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
