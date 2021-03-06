import { RouterOutlet } from "./router";
import { AuthorizedRouteMiddleware } from "./users";

export class AppRouterOutletComponent extends RouterOutlet {
    constructor(el: any) {
        super(el);
    }

    connectedCallback() {
        this.setRoutes([
            { path: "/", name: "product-master-detail", authRequired: true },
            { path: "/products", name: "product-master-detail", authRequired: true },
            { path: "/guests", name: "guest-master-detail", authRequired: true },
            { path: "/contacts", name: "contact-master-detail", authRequired: true },
            { path: "/events", name: "event-master-detail", authRequired: true },
            { path: "/login", name: "login" },
            { path: "/error", name: "error" },
            { path: "*", name: "not-found" }
        ] as any);

        this.use(new AuthorizedRouteMiddleware());

        super.connectedCallback();
    }

}

customElements.define(`ce-app-router-oulet`, AppRouterOutletComponent);