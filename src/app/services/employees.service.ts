import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Employee } from '../models/employee.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {


  baseApiUrl: string = environment.baseApiUrl;
  constructor(private http:HttpClient) { }

  getAllEmployees(token: string):Observable<Employee[]>{
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
     return this.http.get<Employee[]>(this.baseApiUrl + '/api/Employees' , {headers});
  }

  addEmployee(addEmployeeRequest: Employee) : Observable<Employee>{
   return this.http.post<Employee>(this.baseApiUrl + '/api/Employees' ,addEmployeeRequest);
  }

  getEmployee(id:number):Observable<Employee>{
    return this.http.get<Employee>(this.baseApiUrl + '/api/Employees/'+id);

  }

  updateEmployee(id:number , updateEmployeeRequest:Employee):Observable<Employee>{
   return this.http.put<Employee>(this.baseApiUrl + '/api/Employees/'+id ,updateEmployeeRequest);
  }

  deleteEmployee(id:number):Observable<Employee>{
    return this.http.delete<Employee>(this.baseApiUrl + '/api/Employees/'+id );

  }
   

}
