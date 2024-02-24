import { Component } from '@angular/core';
import { Login } from '../../../../models/login';
import { AuthenticationService } from '../../../../services/authentication.service';
import { JwtAuth } from '../../../../models/jwtAuth';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule , CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginDto = new Login();
  jwtDto = new JwtAuth();

  constructor(private router: Router,private authService: AuthenticationService){}

   login(loginDto:Login) {
    this.authService.login(this.loginDto).subscribe(
      (jwtDto: JwtAuth) => {
        const tokenWithBearer = `${jwtDto.token}`;
        localStorage.setItem('jwtToken', tokenWithBearer); 
        this.router.navigate(['employees'], { queryParams: { token: tokenWithBearer } });
      },
      (error) => {
        console.error('Login failed:', error); 
      }
      
    );
  }
}
