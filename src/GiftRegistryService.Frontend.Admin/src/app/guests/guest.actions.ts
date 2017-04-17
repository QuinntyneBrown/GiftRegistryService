import { Guest } from "./guest.model";

export const guestActions = {
    ADD: "[Guest] Add",
    EDIT: "[Guest] Edit",
    DELETE: "[Guest] Delete",
    GUESTS_CHANGED: "[Guest] Guests Changed"
};

export class GuestEvent extends CustomEvent {
    constructor(eventName:string, guest: Guest) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { guest }
        });
    }
}

export class GuestAdd extends GuestEvent {
    constructor(guest: Guest) {
        super(guestActions.ADD, guest);        
    }
}

export class GuestEdit extends GuestEvent {
    constructor(guest: Guest) {
        super(guestActions.EDIT, guest);
    }
}

export class GuestDelete extends GuestEvent {
    constructor(guest: Guest) {
        super(guestActions.DELETE, guest);
    }
}

export class GuestsChanged extends CustomEvent {
    constructor(guests: Array<Guest>) {
        super(guestActions.GUESTS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { guests }
        });
    }
}
