import { fetch } from "../utilities";
import { Guest } from "./guest.model";

export class GuestService {
    constructor(private _fetch = fetch) { }

    private static _instance: GuestService;

    public static get Instance() {
        this._instance = this._instance || new GuestService();
        return this._instance;
    }

    public get(): Promise<Array<Guest>> {
        return this._fetch({ url: "/api/guest/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { guests: Array<Guest> }).guests;
        });
    }

    public getById(id): Promise<Guest> {
        return this._fetch({ url: `/api/guest/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { guest: Guest }).guest;
        });
    }

    public add(guest) {
        return this._fetch({ url: `/api/guest/add`, method: "POST", data: { guest }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/guest/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
