import { Guest } from "./guest.model";

const template = require("./guest-list-embed.component.html");
const styles = require("./guest-list-embed.component.scss");

export class GuestListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "guests"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.guests.length; i++) {
            let el = this._document.createElement(`ce-guest-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.guests[i]));
            this.appendChild(el);
        }    
    }

    guests:Array<Guest> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "guests":
                this.guests = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-guest-list-embed", GuestListEmbedComponent);
