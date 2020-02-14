import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UserComponent } from './user.component';
import { SigninComponent } from './signin/signin.component';

const routes: Routes = [{
  path: '',
  component: UserComponent,
  children: [
    {
      path: 'signin',
      component: SigninComponent,
    }
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserRoutingModule {
}
