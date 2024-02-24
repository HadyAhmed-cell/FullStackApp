import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { EmployeesService } from '../../../services/employees.service';
import { Employee } from '../../../models/employee.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-edit-employee',
  standalone: true,
  imports: [FormsModule , RouterLink ],
  templateUrl: './edit-employee.component.html',
  styleUrl: './edit-employee.component.css'
})
export class EditEmployeeComponent {

  employeeDetails:Employee = {
    id:0,
    name:'',
    email:'',
    phone:0,
    department:''
  }
constructor(private route:ActivatedRoute , private employeeService:EmployeesService ,private router:Router){}
ngOnInit():void{
this.route.paramMap.subscribe({
  next:(params)=>{
    const idString = params.get('id');
const id = idString ? parseInt(idString, 10) : NaN;


    if(id){
this.employeeService.getEmployee(id)
.subscribe({
  next:(response)=>{
    this.employeeDetails = response
  }
})
    }
  }
})
}

updateEmployee(){
  this.employeeService.updateEmployee(this.employeeDetails.id ,this.employeeDetails )
  .subscribe({
    next:(loginDto)=>{
      this.router.navigate(['auth/login']);
  }
    
});
}

deleteEmployee(id:number){
  this.employeeService.deleteEmployee(id)
  .subscribe({
      next:(loginDto)=>{
        this.router.navigate(['auth/login']);
    }
      
  })
}


}
