import { AuthService } from './auth.service';
import { Router, ActivatedRouteSnapshot, CanActivate } from '@angular/router';
import { Injectable } from '@angular/core';
import decode from 'jwt-decode';

@Injectable()
export class RoleGuardService implements CanActivate {

  constructor(public auth: AuthService, public router: Router) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole = route.data.expectedRole;

    const role = this.auth.role;
    if (!this.auth.isAuthenticated() || role !== expectedRole) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }

}
