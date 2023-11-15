import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  public isUserAuthenticated: boolean = false;

  constructor(private authService: LoginService, private router: Router) { }

  ngOnInit(): void {
    this.isUserAuthenticated = this.authService.changedAuth();
  }

  public logout = () => {
    this.authService.logout();
    this.router.navigate(["/signin"]);
  }

}
