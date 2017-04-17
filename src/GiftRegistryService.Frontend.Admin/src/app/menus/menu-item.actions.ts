import { MenuItem } from "./menu-item.model";

export const menuItemActions = {
    ADD: "[MenuItem] Add",
    EDIT: "[MenuItem] Edit",
    DELETE: "[MenuItem] Delete",
    MENU_ITEMS_CHANGED: "[MenuItem] MenuItems Changed"
};

export class MenuItemEvent extends CustomEvent {
    constructor(eventName:string, menuItem: MenuItem) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { menuItem }
        });
    }
}

export class MenuItemAdd extends MenuItemEvent {
    constructor(menuItem: MenuItem) {
        super(menuItemActions.ADD, menuItem);        
    }
}

export class MenuItemEdit extends MenuItemEvent {
    constructor(menuItem: MenuItem) {
        super(menuItemActions.EDIT, menuItem);
    }
}

export class MenuItemDelete extends MenuItemEvent {
    constructor(menuItem: MenuItem) {
        super(menuItemActions.DELETE, menuItem);
    }
}

export class MenuItemsChanged extends CustomEvent {
    constructor(menuItems: Array<MenuItem>) {
        super(menuItemActions.MENU_ITEMS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { menuItems }
        });
    }
}
