import { Component } from '@angular/core';
import { AuthService } from '../core/service/guard/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  constructor(private auth:AuthService,private router:Router) {}
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  isLoggedIn() {
    const token = this.auth.Token;
    return !!token;
  }

  logout() {
    if (this.auth.logout())
      this.router.navigate(['login']);
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
