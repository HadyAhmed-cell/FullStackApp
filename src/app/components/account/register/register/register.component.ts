import { Component } from '@angular/core';
import { AuthenticationService } from '../../../../services/authentication.service';
import { Register } from '../../../../models/register';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule , FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerDto = new Register();



  constructor(private authService:AuthenticationService , private router:Router){}

  register(registerDto:Register){
    this.authService.register(registerDto).subscribe({
      next:(loginDto)=>{
        this.router.navigate(['auth/login']);
    }
      
  });
  }
}
