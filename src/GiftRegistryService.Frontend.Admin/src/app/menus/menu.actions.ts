import { Menu } from "./menu.model";

export const menuActions = {
    ADD: "[Menu] Add",
    EDIT: "[Menu] Edit",
    DELETE: "[Menu] Delete",
    MENUS_CHANGED: "[Menu] Menus Changed"
};

export class MenuEvent extends CustomEvent {
    constructor(eventName:string, menu: Menu) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { menu }
        });
    }
}

export class MenuAdd extends MenuEvent {
    constructor(menu: Menu) {
        super(menuActions.ADD, menu);        
    }
}

export class MenuEdit extends MenuEvent {
    constructor(menu: Menu) {
        super(menuActions.EDIT, menu);
    }
}

export class MenuDelete extends MenuEvent {
    constructor(menu: Menu) {
        super(menuActions.DELETE, menu);
    }
}

export class MenusChanged extends CustomEvent {
    constructor(menus: Array<Menu>) {
        super(menuActions.MENUS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { menus }
        });
    }
}
