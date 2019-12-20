import { BehaviorSubject } from 'rxjs';

const apiUrl = 'http://localhost:8000/api';
import { requestOptions } from '../_helpers/request-options';
import { handleResponse } from '../_helpers/handle-response';

const currentUserSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('currentUser')));

export const authenticationService = {
    login,
    logout,
    register,
    currentUser: currentUserSubject.asObservable(),
    get currentUserValue () { return currentUserSubject.value }
};

function login(username, password) {
    return fetch(apiUrl+`/users/authenticate`, requestOptions.post({ username, password }))
        .then(handleResponse)
        .then(user => {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify(user));
            currentUserSubject.next(user);

            return user;
        });
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    currentUserSubject.next(null);
}

function register(user) {
    return fetch(apiUrl+`/users/register`, requestOptions.post(user)).then(handleResponse);
}