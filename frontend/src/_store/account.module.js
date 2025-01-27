import { authenticationService } from '../_services/authentication.service';

const user = JSON.parse(localStorage.getItem('user'));
const state = user
    ? { status: { loggedIn: true }, user }
    : { status: {}, user: null };
    
const actions = {
    login({ dispatch, commit }, { username, password, router }) {
        commit('loginRequest', { username });
        authenticationService.login(username, password)
            .then(
                user => {
                    commit('loginSuccess', user);
                    router.push('/')
                    window.location.reload()
              
                },
                error => {
                    commit('loginFailure', error);
                    dispatch('alert/error', error, { root: true });
                }
            );
    },
    logout({ commit }) {
        authenticationService.logout();
        commit('logout');
        window.location.reload()
    },
    register({ dispatch, commit }, {user, router}) {
        commit('registerRequest', user);
    
        authenticationService.register(user)
            .then(
                user => {
                    commit('registerSuccess', user);
                    router.push('/login')
                    setTimeout(() => {
                        // display success message after route change completes
                        dispatch('alert/success', 'Registration successful', { root: true });
                    })
                    console.log("pavyko")
                    
                },
                error => {
                    commit('registerFailure', error);
                    dispatch('alert/error', error, { root: true });
                    console.log("nepavyko");
                }
            );
    }
};

const mutations = {
    loginRequest(state, user) {
        state.status = { loggingIn: true };
        state.user = user;
    },
    loginSuccess(state, user) {
        state.status = { loggedIn: true };
        state.user = user;
    },
    loginFailure(state) {
        state.status = {};
        state.user = null;
    },
    logout(state) {
        state.status = {};
        state.user = null;
    },
    registerRequest(state, user) {
        state.status = { registering: true };
    },
    registerSuccess(state, user) {
        state.status = {};
    },
    registerFailure(state, error) {
        state.status = {};
    }
};

export const account = {
    namespaced: true,
    state,
    actions,
    mutations
};