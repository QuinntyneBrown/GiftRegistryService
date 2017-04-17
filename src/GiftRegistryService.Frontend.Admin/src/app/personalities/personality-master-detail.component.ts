import { PersonalityAdd, PersonalityDelete, PersonalityEdit, personalityActions } from "./personality.actions";
import { Personality } from "./personality.model";
import { PersonalityService } from "./personality.service";

const template = require("./personality-master-detail.component.html");
const styles = require("./personality-master-detail.component.scss");

export class PersonalityMasterDetailComponent extends HTMLElement {
    constructor(
        private _personalityService: PersonalityService = PersonalityService.Instance	
	) {
        super();
        this.onPersonalityAdd = this.onPersonalityAdd.bind(this);
        this.onPersonalityEdit = this.onPersonalityEdit.bind(this);
        this.onPersonalityDelete = this.onPersonalityDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "personalitys"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.personalitys = await this._personalityService.get();
        this.personalityListElement.setAttribute("personalitys", JSON.stringify(this.personalitys));
    }

    private _setEventListeners() {
        this.addEventListener(personalityActions.ADD, this.onPersonalityAdd);
        this.addEventListener(personalityActions.EDIT, this.onPersonalityEdit);
        this.addEventListener(personalityActions.DELETE, this.onPersonalityDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(personalityActions.ADD, this.onPersonalityAdd);
        this.removeEventListener(personalityActions.EDIT, this.onPersonalityEdit);
        this.removeEventListener(personalityActions.DELETE, this.onPersonalityDelete);
    }

    public async onPersonalityAdd(e) {

        await this._personalityService.add(e.detail.personality);
        this.personalitys = await this._personalityService.get();
        
        this.personalityListElement.setAttribute("personalitys", JSON.stringify(this.personalitys));
        this.personalityEditElement.setAttribute("personality", JSON.stringify(new Personality()));
    }

    public onPersonalityEdit(e) {
        this.personalityEditElement.setAttribute("personality", JSON.stringify(e.detail.personality));
    }

    public async onPersonalityDelete(e) {

        await this._personalityService.remove(e.detail.personality.id);
        this.personalitys = await this._personalityService.get();
        
        this.personalityListElement.setAttribute("personalitys", JSON.stringify(this.personalitys));
        this.personalityEditElement.setAttribute("personality", JSON.stringify(new Personality()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "personalitys":
                this.personalitys = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Personality> { return this.personalitys; }

    private personalitys: Array<Personality> = [];
    public personality: Personality = <Personality>{};
    public get personalityEditElement(): HTMLElement { return this.querySelector("ce-personality-edit-embed") as HTMLElement; }
    public get personalityListElement(): HTMLElement { return this.querySelector("ce-personality-list-embed") as HTMLElement; }
}

customElements.define(`ce-personality-master-detail`,PersonalityMasterDetailComponent);
