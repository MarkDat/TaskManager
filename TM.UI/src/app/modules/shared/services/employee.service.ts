import { Injectable } from '@angular/core';
import { Employee } from '../models/employee.class';

@Injectable({
  providedIn: 'root'
})


export class EmployeeService {
   employees: Employee[] = [{
    id: 1,
    fullName: "John Heart",
    prefix: "Dr.",
    position: "CEO",
    expanded: true,
    items: [{
        id: 2,
        fullName: "Samantha Bright",
        prefix: "Dr.",
        position: "COO",
        expanded: true,
        items: []
    },{
        id: 3,
        fullName: "Kevin Carter",
        prefix: "Mr.",
        position: "Shipping Manager",
    }, {
        id: 14,
        fullName: "Victor Norris",
        prefix: "Mr.",
        selected: true,
        position: "Shipping Assistant"
    },{
        id: 0,
        fullName: "",
        prefix: "",
        selected: false,
        position: ""
    }]
  }];

  constructor() { }
  getEmployees(): Employee[] {
    return this.employees;
  }
}
