// import config from 'config';
import { requestOptions } from '../_helpers/request-options.js';
import { handleResponse } from '../_helpers/handle-response.js';
const apiUrl = 'http://localhost:8000/api';
export const userService = {
    getAll,
    getById,
    update,
    delete: _delete
};

function getAll() {
    return fetch(apiUrl+`/users`, requestOptions.get()).then(handleResponse);
}

function getById(id) {
    console.log(id)
    return fetch(apiUrl+`/users/`+id, requestOptions.get()).then(handleResponse);
}

function update(user) {

    return fetch(apiUrl+`/users/${user.id}`, requestOptions.put(user)).then(handleResponse);
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {

    return fetch(apiUrl+`/users/${id}`, requestOptions.delete()).then(handleResponse);
}