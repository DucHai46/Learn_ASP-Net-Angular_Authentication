import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { LayOutRoutingModule } from './layout-routing.module';
import { FormsModule } from '@angular/forms';
import { AuthGuard } from '../services/auth.guard';

@NgModule({
  imports: [
    CommonModule,
    LayOutRoutingModule,
    FormsModule
  ],
  exports: [HomeComponent, SignInComponent, SignUpComponent],
  declarations: [HomeComponent, SignInComponent, SignUpComponent],
  providers: [AuthGuard]
})
export class LayoutModule { }
