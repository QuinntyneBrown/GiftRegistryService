import { MenuAdd, MenuDelete, MenuEdit, menuActions } from "./menu.actions";
import { Menu } from "./menu.model";
import { MenuService } from "./menu.service";

const template = require("./menu-master-detail.component.html");
const styles = require("./menu-master-detail.component.scss");

export class MenuMasterDetailComponent extends HTMLElement {
    constructor(
        private _menuService: MenuService = MenuService.Instance	
	) {
        super();
        this.onMenuAdd = this.onMenuAdd.bind(this);
        this.onMenuEdit = this.onMenuEdit.bind(this);
        this.onMenuDelete = this.onMenuDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "menus"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.menus = await this._menuService.get();
        this.menuListElement.setAttribute("menus", JSON.stringify(this.menus));
    }

    private _setEventListeners() {
        this.addEventListener(menuActions.ADD, this.onMenuAdd);
        this.addEventListener(menuActions.EDIT, this.onMenuEdit);
        this.addEventListener(menuActions.DELETE, this.onMenuDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(menuActions.ADD, this.onMenuAdd);
        this.removeEventListener(menuActions.EDIT, this.onMenuEdit);
        this.removeEventListener(menuActions.DELETE, this.onMenuDelete);
    }

    public async onMenuAdd(e) {

        await this._menuService.add(e.detail.menu);
        this.menus = await this._menuService.get();
        
        this.menuListElement.setAttribute("menus", JSON.stringify(this.menus));
        this.menuEditElement.setAttribute("menu", JSON.stringify(new Menu()));
    }

    public onMenuEdit(e) {
        this.menuEditElement.setAttribute("menu", JSON.stringify(e.detail.menu));
    }

    public async onMenuDelete(e) {

        await this._menuService.remove(e.detail.menu.id);
        this.menus = await this._menuService.get();
        
        this.menuListElement.setAttribute("menus", JSON.stringify(this.menus));
        this.menuEditElement.setAttribute("menu", JSON.stringify(new Menu()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "menus":
                this.menus = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Menu> { return this.menus; }

    private menus: Array<Menu> = [];
    public menu: Menu = <Menu>{};
    public get menuEditElement(): HTMLElement { return this.querySelector("ce-menu-edit-embed") as HTMLElement; }
    public get menuListElement(): HTMLElement { return this.querySelector("ce-menu-list-embed") as HTMLElement; }
}

customElements.define(`ce-menu-master-detail`,MenuMasterDetailComponent);
