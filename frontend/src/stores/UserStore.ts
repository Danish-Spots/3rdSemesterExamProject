import { observable, action } from 'mobx'
import { createContext } from 'react';

class UserStore {
    @observable isLoading = false;
    @observable isLoggedIn = false;
    @observable username = "";
    @observable sessionKey = "";
    @observable profileID = 0;
}

export const UserStoreContext = createContext(new UserStore());