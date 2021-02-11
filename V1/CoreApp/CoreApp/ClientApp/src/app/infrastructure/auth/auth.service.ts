import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { Credentials, LoginContext } from '../models';
import { LocalStorage, SessionStorage, ApiService } from '@core/services';
import { Constants } from '@core/constants';

@Injectable()
export class AuthService {

    private _credentials: Credentials = null;

    constructor(private apiService: ApiService, private localStorage: LocalStorage, private sessionStorage: SessionStorage) {
        const savedCredentials = this.sessionStorage.get(Constants.CredentialsKey) || this.localStorage.get(Constants.CredentialsKey);
        if (savedCredentials)
            this._credentials = JSON.parse(savedCredentials);
    }

    /**
     * Authenticates the user.
     * @param {LoginContext} context The login parameters.
     * @return {Observable<Credentials>} The user credentials.
     */
    login(context: LoginContext): Observable<boolean> {
        let model = {
            identifier: context.username,
            secret: context.password,
            isPersistent: context.remember
        };
        return Observable.create((observer) => {
            this.apiService.post(`AuthenticationApi/Login`, model).subscribe((credentials: any) => {
                this.setCredentials(credentials, context.remember);
                if (credentials == null) {
                  observer.next(false);
                } else {
                  observer.next(true);
                };
            }, (error) => {
                observer.next(false);
            });
        });

    }

    /**
     * Logs out the user and clear credentials.
     * @return {Observable<boolean>} True if the user was logged out successfully.
     */
    logout(): Observable<boolean> {
        this.setCredentials();
        return of(true);
    }

    /**
     * Checks is the user is authenticated.
     * @return {boolean} True if the user is authenticated.
     */
    isAuthenticated(): boolean {
        return !!this.credentials;
    }

    /**
     * Gets the user credentials.
     * @return {Credentials} The user credentials or null if the user is not authenticated.
     */
    get credentials(): Credentials | null {
        return this._credentials;
    }

    get token(): string {
        if (this._credentials) {
            const tokenObj = JSON.parse(this._credentials.token);
            return tokenObj.auth_token;
        }
        return '';
    }

    get currentUser(): any {
        if (this._credentials) {
            return this._credentials.user;
        }
        return null;
    }

    /**
     * Sets the user credentials.
     * The credentials may be persisted across sessions by setting the `remember` parameter to true.
     * Otherwise, the credentials are only persisted for the current session.
     * @param {Credentials=} credentials The user credentials.
     * @param {boolean=} remember True to remember credentials across sessions.
     */
    private setCredentials(credentials?: Credentials, remember?: boolean) {
        this._credentials = credentials || null;
        if (credentials) {
            const storage = remember ? this.localStorage : this.sessionStorage;
            storage.set(Constants.CredentialsKey, JSON.stringify(credentials));
        } else {
            this.sessionStorage.remove(Constants.CredentialsKey);
            this.localStorage.remove(Constants.CredentialsKey);
        }
    }

}
