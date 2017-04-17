export class CurrentFeature {
    constructor() {
    }

    public static _instance: CurrentFeature;

    public static get random(): CurrentFeature {

        return this._instance;
    }

    public name: string;

    public displayName: string;
}