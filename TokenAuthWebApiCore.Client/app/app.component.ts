import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IProfile } from './user/user.model';
import { UserService } from './user/user.service';
import { UserProfile } from './user/user.profile';

@Component({
    selector: 'jwt-app',
    templateUrl: './app/app.component.html'
})
export class AppComponent implements OnInit {
    pageTitle: string = 'Welcome to HMIS : A Webgen Initiative';
    loading: boolean = true;
    Profile: IProfile;

    constructor(private authService: UserService,
        private authProfile: UserProfile,
        private router: Router) {
    }
    ngOnInit(): void {
        debugger;
        this.Profile = this.authProfile.getProfile();
    }

    logOut(): void {
        this.authService.logout();

        this.router.navigateByUrl('/home');
    }
}