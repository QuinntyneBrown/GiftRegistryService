import { GuestAdd, GuestDelete, GuestEdit, guestActions } from "./guest.actions";
import { Guest } from "./guest.model";
import { GuestService } from "./guest.service";

const template = require("./guest-master-detail.component.html");
const styles = require("./guest-master-detail.component.scss");

export class GuestMasterDetailComponent extends HTMLElement {
    constructor(
        private _guestService: GuestService = GuestService.Instance	
	) {
        super();
        this.onGuestAdd = this.onGuestAdd.bind(this);
        this.onGuestEdit = this.onGuestEdit.bind(this);
        this.onGuestDelete = this.onGuestDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "guests"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.guests = await this._guestService.get();
        this.guestListElement.setAttribute("guests", JSON.stringify(this.guests));
    }

    private _setEventListeners() {
        this.addEventListener(guestActions.ADD, this.onGuestAdd);
        this.addEventListener(guestActions.EDIT, this.onGuestEdit);
        this.addEventListener(guestActions.DELETE, this.onGuestDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(guestActions.ADD, this.onGuestAdd);
        this.removeEventListener(guestActions.EDIT, this.onGuestEdit);
        this.removeEventListener(guestActions.DELETE, this.onGuestDelete);
    }

    public async onGuestAdd(e) {

        await this._guestService.add(e.detail.guest);
        this.guests = await this._guestService.get();
        
        this.guestListElement.setAttribute("guests", JSON.stringify(this.guests));
        this.guestEditElement.setAttribute("guest", JSON.stringify(new Guest()));
    }

    public onGuestEdit(e) {
        this.guestEditElement.setAttribute("guest", JSON.stringify(e.detail.guest));
    }

    public async onGuestDelete(e) {

        await this._guestService.remove(e.detail.guest.id);
        this.guests = await this._guestService.get();
        
        this.guestListElement.setAttribute("guests", JSON.stringify(this.guests));
        this.guestEditElement.setAttribute("guest", JSON.stringify(new Guest()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "guests":
                this.guests = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Guest> { return this.guests; }

    private guests: Array<Guest> = [];
    public guest: Guest = <Guest>{};
    public get guestEditElement(): HTMLElement { return this.querySelector("ce-guest-edit-embed") as HTMLElement; }
    public get guestListElement(): HTMLElement { return this.querySelector("ce-guest-list-embed") as HTMLElement; }
}

customElements.define(`ce-guest-master-detail`,GuestMasterDetailComponent);
