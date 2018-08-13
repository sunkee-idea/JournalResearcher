import { Routes, RouterModule } from '@angular/router';
import{NgModule } from '@angular/core';

import { SignInComponent } from './auth/sign-in/sign-in.component';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { RoleGuardService } from './core/service/guard/role-gaurd.service';
import { UserComponent } from './user/user.component';
import { UserAddComponent } from './user/user-add/user-add.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [

 
  {
    path: 'admin',
    loadChildren: './admin/admin.module#AdminModule',
    runGuardsAndResolvers: 'always',
    canActivate: [RoleGuardService],
    data: {
      expectedRole:'Admin'
    }
    

  },
  {
    path:'applicant',component:HomeComponent,
    runGuardsAndResolvers: 'always',
    canActivate: [RoleGuardService],
    data: {
      expectedRole: 'User'
    },
    children: [
      { path: 'create' ,component:UserAddComponent},
      { path: 'home' ,component:UserComponent},
    ]

  },
 
  {path:'login', component:SignInComponent},
  {path:'register', component:SignUpComponent},
  { path: '', component: SignInComponent },
  {path:'**',redirectTo:'login',pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]

})
export class Approuting {}
