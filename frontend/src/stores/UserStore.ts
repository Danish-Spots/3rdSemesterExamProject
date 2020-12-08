import { action, makeObservable, observable } from 'mobx'
import { createContext } from 'react';

class UserStore {
    @observable isLoading = false;
    @observable isLoggedIn = sessionStorage.getItem("SessionKey") !== null;
    @observable username = sessionStorage.getItem("Username") !== null ? sessionStorage.getItem("Username") : "";
    @observable sessionKey = sessionStorage.getItem("SessionKey") !== null ? sessionStorage.getItem("SessionKey") : "";
    @observable profileID = sessionStorage.getItem("ProfileID") !== null ? sessionStorage.getItem("ProfileID") : 0;

    @action
    change_setDefault() {
        this.isLoading = true;
        this.isLoggedIn = false;
        this.username = "";
        this.sessionKey = "";
        this.profileID = 0;
    }

    @action
    change_login(isLoading : boolean, isLoggedIn : boolean, username : string, sessionKey : string, profileID : number) {
        this.isLoading = isLoading;
        this.isLoggedIn = isLoggedIn;
        this.username = username;
        this.sessionKey = sessionKey;
        this.profileID = profileID;
    }

    @action
    change_isLoading(value : boolean) {
        this.isLoading = value;
    }
    
    @action
    change_isLoggedIn(value : boolean) {
        this.isLoggedIn = value;
    }

    @action
    change_username(value : string) {
        this.username = value;
    }
    
    @action
    change_sessionKey(value : string) {
        this.sessionKey = value;
    }
    
    @action
    change_profileID(value : number) {
        this.profileID = value;
    }

}

export const UserStoreContext = createContext(makeObservable(new UserStore()));