import { MenuItemAdd, MenuItemDelete, MenuItemEdit, menuItemActions } from "./menu-item.actions";
import { MenuItem } from "./menu-item.model";
import { MenuItemService } from "./menu-item.service";

const template = require("./menu-item-master-detail.component.html");
const styles = require("./menu-item-master-detail.component.scss");

export class MenuItemMasterDetailComponent extends HTMLElement {
    constructor(
        private _menuItemService: MenuItemService = MenuItemService.Instance	
	) {
        super();
        this.onMenuItemAdd = this.onMenuItemAdd.bind(this);
        this.onMenuItemEdit = this.onMenuItemEdit.bind(this);
        this.onMenuItemDelete = this.onMenuItemDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "menu-items"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.menuItems = await this._menuItemService.get();
        this.menuItemListElement.setAttribute("menu-items", JSON.stringify(this.menuItems));
    }

    private _setEventListeners() {
        this.addEventListener(menuItemActions.ADD, this.onMenuItemAdd);
        this.addEventListener(menuItemActions.EDIT, this.onMenuItemEdit);
        this.addEventListener(menuItemActions.DELETE, this.onMenuItemDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(menuItemActions.ADD, this.onMenuItemAdd);
        this.removeEventListener(menuItemActions.EDIT, this.onMenuItemEdit);
        this.removeEventListener(menuItemActions.DELETE, this.onMenuItemDelete);
    }

    public async onMenuItemAdd(e) {

        await this._menuItemService.add(e.detail.menuItem);
        this.menuItems = await this._menuItemService.get();
        
        this.menuItemListElement.setAttribute("menu-items", JSON.stringify(this.menuItems));
        this.menuItemEditElement.setAttribute("menu-item", JSON.stringify(new MenuItem()));
    }

    public onMenuItemEdit(e) {
        this.menuItemEditElement.setAttribute("menu-item", JSON.stringify(e.detail.menuItem));
    }

    public async onMenuItemDelete(e) {

        await this._menuItemService.remove(e.detail.menuItem.id);
        this.menuItems = await this._menuItemService.get();
        
        this.menuItemListElement.setAttribute("menu-items", JSON.stringify(this.menuItems));
        this.menuItemEditElement.setAttribute("menu-item", JSON.stringify(new MenuItem()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "menu-items":
                this.menuItems = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<MenuItem> { return this.menuItems; }

    private menuItems: Array<MenuItem> = [];
    public menuItem: MenuItem = <MenuItem>{};
    public get menuItemEditElement(): HTMLElement { return this.querySelector("ce-menu-item-edit-embed") as HTMLElement; }
    public get menuItemListElement(): HTMLElement { return this.querySelector("ce-menu-item-list-embed") as HTMLElement; }
}

customElements.define(`ce-menu-item-master-detail`,MenuItemMasterDetailComponent);
