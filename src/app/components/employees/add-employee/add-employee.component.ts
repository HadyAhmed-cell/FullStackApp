import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Employee } from '../../../models/employee.model';
import { EmployeesService } from '../../../services/employees.service';
import { Route, Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-add-employee',
  standalone: true,
  imports: [FormsModule , RouterLink ],
  templateUrl: './add-employee.component.html',
  styleUrl: './add-employee.component.css'
})
export class AddEmployeeComponent {

addEmployeeRequest:Employee={
  id:0,
  name:'',
  email:'',
  phone:0,
  department:''
}

constructor(private employeeService:EmployeesService , private router:Router){}

ngOnInit():void{

}

addEmployee(){
  this.employeeService.addEmployee(this.addEmployeeRequest)
  .subscribe({
    next:(loginDto)=>{
      this.router.navigate(['auth/login']);
  }
    
});
}
}
