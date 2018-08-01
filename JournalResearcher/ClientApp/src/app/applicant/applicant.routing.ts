import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders, NgModule } from '@angular/core';
import {ApplicantComponent} from './applicant.component';
import { ApplicantAddComponent } from './applicant-add/applicant-add.component';


const routes: Routes = [
  {
    path: '',
    component:ApplicantComponent
  },
{
  path: 'add',
  component: ApplicantAddComponent
}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]

})
export class ApplicantRouting {}
