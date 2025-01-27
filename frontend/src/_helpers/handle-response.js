import { authenticationService } from '../_services/authentication.service';

export function handleResponse(response) {
    return response.text().then(text => {
        console.log(response);
        const data = text || JSON.parse(text);
        console.log(data)
        if (!response.ok) {
            if ([401, 403].indexOf(response.status) !== -1) {
                // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
                authenticationService.logout();
                location.reload(true);
            }

            const error = (data || data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}