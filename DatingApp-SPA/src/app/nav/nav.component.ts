import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public auth: AuthService, private alertify: AlertifyService,private router:Router) {}

  ngOnInit() {}

  Login() {
    this.auth.login(this.model).subscribe(
      (next) => {
        this.alertify.success("logged in successfully");
      },
      (error) => {
        this.alertify.error(error)
      }, ()=>{
        this.router.navigate(['/members']);
      }
    );
  }

  loggedIn() {
    return this.auth.loggedIn();
  }

  logOut() {
    localStorage.removeItem('token');
    this.alertify.message("logged out successfully");
    this.router.navigate(['/home']);
  }
}
