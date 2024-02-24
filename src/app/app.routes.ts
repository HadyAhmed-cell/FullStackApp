import { RouterModule, Routes } from '@angular/router';
import { EmployeesListComponent } from './components/employees/employees-list/employees-list.component';
import { NgModule } from '@angular/core';
import { AddEmployeeComponent } from './components/employees/add-employee/add-employee.component';
import { EditEmployeeComponent } from './components/employees/edit-employee/edit-employee.component';
import { LoginComponent } from './components/account/login/login/login.component';
import { RegisterComponent } from './components/account/register/register/register.component';

export const routes: Routes = [
    {
        path:'',
        component: EmployeesListComponent
    },
    {
        path:'employees',
        component: EmployeesListComponent
    },
    {
        path:'employees/add',
        component: AddEmployeeComponent
    },
    {
        path:'employees/edit/:id',
        component: EditEmployeeComponent
    },
    {
        path:'auth/login',
        component: LoginComponent
    },
    {
        path:'auth/register',
        component: RegisterComponent
    }


    
];

