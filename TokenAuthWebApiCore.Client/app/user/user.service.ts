import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';

import { CommonService } from '../shared/common.service';
import { contentHeaders } from '../common/headers';
import { UserProfile } from './user.profile';
import { IProfile } from './user.model';
import { Observable } from "rxjs/Observable";

@Injectable()
export class UserService {
    redirectUrl: string;
    errorMessage: string;
    constructor(
        private http: Http,
        private router: Router,
        private authProfile: UserProfile,
        private commonService: CommonService) { }

    isAuthenticated() {
        let profile = this.authProfile.getProfile();
        var validToken = profile.token != "" && profile.token != null;
        var isTokenExpired = this.isTokenExpired(profile);
        return validToken && !isTokenExpired;
    }
    isAuthorized() {
        let profile = this.authProfile.getProfile();
        var validToken = profile.token != "" && profile.token != null;
        var isTokenExpired = this.isTokenExpired(profile);
        return validToken && !isTokenExpired;
    }
    isTokenExpired(profile: IProfile) {
        var expiration = new Date(profile.expiration)
        return expiration < new Date();
    }
    /* 
    User login to get token in reponse
    */
    login(userName: string, password: string) {
        if (!userName || !password) {
            return;
        }
        contentHeaders.append('Authorization', 'Basic ' + btoa(userName + ':' + password));
        let options = new RequestOptions(
            { headers: contentHeaders });
        
        var credentials = {
            grant_type: 'password',
            email: userName,
            password: password
        };
        let url = this.commonService.getBaseUrl() + '/authenticate';

        return this.http.post(url, credentials, options)
            .map((response: Response) => {
                debugger;
                var responseObject = response.json();                
                var userProfile: IProfile = response.json();
                userProfile.currentUser = responseObject.userData;
                this.authProfile.setProfile(userProfile);
                sessionStorage.setItem('access_token', response.headers.get("Token"));
                sessionStorage.setItem('expires_in', response.headers.get("TokenExpiry"));
                return response.json();
            }).catch(this.commonService.handleFullError)       
    }
    /// to get looged in user information with all roles to it by passing token
    getProducts(): Observable<IProfile> {       
        debugger;
        let options = null;
        let profile = this.authProfile.getProfile();
        let url = this.commonService.getBaseUrl() + '/admin' + '?id=' + profile.currentUser.id;

        if (profile != null && profile != undefined) {
           // let headers = new Headers({ 'Authorization': 'Bearer ' + profile.token });
            let headers = new Headers({ 'Token': profile.token });
            options = new RequestOptions({ headers: headers });
        }
        let data: Observable<IProfile> = this.http.get(url, options)        
            .map(res => res.json())
            .do(data => console.log('getProducts: ' + JSON.stringify(data)))
            .catch(this.commonService.handleError);

        return data;
    }

    register(userName: string, password: string, confirmPassword: string) {
        if (!userName || !password) {
            return;
        }
        let options = new RequestOptions(
            { headers: contentHeaders });

        var credentials = {
            email: userName,
            password: password,
            confirmPassword: confirmPassword
        };
        let url = this.commonService.getBaseUrl() + '/auth/register';
        return this.http.post(url, credentials, options)
            .map((response: Response) => {
                return response.json();
            }).catch(this.commonService.handleFullError);
    }

    logout(): void {
        this.authProfile.resetProfile();
    }
}