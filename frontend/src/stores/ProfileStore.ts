import { action, makeObservable, observable } from 'mobx'
import { createContext } from 'react';
import Profile from '../classes/Profile';

class ProfileStore {
    @observable profile = new Profile(0,"Not existing profile", "null", new Date(2018, 11, 24, 10, 33, 30, 0), "null", "null","null");
    @observable isEditing = false;

    @action
    change_setDefault() {
        this.profile = new Profile(0,"Not existing profile", "null", new Date(2018, 11, 24, 10, 33, 30, 0), "null", "null","null");
        this.isEditing = false;
    }

    @action
    change_setProfile(profile : Profile) {
        this.profile = profile;
    }

    @action
    change_setIsEditing(isEditing : boolean) {
        this.isEditing = isEditing;
    }

}

export const ProfileStoreContext = createContext(makeObservable(new ProfileStore()));