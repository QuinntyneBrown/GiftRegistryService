import { Personality } from "./personality.model";

export const personalityActions = {
    ADD: "[Personality] Add",
    EDIT: "[Personality] Edit",
    DELETE: "[Personality] Delete",
    PERSONALITYS_CHANGED: "[Personality] Personalitys Changed"
};

export class PersonalityEvent extends CustomEvent {
    constructor(eventName:string, personality: Personality) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { personality }
        });
    }
}

export class PersonalityAdd extends PersonalityEvent {
    constructor(personality: Personality) {
        super(personalityActions.ADD, personality);        
    }
}

export class PersonalityEdit extends PersonalityEvent {
    constructor(personality: Personality) {
        super(personalityActions.EDIT, personality);
    }
}

export class PersonalityDelete extends PersonalityEvent {
    constructor(personality: Personality) {
        super(personalityActions.DELETE, personality);
    }
}

export class PersonalitysChanged extends CustomEvent {
    constructor(personalitys: Array<Personality>) {
        super(personalityActions.PERSONALITYS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { personalitys }
        });
    }
}
