import { Guest } from "./guest.model";
import { EditorComponent } from "../shared";
import {  GuestDelete, GuestEdit, GuestAdd } from "./guest.actions";

const template = require("./guest-edit-embed.component.html");
const styles = require("./guest-edit-embed.component.scss");

export class GuestEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "guest",
            "guest-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.guest ? "Edit Guest": "Create Guest";

        if (this.guest) {                
            this._nameInputElement.value = this.guest.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._createButtonElement.addEventListener("click", this.onCreate);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._createButtonElement.removeEventListener("click", this.onCreate);
    }

    public onSave() {
        const guest = {
            id: this.guest != null ? this.guest.id : null,
            name: this._nameInputElement.value
        } as Guest;
        
        this.dispatchEvent(new GuestAdd(guest));            
    }

    public onCreate() {        
        this.dispatchEvent(new GuestEdit(new Guest()));            
    }

    public onDelete() {        
        const guest = {
            id: this.guest != null ? this.guest.id : null,
            name: this._nameInputElement.value
        } as Guest;

        this.dispatchEvent(new GuestDelete(guest));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "guest-id":
                this.guestId = newValue;
                break;
            case "guest":
                this.guest = JSON.parse(newValue);
                if (this.parentNode) {
                    this.guestId = this.guest.id;
                    this._nameInputElement.value = this.guest.name != undefined ? this.guest.name : "";
                    this._titleElement.textContent = this.guestId ? "Edit Guest" : "Create Guest";
                }
                break;
        }           
    }

    public guestId: any;
    
	public guest: Guest;
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".guest-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
	private get _nameInputElement(): HTMLInputElement { return this.querySelector(".guest-name") as HTMLInputElement;}
}

customElements.define(`ce-guest-edit-embed`,GuestEditEmbedComponent);
