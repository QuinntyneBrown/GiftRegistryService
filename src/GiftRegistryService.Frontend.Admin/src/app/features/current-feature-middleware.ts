import { RouterMiddleware, Router } from "../router";
import { createElement, Storage, TOKEN_KEY, Log } from "../utilities";
import { environment } from "../environment";

export class CurrentFeatureMiddleware extends RouterMiddleware {
    constructor(private _routeName = null,
        private _router: Router = Router.Instance,
        private _storage: Storage = Storage.Instance,
    ) {
        super();

    }

    public beforeViewTransition(options: RouteChangeOptions) {

    }

    public onViewTransition(options: RouteChangeOptions): HTMLElement {
        return null;
    }

    public afterViewTransition(options: RouteChangeOptions) {

    }

}