import { Component } from '@angular/core';
import { Employee } from '../../../models/employee.model';
import { CommonModule } from '@angular/common';
import { EmployeesService } from '../../../services/employees.service';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthenticationInterceptor } from '../../../services/interceptor';

@Component({
  selector: 'app-employees-list',
  standalone: true,
  imports: [CommonModule , RouterModule],
  providers:[{provide:HTTP_INTERCEPTORS,
    useClass: AuthenticationInterceptor , multi:true}],
  templateUrl: './employees-list.component.html',
  styleUrl: './employees-list.component.css'
})
export class EmployeesListComponent {
  employees : Employee[] = [];
  token: string="";

  constructor(private employeesService:EmployeesService , private route:ActivatedRoute){}


  ngOnInit():void{
    this.route.queryParams.subscribe(params => {
      this.token = params['token'];
      console.log(this.token);
      
    });
    this.employeesService.getAllEmployees(this.token)
    .subscribe({
      next : (employees)=>{
        this.employees = employees;
      },
      error:(response)=>{
        console.log(response);
      }
        
        
      
    })
  }
}
