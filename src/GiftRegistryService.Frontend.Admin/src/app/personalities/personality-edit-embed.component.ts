import { Personality } from "./personality.model";
import { EditorComponent } from "../shared";
import {  PersonalityDelete, PersonalityEdit, PersonalityAdd } from "./personality.actions";

const template = require("./personality-edit-embed.component.html");
const styles = require("./personality-edit-embed.component.scss");

export class PersonalityEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "personality",
            "personality-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.personality ? "Edit Personality": "Create Personality";

        if (this.personality) {                
            this._nameInputElement.value = this.personality.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
    }

    public onSave() {
        const personality = {
            id: this.personality != null ? this.personality.id : null,
            name: this._nameInputElement.value
        } as Personality;
        
        this.dispatchEvent(new PersonalityAdd(personality));            
    }

    public onDelete() {        
        const personality = {
            id: this.personality != null ? this.personality.id : null,
            name: this._nameInputElement.value
        } as Personality;

        this.dispatchEvent(new PersonalityDelete(personality));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "personality-id":
                this.personalityId = newValue;
                break;
            case "personality":
                this.personality = JSON.parse(newValue);
                if (this.parentNode) {
                    this.personalityId = this.personality.id;
                    this._nameInputElement.value = this.personality.name != undefined ? this.personality.name : "";
                    this._titleElement.textContent = this.personalityId ? "Edit Personality" : "Create Personality";
                }
                break;
        }           
    }

    public personalityId: any;
    public personality: Personality;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".personality-name") as HTMLInputElement;}
}

customElements.define(`ce-personality-edit-embed`,PersonalityEditEmbedComponent);
