import { Personality } from "./personality.model";

const template = require("./personality-list-embed.component.html");
const styles = require("./personality-list-embed.component.scss");

export class PersonalityListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "personalitys"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.personalitys.length; i++) {
            let el = this._document.createElement(`ce-personality-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.personalitys[i]));
            this.appendChild(el);
        }    
    }

    personalitys:Array<Personality> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "personalitys":
                this.personalitys = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-personality-list-embed", PersonalityListEmbedComponent);
